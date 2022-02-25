using System.Security.Claims;

namespace MemorySystem.Controllers.Infrastructure.Extentions
{
    public static class IdentityExtentions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
            => principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
