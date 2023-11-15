using System.Security.Claims;

namespace webapi.core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetId(this ClaimsPrincipal user) =>
            int.Parse(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
    }
}
