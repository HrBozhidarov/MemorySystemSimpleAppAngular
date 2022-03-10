namespace MemorySystem.Services
{
    using System.Threading.Tasks;

    using MemorySystem.Services.Models;

    public interface IAccountService
    {
        public Task<Result<UserLogedModel>> Login(BaseUserModel model);
    }
}
