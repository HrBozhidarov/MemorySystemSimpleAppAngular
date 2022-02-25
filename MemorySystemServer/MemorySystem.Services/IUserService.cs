namespace MemorySystem.Services
{
    using System.Threading.Tasks;
    using MemorySystem.Services.Models;

    public interface IUserService
    {
        Task<Result> Register(RegisterUserModel model);
    }
}
