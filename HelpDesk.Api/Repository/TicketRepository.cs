
using HelpDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Repository
{
    public interface ITicketRepository
    {
        Task<bool> Add(Ticket entity, CancellationToken token = default);
        Task<bool> Delete(int id, CancellationToken token = default);
        Task<bool> Delete(Ticket entity, CancellationToken token = default);
        Task<Ticket> Get(int id, CancellationToken token = default);
        Task<List<Ticket>> GetAll(CancellationToken token = default);
        Task<bool> Update(Ticket entity, CancellationToken token = default);
    }

    public class TicketRepository : IRepository<Ticket>, ITicketRepository
    {
        private readonly AppDbContext db;

        public TicketRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> Add(Domain.Models.Ticket entity, CancellationToken token)
        {
            if (entity is not Ticket)
                throw new ArgumentException("entity must be a Ticket", nameof(entity));

            db.Tickets.Add(entity);
            var res = await db.SaveChangesAsync(token);
            return res > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken token = default)
        {
            var ticket = await db.Tickets.FindAsync(id);
            db.Tickets.Remove(ticket!);
            var res = await db.SaveChangesAsync(token);

            return res > 0;
        }

        public async Task<bool> Delete(Domain.Models.Ticket entity, CancellationToken token = default)
        {
            if (entity is not Ticket)
                throw new ArgumentException("entity must be a Ticket", nameof(entity));

            db.Tickets.Remove(entity);
            var res = await db.SaveChangesAsync(token);
            return res > 0;
        }

        public async Task<Ticket> Get(int id, CancellationToken token = default)
        {
            Domain.Models.Ticket? ticket = await db.Tickets
                .Include(cc => cc.Assigned)
                .Include(cc => cc.Attachment)
                .Include(cc => cc.Comments)
                .Include(cc => cc.CreatedBy)
                .Include(cc => cc.Status)
                .Include(cc => cc.Team)
                .Include(cc => cc.Priority)
                .Include(cc => cc.Tags)
                .FirstOrDefaultAsync(cc => cc.Id == id, token);

            return ticket;
        }

        public async Task<List<Ticket>> GetAll(CancellationToken token = default)
        {
            var tickets = await db.Tickets
                .Include(cc => cc.Assigned)
                .Include(cc => cc.Attachment)
                .Include(cc => cc.Comments)
                .Include(cc => cc.CreatedBy)
                .Include(cc => cc.Status)
                .Include(cc => cc.Priority)
                .Include(cc => cc.Tags)
                .ToListAsync(token);

            return tickets;
        }

        public async Task<bool> Update(Ticket entity, CancellationToken token = default)
        {
            db.Tickets.Update(entity);
            var res = await db.SaveChangesAsync(token);
            return res > 0;

        }
    }
}
