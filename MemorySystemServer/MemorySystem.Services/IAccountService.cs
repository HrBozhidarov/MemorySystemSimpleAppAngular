namespace MemorySystem.Services
{
    using System.Threading.Tasks;
    using MemorySystem.Services.Models;
    using MemorySystemApp.Models.Identity;

    public interface IAccountService
    {
        public Task<Result<LoginModel>> Login(LoginUserModel model);
    }
}
