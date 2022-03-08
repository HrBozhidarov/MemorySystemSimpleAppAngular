namespace MemorySystem.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MemorySystem.Common.Infrastructure.AutomapperSettings;
    using MemorySystem.Controllers.Infrastructure.Extentions;
    using MemorySystem.Controllers.Models.Output;
    using MemorySystem.Services;
    using MemorySystem.Services.Models;
    using MemorySystemApp.Models.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class UserController : BaseResponseController
    {
        private readonly IUserService userService;
        private readonly IMemoryService memoryService;

        public UserController(IMemoryService memoryService, IUserService userService)
        {
            this.memoryService = memoryService;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(RegisterUserModel model) =>
            this.ResponseResult(await this.userService.EditProfileAsync(this.User.GetUserId(), Mapper.Map<UserModel>(model)));

        [HttpGet]
        public async Task<IActionResult> Profile() => this.ResponseResult<UserModel, UserResponseModel>(
            await this.userService.ProfileAsync(this.User.GetUserId()));

        [AllowAnonymous]
        [HttpPost]
        [Route(nameof(CreateProfile))]
        public async Task<IActionResult> CreateProfile(RegisterUserModel model)
            => this.ResponseResult(await this.userService.CreateProfileAsync(Mapper.Map<UserModel>(model)));

        // CreateModels and see what will be the structure
        [HttpGet]
        [Route(nameof(MyMemories))]
        public async Task<IActionResult> MyMemories(string category)
            => this.ResponseResult<IEnumerable<MemoryModel>, IEnumerable<MyMemoryResponseModel>>(
                await this.memoryService.GetOwnMemories(this.User.GetUserId(), category));
    }
}
