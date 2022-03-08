namespace MemorySystem.Services
{
    using System.Threading.Tasks;
    using MemorySystem.Services.Models;

    public interface IUserService
    {
        Task<Result> CreateProfileAsync(UserModel model);

        Task<Result> EditProfileAsync(string userId, UserModel model);

        Task<Result<UserModel>> ProfileAsync(string userId);
    }
}
