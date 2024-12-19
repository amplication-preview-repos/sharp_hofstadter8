using AiDrivenSystem.APIs;
using AiDrivenSystem.APIs.Common;
using AiDrivenSystem.APIs.Dtos;
using AiDrivenSystem.APIs.Errors;
using AiDrivenSystem.APIs.Extensions;
using AiDrivenSystem.Infrastructure;
using AiDrivenSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AiDrivenSystem.APIs;

public abstract class CardStacksServiceBase : ICardStacksService
{
    protected readonly AiDrivenSystemDbContext _context;

    public CardStacksServiceBase(AiDrivenSystemDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one CardStack
    /// </summary>
    public async Task<CardStack> CreateCardStack(CardStackCreateInput createDto)
    {
        var cardStack = new CardStackDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            cardStack.Id = createDto.Id;
        }

        _context.CardStacks.Add(cardStack);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CardStackDbModel>(cardStack.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one CardStack
    /// </summary>
    public async Task DeleteCardStack(CardStackWhereUniqueInput uniqueId)
    {
        var cardStack = await _context.CardStacks.FindAsync(uniqueId.Id);
        if (cardStack == null)
        {
            throw new NotFoundException();
        }

        _context.CardStacks.Remove(cardStack);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many CardStacks
    /// </summary>
    public async Task<List<CardStack>> CardStacks(CardStackFindManyArgs findManyArgs)
    {
        var cardStacks = await _context
            .CardStacks.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return cardStacks.ConvertAll(cardStack => cardStack.ToDto());
    }

    /// <summary>
    /// Meta data about CardStack records
    /// </summary>
    public async Task<MetadataDto> CardStacksMeta(CardStackFindManyArgs findManyArgs)
    {
        var count = await _context.CardStacks.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one CardStack
    /// </summary>
    public async Task<CardStack> CardStack(CardStackWhereUniqueInput uniqueId)
    {
        var cardStacks = await this.CardStacks(
            new CardStackFindManyArgs { Where = new CardStackWhereInput { Id = uniqueId.Id } }
        );
        var cardStack = cardStacks.FirstOrDefault();
        if (cardStack == null)
        {
            throw new NotFoundException();
        }

        return cardStack;
    }

    /// <summary>
    /// Update one CardStack
    /// </summary>
    public async Task UpdateCardStack(
        CardStackWhereUniqueInput uniqueId,
        CardStackUpdateInput updateDto
    )
    {
        var cardStack = updateDto.ToModel(uniqueId);

        _context.Entry(cardStack).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.CardStacks.Any(e => e.Id == cardStack.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
