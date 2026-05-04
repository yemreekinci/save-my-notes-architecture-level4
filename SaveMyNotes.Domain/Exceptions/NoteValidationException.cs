namespace SaveMyNotes.Domain.Exceptions;

public class NoteValidationException : Exception
{
    public NoteValidationException(string message) : base(message) { }
}
