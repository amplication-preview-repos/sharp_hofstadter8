using AiDrivenSystem.APIs;
using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AiDrivenSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class EdgesControllerBase : ControllerBase
{
    protected readonly IEdgesService _service;

    public EdgesControllerBase(IEdgesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Edge
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Edge>> CreateEdge(EdgeCreateInput input)
    {
        var edge = await _service.CreateEdge(input);

        return CreatedAtAction(nameof(Edge), new { id = edge.Id }, edge);
    }

    /// <summary>
    /// Delete one Edge
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteEdge([FromRoute()] EdgeWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteEdge(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Edges
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Edge>>> Edges([FromQuery()] EdgeFindManyArgs filter)
    {
        return Ok(await _service.Edges(filter));
    }

    /// <summary>
    /// Meta data about Edge records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> EdgesMeta([FromQuery()] EdgeFindManyArgs filter)
    {
        return Ok(await _service.EdgesMeta(filter));
    }

    /// <summary>
    /// Get one Edge
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Edge>> Edge([FromRoute()] EdgeWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Edge(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Edge
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateEdge(
        [FromRoute()] EdgeWhereUniqueInput uniqueId,
        [FromQuery()] EdgeUpdateInput edgeUpdateDto
    )
    {
        try
        {
            await _service.UpdateEdge(uniqueId, edgeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
