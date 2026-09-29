using HelpDesk.Domain.Models;
using HelpDesk.Services.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDesk.Kernel.Services
{
    public interface IDomainApiService
    {
        Task<bool> CreateTeamAsync(Team team);
        Task<bool> CreateTicketAsync(Ticket entity);
        Task<bool> CreateUserAsync(User entity);
        Task<List<Priority>> GetPrioritiesAsync();
        Task<List<Status>> GetStatusesAsync();
        Task<List<Team>> GetTeamsAsync();
        Task<Ticket> GetTicketAsync(int id);
        Task<List<Ticket>> GetTicketsAsync();
        Task<List<User>> GetUsersAsync();
        Task<bool> UpdateTeamAsync(Team entity);
        Task<bool> UpdateTicketAsync(Ticket entity);
        Task<bool> UpdateUserAsync(User entity);
    }

    public class DomainApiService : IDomainApiService
    {
        private readonly IHttpService client;
        private readonly string apiUrl;
        public DomainApiService(IHttpService client, IConfiguration config)
        {
            this.client = client;
            this.apiUrl = config["ApiServer:Hostname"];
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            var entities = await client.GetAsync<List<Ticket>>($"{apiUrl}/api/ticket/list");
            return entities;
        }

        public async Task<bool> CreateTicketAsync(Ticket entity)
        {
            var response = await client.PostAsync<Ticket>($"{apiUrl}/api/ticket", entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTicketAsync(Ticket entity)
        {
            var response = await client.PutAsync<Ticket>($"{apiUrl}/api/ticket", entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<Ticket> GetTicketAsync(int id)
        {
            var response = await client.GetAsync<Ticket>($"{apiUrl}/api/ticket/{id}");
            return response;
        }

        public async Task<List<Priority>> GetPrioritiesAsync()
        {
            var entities = await client.GetAsync<List<Priority>>($"{apiUrl}/api/code/priorities");
            return entities;
        }

        public async Task<List<Status>> GetStatusesAsync()
        {
            var entities = await client.GetAsync<List<Status>>($"{apiUrl}/api/code/statuses");
            return entities;
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            var entities = await client.GetAsync<List<Team>>($"{apiUrl}/api/team/list");
            return entities;
        }

        public async Task<bool> CreateTeamAsync(Team team)
        {
            var response = await client.PostAsync<Team>($"{apiUrl}/api/team", team);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTeamAsync(Team entity)
        {
            var response = await client.PutAsync<Team>($"{apiUrl}/api/team", entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var entities = await client.GetAsync<List<User>>($"{apiUrl}/api/user/list");
            return entities;
        }

        public async Task<bool> CreateUserAsync(User entity)
        {
            var response = await client.PostAsync<User>($"{apiUrl}/api/user", entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(User entity)
        {
            var response = await client.PutAsync<User>($"{apiUrl}/api/user", entity);
            return response.IsSuccessStatusCode;
        }
    }
}
