namespace MemorySystem.Controllers
{
    using System.Threading.Tasks;
    using MemorySystem.Common.Infrastructure.AutomapperSettings;
    using MemorySystem.Services;
    using MemorySystem.Services.Models;
    using MemorySystemApp.Models.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : ResponseController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login(LoginUserRequestModel model)
            => this.ResponseResult(await this.accountService.Login(Mapper.Map<LoginUserModel>(model)));
    }
}
