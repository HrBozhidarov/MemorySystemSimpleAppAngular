using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MemorySystem.Controllers.Infrastructure.Extentions
{
    public static class IdentityExtentions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
            => principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
