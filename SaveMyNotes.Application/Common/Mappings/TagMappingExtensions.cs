using SaveMyNotes.Application.DTOs;
using SaveMyNotes.Domain.Entities;

namespace SaveMyNotes.Application.Common.Mappings;

public static class TagMappingExtensions
{
    public static TagDTO MapToDto(this Tag tag)
    {
        return new TagDTO(
            Id: tag.Id,
            Name: tag.Name
        );
    }
}