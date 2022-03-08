namespace MemorySystem.Controllers
{
    using System.Threading.Tasks;
    using MemorySystem.Common.Infrastructure.AutomapperSettings;
    using MemorySystem.Controllers.Models.Output;
    using MemorySystem.Services;
    using MemorySystem.Services.Models;
    using MemorySystemApp.Models.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : BaseResponseController
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login(LoginUserRequestModel model)
            => this.ResponseResult<LoginModel, LogedUserDataModel>(await this.accountService.Login(Mapper.Map<BaseUserModel>(model)));
    }
}
