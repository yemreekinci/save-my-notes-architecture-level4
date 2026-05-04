namespace SaveMyNotes.Application.Common.Interfaces;

public interface IAIService
{
    Task<List<string>> ExtractTagsAsync(string content);
}