using System.Security.Claims;

namespace API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetMemberId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst("Id")?.Value;

        if (string.IsNullOrEmpty(value))
            throw new UnauthorizedAccessException("Id claim missing");

        return Guid.Parse(value);
    }
}