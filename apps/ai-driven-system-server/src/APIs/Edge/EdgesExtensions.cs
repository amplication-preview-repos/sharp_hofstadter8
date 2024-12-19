using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.Infrastructure.Models;

namespace AiDrivenSystem.APIs.Extensions;

public static class EdgesExtensions
{
    public static Edge ToDto(this EdgeDbModel model)
    {
        return new Edge
        {
            CreatedAt = model.CreatedAt,
            FromNode = model.FromNode,
            Id = model.Id,
            IsOrthogonal = model.IsOrthogonal,
            ToNode = model.ToNode,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static EdgeDbModel ToModel(this EdgeUpdateInput updateDto, EdgeWhereUniqueInput uniqueId)
    {
        var edge = new EdgeDbModel
        {
            Id = uniqueId.Id,
            FromNode = updateDto.FromNode,
            IsOrthogonal = updateDto.IsOrthogonal,
            ToNode = updateDto.ToNode
        };

        if (updateDto.CreatedAt != null)
        {
            edge.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            edge.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return edge;
    }
}
