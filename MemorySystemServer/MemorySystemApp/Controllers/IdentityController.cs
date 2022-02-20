namespace MemorySystemApp.Controllers
{
    using System.Threading.Tasks;

    using MemorySystemApp.Models.Identity;
    using MemorySystemApp.Services.Identity;

    using Microsoft.AspNetCore.Mvc;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserRequestModel model)
        {
            var result = await this.identityService.Register(model);
            if (result.IfHaveError)
            {
                return this.BadRequest(result);
            }

            return this.Ok(result);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult> Login(LoginUserRequestModel model)
        {
            var result = await this.identityService.Login(model);
            if (result.IfHaveError)
            {
                return this.BadRequest(result);
            }

            return this.Ok(result);
        }
    }
}
