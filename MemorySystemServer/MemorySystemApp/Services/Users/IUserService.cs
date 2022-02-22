namespace MemorySystemApp.Services.Users
{
    using System.Threading.Tasks;

    using MemorySystemApp.Data.Models;
    using MemorySystemApp.Models.Users;

    public interface IUserService
    {
        Task<Result<User>> Register(RegisterUserRequestModel model);
    }
}
