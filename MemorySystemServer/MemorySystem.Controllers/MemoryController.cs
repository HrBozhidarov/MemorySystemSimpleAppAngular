namespace MemorySystem.Controllers
{
    using System.Threading.Tasks;

    using MemorySystem.Controllers.Infrastructure.Extentions;
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
        public async Task<IActionResult> Create(MemoryRequestModel model)
            => this.ResponseResult(await this.memoryService.Create(model, this.User.GetUserId()));

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
    }
}
