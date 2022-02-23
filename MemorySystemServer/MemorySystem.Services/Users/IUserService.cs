namespace MemorySystem.Services
{
    using System.Threading.Tasks;
    using MemorySystem.Data.Models;
    using MemorySystem.Services.Models;

    public interface IUserService
    {
        Task<Result<User>> Register(RegisterUserModel model);
    }
}
