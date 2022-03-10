namespace MemorySystem.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MemorySystem.Common.Infrastructure.AutomapperSettings;
    using MemorySystem.Controllers.Infrastructure.Extentions;
    using MemorySystem.Controllers.Models.Input;
    using MemorySystem.Controllers.Models.Output;
    using MemorySystem.Services;
    using MemorySystem.Services.Models;
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

        [AllowAnonymous]
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(CreateUserModel model)
            => this.ResponseResult(await this.userService.CreateAsync(Mapper.Map<UserModel>(model)));

        [HttpPost(nameof(Update))]
        public async Task<IActionResult> Update(UpdateUserModel model) =>
            this.ResponseResult(await this.userService.UpdateAsync(this.User.GetUserId(), Mapper.Map<UserModel>(model)));

        [HttpGet(nameof(Details))]
        public async Task<IActionResult> Details() => this.ResponseResult<UserModel, UserProfileResponseModel>(
            await this.userService.DetailsAsync(this.User.GetUserId()));
    }
}
