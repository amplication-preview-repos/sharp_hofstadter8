using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;

namespace AiDrivenSystem.APIs;

public interface IUserControlsService
{
    /// <summary>
    /// Create one UserControl
    /// </summary>
    public Task<UserControl> CreateUserControl(UserControlCreateInput usercontrol);

    /// <summary>
    /// Delete one UserControl
    /// </summary>
    public Task DeleteUserControl(UserControlWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many UserControls
    /// </summary>
    public Task<List<UserControl>> UserControls(UserControlFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about UserControl records
    /// </summary>
    public Task<MetadataDto> UserControlsMeta(UserControlFindManyArgs findManyArgs);

    /// <summary>
    /// Get one UserControl
    /// </summary>
    public Task<UserControl> UserControl(UserControlWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one UserControl
    /// </summary>
    public Task UpdateUserControl(
        UserControlWhereUniqueInput uniqueId,
        UserControlUpdateInput updateDto
    );

    /// <summary>
    /// Get a Node record for UserControl
    /// </summary>
    public Task<Node> GetNode(UserControlWhereUniqueInput uniqueId);
}
