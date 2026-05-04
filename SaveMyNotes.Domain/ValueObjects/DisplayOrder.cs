using SaveMyNotes.Domain.Exceptions;

namespace SaveMyNotes.Domain.ValueObjects;

public record DisplayOrder
{
    public int Order {  get; init; }
    public DisplayOrder(int order)
    {
        if (order < 0)
        {
            throw new NoteValidationException("Order value cannot be negative");
        }
        Order = order;
    }
}
