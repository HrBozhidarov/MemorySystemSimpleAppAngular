namespace MemorySystem.Controllers
{
    using System.Threading.Tasks;
    using MemorySystem.Common.Infrastructure.AutomapperSettings;
    using MemorySystem.Services;
    using MemorySystem.Services.Models;
    using MemorySystemApp.Models.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class IdentityController : ResponseController
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login(LoginUserRequestModel model)
            => this.ResponseResult(await this.identityService.Login(Mapper.Map<LoginUserModel>(model)));
    }
}
