using MediatR;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Application.Common.Mappings;
using SaveMyNotes.Application.DTOs;

namespace SaveMyNotes.Application.Features.Tags.Queries.GetTagsList;

public class GetTagsListHandler : IRequestHandler<GetTagsListQuery, IEnumerable<TagDTO>>
{
    private readonly ITagRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public GetTagsListHandler(ITagRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<TagDTO>> Handle(GetTagsListQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_currentUserService.UserId ?? throw new Exception("Kullanıcı bulunamadı."));
        var tags = await _repository.GetAllAsync(userId);
        return tags.Select(t => t.MapToDto());
    }
}
