using HelpDesk.Api.Repository;
using HelpDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Api.Util
{
    public static class SeedDb
    {
        public static void SeedSampleData(AppDbContext db)
        {
            Team team = new Team() { Id = 0, Name = "Development", Organization = "SchurKorp", DateCreated = DateTime.Now };
            if (!db.Teams.Any())
            {
                db.Teams.Add(team);
            }

            User user = new User
            {
                Id = 0,
                Email = "brettschurko@gmail.com",
                FirstName = "brett",
                LastName = "Schurko",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                HashPassword = PasswordHashUtil.Hash("Password123"),
                Username = "brettschurko",
                Team = team
            };


            if (!db.Users.Any())
            {
                db.Users.Add(
                    user
                );
            }

            var tags = new List<Tag>()
                    {
                        new Tag { Id = 0, Name = "Software Development" },
                        new Tag { Id = 0, Name = "UI Design" },
                        new Tag { Id = 0, Name = "Enterprise Architect" },
                        new Tag { Id = 0, Name = "QA" },
                        new Tag { Id = 0, Name = "Software Bug" }
                    };

            if (!db.Tags.Any())
            {
                db.Tags.AddRange(
                    tags
                );
            }

            var comments = new List<Comment>()
            {
                new Comment
                {
                    Id = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Title = "QA Needed!",
                    Message = "A QA is required to review bugs in this software."
                },
                new Comment
                {
                    Id = 0,
                    DateModified = DateTime.Now,
                    DateCreated = DateTime.Now,
                    Title = "Bug Review",
                    Message = "We need developers to review this bug!"
                },
                new Comment
                {
                    Id = 0,
                    DateModified = DateTime.Now,
                    DateCreated = DateTime.Now,
                    Title = "Developer Needed!",
                    Message = "We need developers to fix the software!"
                }
            };

            if (!db.Comments.Any())
            {
                db.Comments.AddRange(comments);
            }

            if (!db.Tickets.Any())
            {
                db.Tickets.Add(
                    new Ticket()
                    {
                        Id = 0,
                        Assigned = user,
                        AssignedId = user.Id,
                        Attachment = null,
                        Comments = comments,
                        CreatedBy = user,
                        CreatedById = user.Id,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        Tags = tags,
                        Status = db.Status.FirstOrDefault(cc => cc.Name.Equals("Open")),
                        Priority = db.Priority.FirstOrDefault(cc => cc.Name.Equals("Normal")),
                        TicketNumber = Guid.NewGuid().ToString(),
                        Title = "Software Bug in Order Page",
                        Team = team,
                        TeamId = team.Id,
                        Description = "There is a bad software bug on the orders page, when users checkout their products."
                    }
                );
            }

            db.SaveChanges();
        }

        public static void SeedData(AppDbContext db)
        {
            if (!db.Priority.Any())
            {
                db.Priority.AddRange(
                    new List<Priority>()
                    {
                        new Priority { Id = 0, Name = "Low" },
                        new Priority { Id = 0, Name = "Normal" },
                        new Priority { Id = 0, Name = "High" },
                        new Priority { Id = 0, Name = "Critical" }
                    }
                );
            }


            if (!db.Status.Any())
            {
                db.Status.AddRange(
                    new List<Status>
                    {
                        new Status { Id = 0, Name = "Open" },
                        new Status { Id = 0, Name = "In Progress" },
                        new Status { Id = 0, Name = "Pending" },
                        new Status { Id = 0, Name = "Resolved" },
                        new Status { Id = 0, Name = "Closed" }
                    }
                );
            }

            db.SaveChanges();
        }
    }
}