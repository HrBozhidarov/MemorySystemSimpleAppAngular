namespace MemorySystemApp.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MemorySystemApp.Infrastructures;
    using MemorySystemApp.Models.Pictures;
    using MemorySystemApp.Services;
    using MemorySystemApp.Services.Identity;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class UsersController : ApiController
    {
        private readonly IPicturesService picturesService;

        public UsersController(IPicturesService picturesService)
        {
            this.picturesService = picturesService;
        }

        // public ActionResult Prifile(string userId)
        // {
        // }
        [HttpGet]
        [Route(nameof(MyMemories))]
        public async Task<ActionResult<Result<IEnumerable<PictureModel>>>> MyMemories(string category)
        {
            var pictures = await this.picturesService.GetOwnPictures(this.User.GetUserId(), category);
            if (pictures.IfHaveError)
            {
                return this.NotFound(pictures);
            }

            return pictures;
        }
    }
}
