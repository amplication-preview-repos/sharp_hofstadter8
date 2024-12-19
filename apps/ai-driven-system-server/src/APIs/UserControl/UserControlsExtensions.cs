using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.Infrastructure.Models;

namespace AiDrivenSystem.APIs.Extensions;

public static class UserControlsExtensions
{
    public static UserControl ToDto(this UserControlDbModel model)
    {
        return new UserControl
        {
            ControlType = model.ControlType,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Node = model.NodeId,
            Parameters = model.Parameters,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static UserControlDbModel ToModel(
        this UserControlUpdateInput updateDto,
        UserControlWhereUniqueInput uniqueId
    )
    {
        var userControl = new UserControlDbModel
        {
            Id = uniqueId.Id,
            ControlType = updateDto.ControlType,
            Parameters = updateDto.Parameters
        };

        if (updateDto.CreatedAt != null)
        {
            userControl.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Node != null)
        {
            userControl.NodeId = updateDto.Node;
        }
        if (updateDto.UpdatedAt != null)
        {
            userControl.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return userControl;
    }
}
