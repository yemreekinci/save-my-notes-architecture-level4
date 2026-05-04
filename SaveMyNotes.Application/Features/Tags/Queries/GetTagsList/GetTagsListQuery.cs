using MediatR;
using SaveMyNotes.Application.DTOs;

namespace SaveMyNotes.Application.Features.Tags.Queries.GetTagsList;

public record GetTagsListQuery() : IRequest<IEnumerable<TagDTO>>;
