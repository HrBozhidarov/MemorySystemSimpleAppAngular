namespace MemorySystemApp.Controllers
{
    using System.Threading.Tasks;

    using MemorySystemApp.Infrastructures;
    using MemorySystemApp.Models.Pictures;
    using MemorySystemApp.Services;
    using MemorySystemApp.Services.Identity;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    // [Authorize]
    public class PicturesController : ApiController
    {
        private readonly IPicturesService picturesService;

        public PicturesController(IPicturesService picturesService)
        {
            this.picturesService = picturesService;
        }

        [HttpPost]
        [Route(nameof(Create))]
        [Authorize]
        public ActionResult Create(PictureRequestModel model)
        {
            var userId = this.User.GetUserId();

            var isCreated = this.picturesService.Create(model, userId);
            if (!isCreated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpPost]
        [Route(nameof(Like))]
        [Authorize]
        public async Task<ActionResult<Result<bool>>> Like(int id)
        {
            var result = await this.picturesService.LikeAsync(id, this.User.GetUserId());
            if (result.IfHaveError)
            {
                return this.BadRequest(result);
            }

            return this.Ok(result);
        }

        // [Authorize]
        [HttpGet]
        [Route(nameof(Details))]
        public async Task<ActionResult> Details(int id)
        {
            await this.picturesService.Test();
            return this.Ok();
        }
    }
}
