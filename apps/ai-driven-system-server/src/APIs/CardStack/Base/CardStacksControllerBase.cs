using AiDrivenSystem.APIs;
using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AiDrivenSystem.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CardStacksControllerBase : ControllerBase
{
    protected readonly ICardStacksService _service;

    public CardStacksControllerBase(ICardStacksService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one CardStack
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<CardStack>> CreateCardStack(CardStackCreateInput input)
    {
        var cardStack = await _service.CreateCardStack(input);

        return CreatedAtAction(nameof(CardStack), new { id = cardStack.Id }, cardStack);
    }

    /// <summary>
    /// Delete one CardStack
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCardStack(
        [FromRoute()] CardStackWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteCardStack(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many CardStacks
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<CardStack>>> CardStacks(
        [FromQuery()] CardStackFindManyArgs filter
    )
    {
        return Ok(await _service.CardStacks(filter));
    }

    /// <summary>
    /// Meta data about CardStack records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CardStacksMeta(
        [FromQuery()] CardStackFindManyArgs filter
    )
    {
        return Ok(await _service.CardStacksMeta(filter));
    }

    /// <summary>
    /// Get one CardStack
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<CardStack>> CardStack(
        [FromRoute()] CardStackWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.CardStack(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one CardStack
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCardStack(
        [FromRoute()] CardStackWhereUniqueInput uniqueId,
        [FromQuery()] CardStackUpdateInput cardStackUpdateDto
    )
    {
        try
        {
            await _service.UpdateCardStack(uniqueId, cardStackUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
