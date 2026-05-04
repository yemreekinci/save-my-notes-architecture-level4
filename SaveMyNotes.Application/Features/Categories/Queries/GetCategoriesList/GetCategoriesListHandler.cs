using MediatR;
using SaveMyNotes.Application.Common.Interfaces;
using SaveMyNotes.Application.Common.Mappings;
using SaveMyNotes.Application.DTOs;

namespace SaveMyNotes.Application.Features.Categories.Queries.GetCategoriesList;

public class GetCategoriesListHandler : IRequestHandler<GetCategoriesListQuery, IEnumerable<CategoryDTO>>
{
    private readonly ICategoryRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public GetCategoriesListHandler(ICategoryRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<CategoryDTO>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_currentUserService.UserId ?? throw new Exception("Kullanıcı bulunamadı."));
        var categories = await _repository.GetAllAsync(userId);
        return categories.Select(c => c.MapToDto());
    }
}
