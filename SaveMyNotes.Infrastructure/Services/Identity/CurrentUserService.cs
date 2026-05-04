using SaveMyNotes.Application.Common.Interfaces;

namespace SaveMyNotes.Infrastructure.Services.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        public string? UserId => "00000000-0000-0000-0000-000000000000";
    }
}
