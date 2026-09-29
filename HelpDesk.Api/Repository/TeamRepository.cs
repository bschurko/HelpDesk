using HelpDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Repository
{
    public interface ITeamRepository
    {
        Task<bool> Add(Team entity, CancellationToken token = default);
        Task<bool> Delete(int id, CancellationToken token = default);
        Task<bool> Delete(Team entity, CancellationToken token = default);
        Task<Team> Get(int id, CancellationToken token = default);
        Task<List<Team>> GetAll(CancellationToken token = default);
        Task<bool> Update(Team entity, CancellationToken token = default);
    }

    public class TeamRepository : IRepository<Team>, ITeamRepository
    {
        private readonly AppDbContext db;

        public TeamRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Add(Team entity, CancellationToken token = default)
        {
            db.Teams.Add(entity);
            var res = await db.SaveChangesAsync(token);
            return res > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken token = default)
        {
            var entity = await db.Teams.FindAsync(id, token);
            db.Teams.Remove(entity);
            var res = await db.SaveChangesAsync(token);
            return res > 0;
        }

        public async Task<bool> Delete(Team entity, CancellationToken token = default)
        {
            db.Teams.Remove(entity);
            var res = await db.SaveChangesAsync(token);
            return res > 0;
        }

        public async Task<Team> Get(int id, CancellationToken token = default)
        {
            var entity = await db.Teams.FindAsync(id, token);
            return entity;
        }

        public async Task<List<Team>> GetAll(CancellationToken token = default)
        {
            var entities = await db.Teams.ToListAsync<Team>(token);
            return entities;
        }

        public async Task<bool> Update(Team entity, CancellationToken token = default)
        {
            db.Teams.Update(entity);
            var res = await db.SaveChangesAsync(token);
            return res > 0;
        }
    }
}
