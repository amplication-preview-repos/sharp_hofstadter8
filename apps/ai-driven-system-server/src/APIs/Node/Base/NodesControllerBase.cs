using AiDrivenSystem.APIs;
using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AiDrivenSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class NodesControllerBase : ControllerBase
{
    protected readonly INodesService _service;

    public NodesControllerBase(INodesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Node
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Node>> CreateNode(NodeCreateInput input)
    {
        var node = await _service.CreateNode(input);

        return CreatedAtAction(nameof(Node), new { id = node.Id }, node);
    }

    /// <summary>
    /// Delete one Node
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteNode([FromRoute()] NodeWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteNode(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Nodes
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Node>>> Nodes([FromQuery()] NodeFindManyArgs filter)
    {
        return Ok(await _service.Nodes(filter));
    }

    /// <summary>
    /// Meta data about Node records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> NodesMeta([FromQuery()] NodeFindManyArgs filter)
    {
        return Ok(await _service.NodesMeta(filter));
    }

    /// <summary>
    /// Get one Node
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Node>> Node([FromRoute()] NodeWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Node(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Node
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateNode(
        [FromRoute()] NodeWhereUniqueInput uniqueId,
        [FromQuery()] NodeUpdateInput nodeUpdateDto
    )
    {
        try
        {
            await _service.UpdateNode(uniqueId, nodeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple AIActions records to Node
    /// </summary>
    [HttpPost("{Id}/aiActions")]
    public async Task<ActionResult> ConnectAiActions(
        [FromRoute()] NodeWhereUniqueInput uniqueId,
        [FromQuery()] AiActionWhereUniqueInput[] aiActionsId
    )
    {
        try
        {
            await _service.ConnectAiActions(uniqueId, aiActionsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple AIActions records from Node
    /// </summary>
    [HttpDelete("{Id}/aiActions")]
    public async Task<ActionResult> DisconnectAiActions(
        [FromRoute()] NodeWhereUniqueInput uniqueId,
        [FromBody()] AiActionWhereUniqueInput[] aiActionsId
    )
    {
        try
        {
            await _service.DisconnectAiActions(uniqueId, aiActionsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple AIActions records for Node
    /// </summary>
    [HttpGet("{Id}/aiActions")]
    public async Task<ActionResult<List<AiAction>>> FindAiActions(
        [FromRoute()] NodeWhereUniqueInput uniqueId,
        [FromQuery()] AiActionFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindAiActions(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple AIActions records for Node
    /// </summary>
    [HttpPatch("{Id}/aiActions")]
    public async Task<ActionResult> UpdateAiActions(
        [FromRoute()] NodeWhereUniqueInput uniqueId,
        [FromBody()] AiActionWhereUniqueInput[] aiActionsId
    )
    {
        try
        {
            await _service.UpdateAiActions(uniqueId, aiActionsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple UserControls records to Node
    /// </summary>
    [HttpPost("{Id}/userControls")]
    public async Task<ActionResult> ConnectUserControls(
        [FromRoute()] NodeWhereUniqueInput uniqueId,
        [FromQuery()] UserControlWhereUniqueInput[] userControlsId
    )
    {
        try
        {
            await _service.ConnectUserControls(uniqueId, userControlsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple UserControls records from Node
    /// </summary>
    [HttpDelete("{Id}/userControls")]
    public async Task<ActionResult> DisconnectUserControls(
        [FromRoute()] NodeWhereUniqueInput uniqueId,
        [FromBody()] UserControlWhereUniqueInput[] userControlsId
    )
    {
        try
        {
            await _service.DisconnectUserControls(uniqueId, userControlsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple UserControls records for Node
    /// </summary>
    [HttpGet("{Id}/userControls")]
    public async Task<ActionResult<List<UserControl>>> FindUserControls(
        [FromRoute()] NodeWhereUniqueInput uniqueId,
        [FromQuery()] UserControlFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindUserControls(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple UserControls records for Node
    /// </summary>
    [HttpPatch("{Id}/userControls")]
    public async Task<ActionResult> UpdateUserControls(
        [FromRoute()] NodeWhereUniqueInput uniqueId,
        [FromBody()] UserControlWhereUniqueInput[] userControlsId
    )
    {
        try
        {
            await _service.UpdateUserControls(uniqueId, userControlsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
