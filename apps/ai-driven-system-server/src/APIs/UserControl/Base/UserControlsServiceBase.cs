using AiDrivenSystem.APIs;
using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.APIs.Errors;
using AiDrivenSystem.APIs.Extensions;
using AiDrivenSystem.Infrastructure;
using AiDrivenSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AiDrivenSystem.APIs;

public abstract class UserControlsServiceBase : IUserControlsService
{
    protected readonly AiDrivenSystemDbContext _context;

    public UserControlsServiceBase(AiDrivenSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one UserControl
    /// </summary>
    public async Task<UserControl> CreateUserControl(UserControlCreateInput createDto)
    {
        var userControl = new UserControlDbModel
        {
            ControlType = createDto.ControlType,
            CreatedAt = createDto.CreatedAt,
            Parameters = createDto.Parameters,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            userControl.Id = createDto.Id;
        }
        if (createDto.Node != null)
        {
            userControl.Node = await _context
                .Nodes.Where(node => createDto.Node.Id == node.Id)
                .FirstOrDefaultAsync();
        }

        _context.UserControls.Add(userControl);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<UserControlDbModel>(userControl.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one UserControl
    /// </summary>
    public async Task DeleteUserControl(UserControlWhereUniqueInput uniqueId)
    {
        var userControl = await _context.UserControls.FindAsync(uniqueId.Id);
        if (userControl == null)
        {
            throw new NotFoundException();
        }

        _context.UserControls.Remove(userControl);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many UserControls
    /// </summary>
    public async Task<List<UserControl>> UserControls(UserControlFindManyArgs findManyArgs)
    {
        var userControls = await _context
            .UserControls.Include(x => x.Node)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return userControls.ConvertAll(userControl => userControl.ToDto());
    }

    /// <summary>
    /// Meta data about UserControl records
    /// </summary>
    public async Task<MetadataDto> UserControlsMeta(UserControlFindManyArgs findManyArgs)
    {
        var count = await _context.UserControls.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one UserControl
    /// </summary>
    public async Task<UserControl> UserControl(UserControlWhereUniqueInput uniqueId)
    {
        var userControls = await this.UserControls(
            new UserControlFindManyArgs { Where = new UserControlWhereInput { Id = uniqueId.Id } }
        );
        var userControl = userControls.FirstOrDefault();
        if (userControl == null)
        {
            throw new NotFoundException();
        }

        return userControl;
    }

    /// <summary>
    /// Update one UserControl
    /// </summary>
    public async Task UpdateUserControl(
        UserControlWhereUniqueInput uniqueId,
        UserControlUpdateInput updateDto
    )
    {
        var userControl = updateDto.ToModel(uniqueId);

        if (updateDto.Node != null)
        {
            userControl.Node = await _context
                .Nodes.Where(node => updateDto.Node == node.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(userControl).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.UserControls.Any(e => e.Id == userControl.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Node record for UserControl
    /// </summary>
    public async Task<Node> GetNode(UserControlWhereUniqueInput uniqueId)
    {
        var userControl = await _context
            .UserControls.Where(userControl => userControl.Id == uniqueId.Id)
            .Include(userControl => userControl.Node)
            .FirstOrDefaultAsync();
        if (userControl == null)
        {
            throw new NotFoundException();
        }
        return userControl.Node.ToDto();
    }
}
