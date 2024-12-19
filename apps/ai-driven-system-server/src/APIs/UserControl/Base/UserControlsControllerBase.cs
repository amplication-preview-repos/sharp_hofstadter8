using AiDrivenSystem.APIs;
using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AiDrivenSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class UserControlsControllerBase : ControllerBase
{
    protected readonly IUserControlsService _service;

    public UserControlsControllerBase(IUserControlsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one UserControl
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<UserControl>> CreateUserControl(UserControlCreateInput input)
    {
        var userControl = await _service.CreateUserControl(input);

        return CreatedAtAction(nameof(UserControl), new { id = userControl.Id }, userControl);
    }

    /// <summary>
    /// Delete one UserControl
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteUserControl(
        [FromRoute()] UserControlWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteUserControl(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many UserControls
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<UserControl>>> UserControls(
        [FromQuery()] UserControlFindManyArgs filter
    )
    {
        return Ok(await _service.UserControls(filter));
    }

    /// <summary>
    /// Meta data about UserControl records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UserControlsMeta(
        [FromQuery()] UserControlFindManyArgs filter
    )
    {
        return Ok(await _service.UserControlsMeta(filter));
    }

    /// <summary>
    /// Get one UserControl
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<UserControl>> UserControl(
        [FromRoute()] UserControlWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.UserControl(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one UserControl
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateUserControl(
        [FromRoute()] UserControlWhereUniqueInput uniqueId,
        [FromQuery()] UserControlUpdateInput userControlUpdateDto
    )
    {
        try
        {
            await _service.UpdateUserControl(uniqueId, userControlUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Node record for UserControl
    /// </summary>
    [HttpGet("{Id}/node")]
    public async Task<ActionResult<List<Node>>> GetNode(
        [FromRoute()] UserControlWhereUniqueInput uniqueId
    )
    {
        var node = await _service.GetNode(uniqueId);
        return Ok(node);
    }
}
