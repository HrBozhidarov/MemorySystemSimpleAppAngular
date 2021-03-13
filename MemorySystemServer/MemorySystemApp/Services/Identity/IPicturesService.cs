namespace MemorySystemApp.Services.Identity
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MemorySystemApp.Models.pictures;

    public interface IPicturesService
    {
        bool Create(PictureRequestModel model, string userId);

        Task<Result<IEnumerable<PictureModel>>> GetOwnPictures(string userId, string category);

        Task<Result<IEnumerable<PictureModel>>> GetUserPictures(string currentUserId, string userId);

        Task<Result<int>> LikeAsync(int id, string userId);
    }
}
