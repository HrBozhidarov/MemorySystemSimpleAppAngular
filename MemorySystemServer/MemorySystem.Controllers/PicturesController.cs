namespace MemorySystem.Controllers
{
    using System.Threading.Tasks;
    using MemorySystem.Controllers.Infrastructure.Extentions;
    using MemorySystem.Services;
    using MemorySystemApp.Models.Pictures;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    // [Authorize]
    public class PicturesController : ResponseController
    {
        private readonly IPicturesService picturesService;

        public PicturesController(IPicturesService picturesService)
        {
            this.picturesService = picturesService;
        }

        [HttpPost]
        [Route(nameof(Create))]
        [Authorize]
        public async Task<IActionResult> Create(PictureRequestModel model)
            => this.ResponseResult(await this.picturesService.Create(model, this.User.GetUserId()));

        [HttpPost]
        [Route(nameof(Like))]
        [Authorize]
        public async Task<IActionResult> Like(int id)
            => this.ResponseResult(await this.picturesService.LikeAsync(id, this.User.GetUserId()));

        // [Authorize]
        [HttpGet]
        [Route(nameof(Details))]
        public async Task<IActionResult> Details(int id)
        {
            await this.picturesService.Test();
            return this.Ok();
        }
    }
}
