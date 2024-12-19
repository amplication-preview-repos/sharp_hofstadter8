using AiDrivenSystem.APIs;
using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AiDrivenSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AiActionsControllerBase : ControllerBase
{
    protected readonly IAiActionsService _service;

    public AiActionsControllerBase(IAiActionsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one AIAction
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<AiAction>> CreateAiAction(AiActionCreateInput input)
    {
        var aiAction = await _service.CreateAiAction(input);

        return CreatedAtAction(nameof(AiAction), new { id = aiAction.Id }, aiAction);
    }

    /// <summary>
    /// Delete one AIAction
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteAiAction([FromRoute()] AiActionWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteAiAction(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many AIActions
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<AiAction>>> AiActions(
        [FromQuery()] AiActionFindManyArgs filter
    )
    {
        return Ok(await _service.AiActions(filter));
    }

    /// <summary>
    /// Meta data about AIAction records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AiActionsMeta(
        [FromQuery()] AiActionFindManyArgs filter
    )
    {
        return Ok(await _service.AiActionsMeta(filter));
    }

    /// <summary>
    /// Get one AIAction
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<AiAction>> AiAction(
        [FromRoute()] AiActionWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.AiAction(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one AIAction
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateAiAction(
        [FromRoute()] AiActionWhereUniqueInput uniqueId,
        [FromQuery()] AiActionUpdateInput aiActionUpdateDto
    )
    {
        try
        {
            await _service.UpdateAiAction(uniqueId, aiActionUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Node record for AIAction
    /// </summary>
    [HttpGet("{Id}/node")]
    public async Task<ActionResult<List<Node>>> GetNode(
        [FromRoute()] AiActionWhereUniqueInput uniqueId
    )
    {
        var node = await _service.GetNode(uniqueId);
        return Ok(node);
    }
}
