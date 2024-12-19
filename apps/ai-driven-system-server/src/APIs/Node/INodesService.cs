using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;

namespace AiDrivenSystem.APIs;

public interface INodesService
{
    /// <summary>
    /// Create one Node
    /// </summary>
    public Task<Node> CreateNode(NodeCreateInput node);

    /// <summary>
    /// Delete one Node
    /// </summary>
    public Task DeleteNode(NodeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Nodes
    /// </summary>
    public Task<List<Node>> Nodes(NodeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Node records
    /// </summary>
    public Task<MetadataDto> NodesMeta(NodeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Node
    /// </summary>
    public Task<Node> Node(NodeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Node
    /// </summary>
    public Task UpdateNode(NodeWhereUniqueInput uniqueId, NodeUpdateInput updateDto);

    /// <summary>
    /// Connect multiple AIActions records to Node
    /// </summary>
    public Task ConnectAiActions(
        NodeWhereUniqueInput uniqueId,
        AiActionWhereUniqueInput[] aiActionsId
    );

    /// <summary>
    /// Disconnect multiple AIActions records from Node
    /// </summary>
    public Task DisconnectAiActions(
        NodeWhereUniqueInput uniqueId,
        AiActionWhereUniqueInput[] aiActionsId
    );

    /// <summary>
    /// Find multiple AIActions records for Node
    /// </summary>
    public Task<List<AiAction>> FindAiActions(
        NodeWhereUniqueInput uniqueId,
        AiActionFindManyArgs AiActionFindManyArgs
    );

    /// <summary>
    /// Update multiple AIActions records for Node
    /// </summary>
    public Task UpdateAiActions(
        NodeWhereUniqueInput uniqueId,
        AiActionWhereUniqueInput[] aiActionsId
    );

    /// <summary>
    /// Connect multiple UserControls records to Node
    /// </summary>
    public Task ConnectUserControls(
        NodeWhereUniqueInput uniqueId,
        UserControlWhereUniqueInput[] userControlsId
    );

    /// <summary>
    /// Disconnect multiple UserControls records from Node
    /// </summary>
    public Task DisconnectUserControls(
        NodeWhereUniqueInput uniqueId,
        UserControlWhereUniqueInput[] userControlsId
    );

    /// <summary>
    /// Find multiple UserControls records for Node
    /// </summary>
    public Task<List<UserControl>> FindUserControls(
        NodeWhereUniqueInput uniqueId,
        UserControlFindManyArgs UserControlFindManyArgs
    );

    /// <summary>
    /// Update multiple UserControls records for Node
    /// </summary>
    public Task UpdateUserControls(
        NodeWhereUniqueInput uniqueId,
        UserControlWhereUniqueInput[] userControlsId
    );
}
