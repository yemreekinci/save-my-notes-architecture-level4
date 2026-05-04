using SaveMyNotes.Application.DTOs;
using SaveMyNotes.Domain.Entities;

namespace SaveMyNotes.Application.Common.Mappings;

public static class CategoryMappingExtensions
{
    public static CategoryDTO MapToDto(this Category category)
    {
        return new CategoryDTO(
            Id: category.Id,
            Name: category.Name,
            Color: category.Color.Color
        );
    }
}