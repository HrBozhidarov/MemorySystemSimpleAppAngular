namespace MemorySystemApp.Controllers
{
    using System.Threading.Tasks;

    using MemorySystemApp.Infrastructures;
    using MemorySystemApp.Models.pictures;
    using MemorySystemApp.Services;
    using MemorySystemApp.Services.Identity;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
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
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        [Route(nameof(Like))]
        [Authorize]
        public async Task<ActionResult<Result<bool>>> Like(int id)
        {
            var result = await this.picturesService.LikeAsync(id, this.User.GetUserId());
            if (result.IfHaveError)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route(nameof(Details))]
        [Authorize]
        public ActionResult Details(int id)
        {

            return null;
        }
    }
}
