using AiDrivenSystem.APIs;
using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.APIs.Errors;
using AiDrivenSystem.APIs.Extensions;
using AiDrivenSystem.Infrastructure;
using AiDrivenSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AiDrivenSystem.APIs;

public abstract class EdgesServiceBase : IEdgesService
{
    protected readonly AiDrivenSystemDbContext _context;

    public EdgesServiceBase(AiDrivenSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Edge
    /// </summary>
    public async Task<Edge> CreateEdge(EdgeCreateInput createDto)
    {
        var edge = new EdgeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            FromNode = createDto.FromNode,
            IsOrthogonal = createDto.IsOrthogonal,
            ToNode = createDto.ToNode,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            edge.Id = createDto.Id;
        }

        _context.Edges.Add(edge);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<EdgeDbModel>(edge.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Edge
    /// </summary>
    public async Task DeleteEdge(EdgeWhereUniqueInput uniqueId)
    {
        var edge = await _context.Edges.FindAsync(uniqueId.Id);
        if (edge == null)
        {
            throw new NotFoundException();
        }

        _context.Edges.Remove(edge);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Edges
    /// </summary>
    public async Task<List<Edge>> Edges(EdgeFindManyArgs findManyArgs)
    {
        var edges = await _context
            .Edges.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return edges.ConvertAll(edge => edge.ToDto());
    }

    /// <summary>
    /// Meta data about Edge records
    /// </summary>
    public async Task<MetadataDto> EdgesMeta(EdgeFindManyArgs findManyArgs)
    {
        var count = await _context.Edges.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Edge
    /// </summary>
    public async Task<Edge> Edge(EdgeWhereUniqueInput uniqueId)
    {
        var edges = await this.Edges(
            new EdgeFindManyArgs { Where = new EdgeWhereInput { Id = uniqueId.Id } }
        );
        var edge = edges.FirstOrDefault();
        if (edge == null)
        {
            throw new NotFoundException();
        }

        return edge;
    }

    /// <summary>
    /// Update one Edge
    /// </summary>
    public async Task UpdateEdge(EdgeWhereUniqueInput uniqueId, EdgeUpdateInput updateDto)
    {
        var edge = updateDto.ToModel(uniqueId);

        _context.Entry(edge).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Edges.Any(e => e.Id == edge.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
