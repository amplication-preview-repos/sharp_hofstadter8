using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.Infrastructure.Models;

namespace AiDrivenSystem.APIs.Extensions;

public static class NodesExtensions
{
    public static Node ToDto(this NodeDbModel model)
    {
        return new Node
        {
            AiActions = model.AiActions?.Select(x => x.Id).ToList(),
            AiResponse = model.AiResponse,
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Status = model.Status,
            Title = model.Title,
            TypeField = model.TypeField,
            UpdatedAt = model.UpdatedAt,
            UserControls = model.UserControls?.Select(x => x.Id).ToList(),
        };
    }

    public static NodeDbModel ToModel(this NodeUpdateInput updateDto, NodeWhereUniqueInput uniqueId)
    {
        var node = new NodeDbModel
        {
            Id = uniqueId.Id,
            AiResponse = updateDto.AiResponse,
            Description = updateDto.Description,
            Status = updateDto.Status,
            Title = updateDto.Title,
            TypeField = updateDto.TypeField
        };

        if (updateDto.CreatedAt != null)
        {
            node.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            node.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return node;
    }
}
