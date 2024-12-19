using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.Infrastructure.Models;

namespace AiDrivenSystem.APIs.Extensions;

public static class AiActionsExtensions
{
    public static AiAction ToDto(this AiActionDbModel model)
    {
        return new AiAction
        {
            ActionName = model.ActionName,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Node = model.NodeId,
            Output = model.Output,
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AiActionDbModel ToModel(
        this AiActionUpdateInput updateDto,
        AiActionWhereUniqueInput uniqueId
    )
    {
        var aiAction = new AiActionDbModel
        {
            Id = uniqueId.Id,
            ActionName = updateDto.ActionName,
            Output = updateDto.Output,
            Status = updateDto.Status
        };

        if (updateDto.CreatedAt != null)
        {
            aiAction.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Node != null)
        {
            aiAction.NodeId = updateDto.Node;
        }
        if (updateDto.UpdatedAt != null)
        {
            aiAction.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return aiAction;
    }
}
