using BoundVerse.Application.Abstractions.Interfaces;
using System.Security.Claims;

namespace BoundVerse.Api.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string? UserName => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
