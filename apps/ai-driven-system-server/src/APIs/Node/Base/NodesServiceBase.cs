using AiDrivenSystem.APIs;
using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.APIs.Errors;
using AiDrivenSystem.APIs.Extensions;
using AiDrivenSystem.Infrastructure;
using AiDrivenSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AiDrivenSystem.APIs;

public abstract class NodesServiceBase : INodesService
{
    protected readonly AiDrivenSystemDbContext _context;

    public NodesServiceBase(AiDrivenSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Node
    /// </summary>
    public async Task<Node> CreateNode(NodeCreateInput createDto)
    {
        var node = new NodeDbModel
        {
            AiResponse = createDto.AiResponse,
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Status = createDto.Status,
            Title = createDto.Title,
            TypeField = createDto.TypeField,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            node.Id = createDto.Id;
        }
        if (createDto.AiActions != null)
        {
            node.AiActions = await _context
                .AiActions.Where(aiAction =>
                    createDto.AiActions.Select(t => t.Id).Contains(aiAction.Id)
                )
                .ToListAsync();
        }

        if (createDto.UserControls != null)
        {
            node.UserControls = await _context
                .UserControls.Where(userControl =>
                    createDto.UserControls.Select(t => t.Id).Contains(userControl.Id)
                )
                .ToListAsync();
        }

        _context.Nodes.Add(node);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<NodeDbModel>(node.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Node
    /// </summary>
    public async Task DeleteNode(NodeWhereUniqueInput uniqueId)
    {
        var node = await _context.Nodes.FindAsync(uniqueId.Id);
        if (node == null)
        {
            throw new NotFoundException();
        }

        _context.Nodes.Remove(node);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Nodes
    /// </summary>
    public async Task<List<Node>> Nodes(NodeFindManyArgs findManyArgs)
    {
        var nodes = await _context
            .Nodes.Include(x => x.UserControls)
            .Include(x => x.AiActions)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return nodes.ConvertAll(node => node.ToDto());
    }

    /// <summary>
    /// Meta data about Node records
    /// </summary>
    public async Task<MetadataDto> NodesMeta(NodeFindManyArgs findManyArgs)
    {
        var count = await _context.Nodes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Node
    /// </summary>
    public async Task<Node> Node(NodeWhereUniqueInput uniqueId)
    {
        var nodes = await this.Nodes(
            new NodeFindManyArgs { Where = new NodeWhereInput { Id = uniqueId.Id } }
        );
        var node = nodes.FirstOrDefault();
        if (node == null)
        {
            throw new NotFoundException();
        }

        return node;
    }

    /// <summary>
    /// Update one Node
    /// </summary>
    public async Task UpdateNode(NodeWhereUniqueInput uniqueId, NodeUpdateInput updateDto)
    {
        var node = updateDto.ToModel(uniqueId);

        if (updateDto.AiActions != null)
        {
            node.AiActions = await _context
                .AiActions.Where(aiAction =>
                    updateDto.AiActions.Select(t => t).Contains(aiAction.Id)
                )
                .ToListAsync();
        }

        if (updateDto.UserControls != null)
        {
            node.UserControls = await _context
                .UserControls.Where(userControl =>
                    updateDto.UserControls.Select(t => t).Contains(userControl.Id)
                )
                .ToListAsync();
        }

        _context.Entry(node).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Nodes.Any(e => e.Id == node.Id))
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
    /// Connect multiple AIActions records to Node
    /// </summary>
    public async Task ConnectAiActions(
        NodeWhereUniqueInput uniqueId,
        AiActionWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Nodes.Include(x => x.AiActions)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .AiActions.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.AiActions);

        foreach (var child in childrenToConnect)
        {
            parent.AiActions.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple AIActions records from Node
    /// </summary>
    public async Task DisconnectAiActions(
        NodeWhereUniqueInput uniqueId,
        AiActionWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Nodes.Include(x => x.AiActions)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .AiActions.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.AiActions?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple AIActions records for Node
    /// </summary>
    public async Task<List<AiAction>> FindAiActions(
        NodeWhereUniqueInput uniqueId,
        AiActionFindManyArgs nodeFindManyArgs
    )
    {
        var aiActions = await _context
            .AiActions.Where(m => m.NodeId == uniqueId.Id)
            .ApplyWhere(nodeFindManyArgs.Where)
            .ApplySkip(nodeFindManyArgs.Skip)
            .ApplyTake(nodeFindManyArgs.Take)
            .ApplyOrderBy(nodeFindManyArgs.SortBy)
            .ToListAsync();

        return aiActions.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple AIActions records for Node
    /// </summary>
    public async Task UpdateAiActions(
        NodeWhereUniqueInput uniqueId,
        AiActionWhereUniqueInput[] childrenIds
    )
    {
        var node = await _context
            .Nodes.Include(t => t.AiActions)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (node == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .AiActions.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        node.AiActions = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple UserControls records to Node
    /// </summary>
    public async Task ConnectUserControls(
        NodeWhereUniqueInput uniqueId,
        UserControlWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Nodes.Include(x => x.UserControls)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .UserControls.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.UserControls);

        foreach (var child in childrenToConnect)
        {
            parent.UserControls.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple UserControls records from Node
    /// </summary>
    public async Task DisconnectUserControls(
        NodeWhereUniqueInput uniqueId,
        UserControlWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Nodes.Include(x => x.UserControls)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .UserControls.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.UserControls?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple UserControls records for Node
    /// </summary>
    public async Task<List<UserControl>> FindUserControls(
        NodeWhereUniqueInput uniqueId,
        UserControlFindManyArgs nodeFindManyArgs
    )
    {
        var userControls = await _context
            .UserControls.Where(m => m.NodeId == uniqueId.Id)
            .ApplyWhere(nodeFindManyArgs.Where)
            .ApplySkip(nodeFindManyArgs.Skip)
            .ApplyTake(nodeFindManyArgs.Take)
            .ApplyOrderBy(nodeFindManyArgs.SortBy)
            .ToListAsync();

        return userControls.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple UserControls records for Node
    /// </summary>
    public async Task UpdateUserControls(
        NodeWhereUniqueInput uniqueId,
        UserControlWhereUniqueInput[] childrenIds
    )
    {
        var node = await _context
            .Nodes.Include(t => t.UserControls)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (node == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .UserControls.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        node.UserControls = children;
        await _context.SaveChangesAsync();
    }
}
