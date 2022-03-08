namespace MemorySystem.Controllers.Infrastructure.Extentions
{
    using System.Security.Claims;

    public static class IdentityExtentions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
            => principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
