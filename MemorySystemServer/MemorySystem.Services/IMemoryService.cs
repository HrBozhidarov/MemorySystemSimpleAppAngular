namespace MemorySystem.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MemorySystem.Services.Models;

    public interface IMemoryService
    {
        Task<Result> Create(CreateMemoryModel model, string userId);

        Task<Result<IEnumerable<MemoryModel>>> UserMemories(string userId, string category);

        Task<Result<IEnumerable<MemoryModel>>> GetUserMemories(string currentUserId, string userId);

        Task<Result<int>> LikeAsync(int id, string userId);

        Task Test();
    }
}
