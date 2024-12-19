using AiDrivenSystem.APIs;
using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.APIs.Errors;
using AiDrivenSystem.APIs.Extensions;
using AiDrivenSystem.Infrastructure;
using AiDrivenSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AiDrivenSystem.APIs;

public abstract class AiActionsServiceBase : IAiActionsService
{
    protected readonly AiDrivenSystemDbContext _context;

    public AiActionsServiceBase(AiDrivenSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one AIAction
    /// </summary>
    public async Task<AiAction> CreateAiAction(AiActionCreateInput createDto)
    {
        var aiAction = new AiActionDbModel
        {
            ActionName = createDto.ActionName,
            CreatedAt = createDto.CreatedAt,
            Output = createDto.Output,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            aiAction.Id = createDto.Id;
        }
        if (createDto.Node != null)
        {
            aiAction.Node = await _context
                .Nodes.Where(node => createDto.Node.Id == node.Id)
                .FirstOrDefaultAsync();
        }

        _context.AiActions.Add(aiAction);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AiActionDbModel>(aiAction.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one AIAction
    /// </summary>
    public async Task DeleteAiAction(AiActionWhereUniqueInput uniqueId)
    {
        var aiAction = await _context.AiActions.FindAsync(uniqueId.Id);
        if (aiAction == null)
        {
            throw new NotFoundException();
        }

        _context.AiActions.Remove(aiAction);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many AIActions
    /// </summary>
    public async Task<List<AiAction>> AiActions(AiActionFindManyArgs findManyArgs)
    {
        var aiActions = await _context
            .AiActions.Include(x => x.Node)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return aiActions.ConvertAll(aiAction => aiAction.ToDto());
    }

    /// <summary>
    /// Meta data about AIAction records
    /// </summary>
    public async Task<MetadataDto> AiActionsMeta(AiActionFindManyArgs findManyArgs)
    {
        var count = await _context.AiActions.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one AIAction
    /// </summary>
    public async Task<AiAction> AiAction(AiActionWhereUniqueInput uniqueId)
    {
        var aiActions = await this.AiActions(
            new AiActionFindManyArgs { Where = new AiActionWhereInput { Id = uniqueId.Id } }
        );
        var aiAction = aiActions.FirstOrDefault();
        if (aiAction == null)
        {
            throw new NotFoundException();
        }

        return aiAction;
    }

    /// <summary>
    /// Update one AIAction
    /// </summary>
    public async Task UpdateAiAction(
        AiActionWhereUniqueInput uniqueId,
        AiActionUpdateInput updateDto
    )
    {
        var aiAction = updateDto.ToModel(uniqueId);

        if (updateDto.Node != null)
        {
            aiAction.Node = await _context
                .Nodes.Where(node => updateDto.Node == node.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(aiAction).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.AiActions.Any(e => e.Id == aiAction.Id))
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
    /// Get a Node record for AIAction
    /// </summary>
    public async Task<Node> GetNode(AiActionWhereUniqueInput uniqueId)
    {
        var aiAction = await _context
            .AiActions.Where(aiAction => aiAction.Id == uniqueId.Id)
            .Include(aiAction => aiAction.Node)
            .FirstOrDefaultAsync();
        if (aiAction == null)
        {
            throw new NotFoundException();
        }
        return aiAction.Node.ToDto();
    }
}
