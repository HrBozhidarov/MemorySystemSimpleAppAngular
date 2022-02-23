namespace MemorySystem.Services
{
    using System.Threading.Tasks;
    using MemorySystem.Services.Models;
    using MemorySystemApp.Models.Identity;

    public interface IIdentityService
    {
        public Task<Result<LoginModel>> Login(LoginUserModel model);
    }
}
