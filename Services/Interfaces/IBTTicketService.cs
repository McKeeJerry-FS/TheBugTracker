using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Models;

namespace TheBugTracker.Services.Interfaces
{
    public interface IBTTicketService
    {
        // CRUD Methods
        public Task AddNewTicketAsync(BTTicket ticket);
        public Task UpdateTicketAsync(BTTicket ticket);
        public Task<BTTicket> GetTicketByIdAsync(int ticketId);
        public Task ArchiveTicketAsync(BTTicket ticket);

        public Task AssignTicketAsync(int ticketId, string userId);

        // "GET" Methods
        public Task<List<BTTicket>> GetArchivedTicketsAsync(int companyId);
        public Task<List<BTTicket>> GetAllTicketsByCompanyAsync(int companyId);
        public Task<List<BTTicket>> GetAllTicketsByPriorityAsync(int companyId, string priorityName);
        public Task<List<BTTicket>> GetAllTicketsByStatusAsync(int companyId, string statusName);
        public Task<List<BTTicket>> GetAllTicketsByTypeAsync(int companyId, string typeName);
        public Task<BTUser> GetTicketDeveloperAsync(int ticketId, int companyId); // **** UPDATED **** added "int companyId" 
        public Task<List<BTTicket>> GetTicketsByRoleAsync(string role, string userId, int companyId);
        public Task<List<BTTicket>> GetTicketsByUserIdAsync(string userId, int companyId);
        public Task<List<BTTicket>> GetProjectTicketsByRoleAsync(string role, string userId, int projectId, int companyId);
        public Task<List<BTTicket>> GetProjectTicketsByStatusAsync(string statusName, int companyId, int projectId);
        public Task<List<BTTicket>> GetProjectTicketsByPriorityAsync(string priorityName, int companyId, int projectId);
        public Task<List<BTTicket>> GetProjectTicketsByTypeAsync(string typeName, int companyId, int projectId);

        // "Lookup" Methods 
        public Task<int?> LookupTicketPriorityIdAsync(string priorityName);
        public Task<int?> LookupTicketStatusIdAsync(string statusName);
        public Task<int?> LookupTicketTypeIdAsync(string typeName);
    }
}
