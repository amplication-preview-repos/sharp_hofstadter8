using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;

namespace AiDrivenSystem.APIs;

public interface IEdgesService
{
    /// <summary>
    /// Create one Edge
    /// </summary>
    public Task<Edge> CreateEdge(EdgeCreateInput edge);

    /// <summary>
    /// Delete one Edge
    /// </summary>
    public Task DeleteEdge(EdgeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Edges
    /// </summary>
    public Task<List<Edge>> Edges(EdgeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Edge records
    /// </summary>
    public Task<MetadataDto> EdgesMeta(EdgeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Edge
    /// </summary>
    public Task<Edge> Edge(EdgeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Edge
    /// </summary>
    public Task UpdateEdge(EdgeWhereUniqueInput uniqueId, EdgeUpdateInput updateDto);
}
