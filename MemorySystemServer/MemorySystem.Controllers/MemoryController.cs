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

    // [Authorize]
    public class MemoryController : BaseResponseController
    {
        private readonly IMemoryService memoryService;

        public MemoryController(IMemoryService memoryService)
        {
            this.memoryService = memoryService;
        }

        [HttpPost]
        [Route(nameof(Create))]
        [Authorize]
        public async Task<IActionResult> Create(Models.Input.CreateMemoryModel model)
            => this.ResponseResult(await this.memoryService.Create(
                Mapper.Map<Services.Models.CreateMemoryModel>(model), this.User.GetUserId()));

        [HttpPost]
        [Route(nameof(Like))]
        [Authorize]
        public async Task<IActionResult> Like(int id)
            => this.ResponseResult<int, int>(await this.memoryService.LikeAsync(id, this.User.GetUserId()));

        // [Authorize]
        [HttpGet]
        [Route(nameof(Details))]
        public async Task<IActionResult> Details(int id)
        {
            await this.memoryService.Test();
            return this.Ok();
        }

        [HttpGet]
        [Route(nameof(UserMemories))]
        public async Task<IActionResult> UserMemories(string category)
            => this.ResponseResult<IEnumerable<MemoryModel>, IEnumerable<MemoryResponseModel>>(
                await this.memoryService.UserMemories(this.User.GetUserId(), category));
    }
}
