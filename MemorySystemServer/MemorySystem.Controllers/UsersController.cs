namespace MemorySystem.Controllers
{
    using System.Threading.Tasks;
    using MemorySystem.Common.Infrastructure.AutomapperSettings;
    using MemorySystem.Controllers.Infrastructure.Extentions;
    using MemorySystem.Services;
    using MemorySystem.Services.Models;
    using MemorySystemApp.Models.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseResponseController
    {
        private readonly IUserService userService;
        private readonly IPicturesService picturesService;

        public UsersController(IPicturesService picturesService, IUserService userService)
        {
            this.picturesService = picturesService;
            this.userService = userService;
        }

        public async Task<IActionResult> EditProfile()
        {
            this.User.GetUserId();

            return Ok();
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(RegisterUserRequestModel model)
            => this.ResponseResult(await this.userService.Register(Mapper.Map<RegisterUserModel>(model)));

        [Authorize]
        [HttpGet]
        [Route(nameof(MyMemories))]
        public async Task<IActionResult> MyMemories(string category)
            => this.ResponseResult(await this.picturesService.GetOwnPictures(this.User.GetUserId(), category));
    }
}
