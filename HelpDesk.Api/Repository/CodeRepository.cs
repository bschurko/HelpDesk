using HelpDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Repository
{
    public interface ICodeRepository
    {
        Task<List<Priority>> GetAllPriorities(CancellationToken ct = default);
        Task<List<Status>> GetAllStatuses(CancellationToken ct = default);
    }

    public class CodeRepository : ICodeRepository
    {
        private readonly AppDbContext db;

        public CodeRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Status>> GetAllStatuses(CancellationToken ct = default)
        {
            var entities = await db.Status.ToListAsync(ct);
            return entities;
        }

        public async Task<List<Priority>> GetAllPriorities(CancellationToken ct = default)
        {
            var entities = await db.Priority.ToListAsync(ct);
            return entities;
        }
    }
}
