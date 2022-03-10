namespace MemorySystem.Services
{
    using System.Threading.Tasks;

    using MemorySystem.Services.Models;

    public interface IUserService
    {
        Task<Result> CreateAsync(UserModel model);

        Task<Result> UpdateAsync(string userId, UserModel model);

        Task<Result<UserModel>> DetailsAsync(string userId);
    }
}
