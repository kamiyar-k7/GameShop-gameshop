using Domain.entities.UserPart.User;
using System.Security.Claims;
using System.Security.Principal;

namespace Application.Helpers;

public static class UsersUtilities
{
    public static ulong GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal != null)
        {
            var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
            if (data != null) return (ulong)Convert.ToInt32(data?.Value);
            return default(long);
        }

        return default(long);
    }

    public static ulong GetUserId(this IPrincipal principal)
    {
        var user = (ClaimsPrincipal)principal;

        return user.GetUserId();
    }

    public static string GetFullName(this User user)
    {
        return user.UserName;
    }
}
