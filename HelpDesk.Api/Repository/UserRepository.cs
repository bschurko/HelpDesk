using HelpDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Repository
{
    public interface IUserRepository
    {
        Task<bool> Add(User entity, CancellationToken token = default);
        Task<bool> Delete(int id, CancellationToken token = default);
        Task<bool> Delete(User entity, CancellationToken token = default);
        Task<User> Get(int id, CancellationToken token = default);
        Task<List<User>> GetAll(CancellationToken token = default);
        Task<bool> Update(User entity, CancellationToken token = default);
    }

    public class UserRepository : IRepository<User>, IUserRepository
    {
        private readonly AppDbContext db;

        public UserRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Add(User entity, CancellationToken token = default)
        {
            db.Users.Add(entity);
            var res = await db.SaveChangesAsync(token);
            return res > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken token = default)
        {
            var entity = await db.Users.FindAsync(id, token);
            db.Users.Remove(entity);
            var res = await db.SaveChangesAsync(token);
            return res > 0;
        }

        public async Task<bool> Delete(User entity, CancellationToken token = default)
        {
            db.Users.Remove(entity);
            var res = await db.SaveChangesAsync(token);
            return res > 0;
        }

        public async Task<User> Get(int id, CancellationToken token = default)
        {
            var entity = await db.Users
                .Include(cc => cc.Team)
                .FirstOrDefaultAsync(cc => cc.Id == id, cancellationToken: token);

            return entity;
        }

        public async Task<List<User>> GetAll(CancellationToken token = default)
        {
            var entities = await db.Users
                .Include(cc => cc.Team)
                .ToListAsync<User>(token);

            return entities;
        }

        public async Task<bool> Update(User entity, CancellationToken token = default)
        {
            db.Users.Update(entity);
            var res = await db.SaveChangesAsync(token);
            return res > 0;
        }
    }
}
