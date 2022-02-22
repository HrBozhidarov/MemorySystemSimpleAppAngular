namespace MemorySystemApp.Services.Identity
{
    using System.Threading.Tasks;

    using MemorySystemApp.Models.Identity;

    public interface IIdentityService
    {
        public Task<Result<LoginModel>> Login(LoginUserRequestModel model);
    }
}
