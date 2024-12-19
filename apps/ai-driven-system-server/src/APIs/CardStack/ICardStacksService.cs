using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;

namespace AiDrivenSystem.APIs;

public interface ICardStacksService
{
    /// <summary>
    /// Create one CardStack
    /// </summary>
    public Task<CardStack> CreateCardStack(CardStackCreateInput cardstack);

    /// <summary>
    /// Delete one CardStack
    /// </summary>
    public Task DeleteCardStack(CardStackWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many CardStacks
    /// </summary>
    public Task<List<CardStack>> CardStacks(CardStackFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about CardStack records
    /// </summary>
    public Task<MetadataDto> CardStacksMeta(CardStackFindManyArgs findManyArgs);

    /// <summary>
    /// Get one CardStack
    /// </summary>
    public Task<CardStack> CardStack(CardStackWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one CardStack
    /// </summary>
    public Task UpdateCardStack(CardStackWhereUniqueInput uniqueId, CardStackUpdateInput updateDto);
}
