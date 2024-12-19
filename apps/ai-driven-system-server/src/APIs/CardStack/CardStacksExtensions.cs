using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.Infrastructure.Models;

namespace AiDrivenSystem.APIs.Extensions;

public static class CardStacksExtensions
{
    public static CardStack ToDto(this CardStackDbModel model)
    {
        return new CardStack
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CardStackDbModel ToModel(
        this CardStackUpdateInput updateDto,
        CardStackWhereUniqueInput uniqueId
    )
    {
        var cardStack = new CardStackDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            cardStack.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            cardStack.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return cardStack;
    }
}
