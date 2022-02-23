namespace MemorySystemApp.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MemorySystemApp.Infrastructures;
    using MemorySystemApp.Models.Pictures;
    using MemorySystemApp.Models.Users;
    using MemorySystemApp.Services;
    using MemorySystemApp.Services.Identity;
    using MemorySystemApp.Services.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : ApiController
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
        public async Task<ActionResult> Register(RegisterUserRequestModel model)
        {
            var result = await this.userService.Register(model);
            if (result.IfHaveError)
            {
                return this.BadRequest(result.ErrorMessage);
            }

            return this.Ok();
        }

        [Authorize]
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
