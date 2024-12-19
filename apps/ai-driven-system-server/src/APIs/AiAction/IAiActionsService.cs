using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;

namespace AiDrivenSystem.APIs;

public interface IAiActionsService
{
    /// <summary>
    /// Create one AIAction
    /// </summary>
    public Task<AiAction> CreateAiAction(AiActionCreateInput aiaction);

    /// <summary>
    /// Delete one AIAction
    /// </summary>
    public Task DeleteAiAction(AiActionWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many AIActions
    /// </summary>
    public Task<List<AiAction>> AiActions(AiActionFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about AIAction records
    /// </summary>
    public Task<MetadataDto> AiActionsMeta(AiActionFindManyArgs findManyArgs);

    /// <summary>
    /// Get one AIAction
    /// </summary>
    public Task<AiAction> AiAction(AiActionWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one AIAction
    /// </summary>
    public Task UpdateAiAction(AiActionWhereUniqueInput uniqueId, AiActionUpdateInput updateDto);

    /// <summary>
    /// Get a Node record for AIAction
    /// </summary>
    public Task<Node> GetNode(AiActionWhereUniqueInput uniqueId);
}
