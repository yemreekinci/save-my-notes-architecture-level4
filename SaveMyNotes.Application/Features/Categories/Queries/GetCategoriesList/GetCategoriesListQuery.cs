using MediatR;
using SaveMyNotes.Application.DTOs;

namespace SaveMyNotes.Application.Features.Categories.Queries.GetCategoriesList;

public record GetCategoriesListQuery() : IRequest<IEnumerable<CategoryDTO>>;
