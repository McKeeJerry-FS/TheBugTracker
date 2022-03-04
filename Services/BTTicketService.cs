using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Data;
using TheBugTracker.Models;
using TheBugTracker.Models.Enums;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Services
{
    public class BTTicketService : IBTTicketService
    {
        //Dependency Injection
        private readonly ApplicationDbContext _context;
        private readonly IBTRolesService _rolesService;
        private readonly IBTProjectService _projectService;

        //Constructor
        public BTTicketService(ApplicationDbContext context, 
                               IBTRolesService rolesService, 
                               IBTProjectService projectService)
        {
            _context = context;
            _rolesService = rolesService;
            _projectService = projectService;
        }

        //CRUD Methods - COMPLETED!
        //CRUD - CREATE
        public async Task AddNewTicketAsync(BTTicket ticket)
        {
            _context.Add(ticket);
            await _context.SaveChangesAsync();
        }

        //CRUD - READ
        public async Task<BTTicket> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        //CRUD - UPDATE
        public async  Task UpdateTicketAsync(BTTicket ticket)
        {
            _context.Update(ticket);
            await _context.SaveChangesAsync();
        }

        //CRUD - DELETE
        public async Task ArchiveTicketAsync(BTTicket ticket)
        {
            ticket.Archived = true;
            _context.Remove(ticket);
            await _context.SaveChangesAsync();
        }

        
        
        public async Task AssignTicketAsync(int ticketId, string userId)
        {
            BTTicket ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            try
            {
                if (ticket != null)
                {
                    ticket.DeveloperUserId = userId;
                    // Revisit this code when assigning tickets
                    ticket.TicketStatusId = (await LookupTicketStatusIdAsync("Development")).Value;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        // "GET" Methods

        public async Task<List<BTTicket>> GetAllTicketsByCompanyAsync(int companyId)
        {
            try
            {
                List<BTTicket> tickets = await _context.Projects
                                                       .Where(p => p.CompanyId == companyId)
                                                       .SelectMany(p => p.Tickets)
                                                            .Include(t => t.Attachments)
                                                            .Include(t => t.Comments)
                                                            .Include(t => t.DeveloperUser)
                                                            .Include(t => t.History)
                                                            .Include(t => t.OwnerUser)
                                                            .Include(t => t.TicketPriority)
                                                            .Include(t => t.TicketStatus)
                                                            .Include(t => t.TicketType)
                                                            .Include(t => t.Project)
                                                        .ToListAsync();
                return tickets;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BTTicket>> GetAllTicketsByPriorityAsync(int companyId, string priorityName)
        {
            int priorityId = (await LookupTicketPriorityIdAsync(priorityName)).Value;
            try
            {
                List<BTTicket> tickets = await _context.Projects
                                                       .Where(p => p.CompanyId == companyId)
                                                       .SelectMany(p => p.Tickets)
                                                            .Include(t => t.Attachments)
                                                            .Include(t => t.Comments)
                                                            .Include(t => t.DeveloperUser)
                                                            .Include(t => t.History)
                                                            .Include(t => t.OwnerUser)
                                                            .Include(t => t.TicketPriority)
                                                            .Include(t => t.TicketStatus)
                                                            .Include(t => t.TicketType)
                                                            .Include(t => t.Project)
                                                       .Where(t => t.TicketPriorityId == priorityId)
                                                       .ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BTTicket>> GetAllTicketsByStatusAsync(int companyId, string statusName)
        {
            int statusId = (await LookupTicketStatusIdAsync(statusName)).Value;
            try
            {
                List<BTTicket> tickets = await _context.Projects
                                                       .Where(p => p.CompanyId == companyId)
                                                       .SelectMany(p => p.Tickets)
                                                            .Include(t => t.Attachments)
                                                            .Include(t => t.Comments)
                                                            .Include(t => t.DeveloperUser)
                                                            .Include(t => t.History)
                                                            .Include(t => t.OwnerUser)
                                                            .Include(t => t.TicketPriority)
                                                            .Include(t => t.TicketStatus)
                                                            .Include(t => t.TicketType)
                                                            .Include(t => t.Project)
                                                       .Where(t => t.TicketStatusId == statusId)
                                                       .ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BTTicket>> GetAllTicketsByTypeAsync(int companyId, string typeName)
        {
            int typeId = (await LookupTicketTypeIdAsync(typeName)).Value;
            try
            {
                List<BTTicket> tickets = await _context.Projects
                                                       .Where(p => p.CompanyId == companyId)
                                                       .SelectMany(p => p.Tickets)
                                                            .Include(t => t.Attachments)
                                                            .Include(t => t.Comments)
                                                            .Include(t => t.DeveloperUser)
                                                            .Include(t => t.History)
                                                            .Include(t => t.OwnerUser)
                                                            .Include(t => t.TicketPriority)
                                                            .Include(t => t.TicketStatus)
                                                            .Include(t => t.TicketType)
                                                            .Include(t => t.Project)
                                                       .Where(t => t.TicketTypeId == typeId)
                                                       .ToListAsync();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }




        public async Task<List<BTTicket>> GetArchivedTicketsAsync(int companyId)
        {
            try
            {
                List<BTTicket> tickets = (await GetAllTicketsByCompanyAsync(companyId))
                                                .Where(t => t.Archived == true)
                                                .ToList();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }


        //"GET" Project Ticket Methods
        public async Task<List<BTTicket>> GetProjectTicketsByPriorityAsync(string priorityName, int companyId, int projectId)
        {
            List<BTTicket> tickets = new();
            try
            {
                tickets = (await GetAllTicketsByPriorityAsync(companyId, priorityName)).Where(t => t.ProjectID == projectId).ToList();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BTTicket>> GetProjectTicketsByRoleAsync(string role, string userId, int projectId, int companyId)
        {
            List<BTTicket> tickets = new();
            try
            {
                tickets = (await GetTicketsByRoleAsync(role, userId, companyId)).Where(t => t.ProjectID == projectId).ToList();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BTTicket>> GetProjectTicketsByStatusAsync(string statusName, int companyId, int projectId)
        {
            List<BTTicket> tickets = new();
            try
            {
                tickets = (await GetAllTicketsByStatusAsync(companyId, statusName)).Where(t => t.ProjectID == projectId).ToList();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BTTicket>> GetProjectTicketsByTypeAsync(string typeName, int companyId, int projectId)
        {
            List<BTTicket> tickets = new();
            try
            {
                tickets = (await GetAllTicketsByTypeAsync(companyId, typeName)).Where(t => t.ProjectID == projectId).ToList();
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BTUser> GetTicketDeveloperAsync(int ticketId, int companyId)
        {
            BTUser developer = new();
            try
            {
                BTTicket ticket = (await GetAllTicketsByCompanyAsync(companyId)).FirstOrDefault(t => t.Id == ticketId);
                if(ticket?.DeveloperUserId != null)
                {
                    developer = ticket.DeveloperUser;

                }
                return developer;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BTTicket>> GetTicketsByRoleAsync(string role, string userId, int companyId)
        {
            List<BTTicket> tickets = new();

            try
            {
                if (role == Roles.Admin.ToString())
                {
                    tickets = await GetAllTicketsByCompanyAsync(companyId);
                }
                else if (role == Roles.Developer.ToString())
                {
                    tickets = (await GetAllTicketsByCompanyAsync(companyId)).Where(t => t.DeveloperUserId == userId).ToList();
                }
                else if (role == Roles.Submitter.ToString())
                {
                    tickets = (await GetAllTicketsByCompanyAsync(companyId)).Where(t => t.OwnerUserId == userId).ToList();
                }
                else if (role == Roles.ProjectManager.ToString())
                {
                    tickets = await GetTicketsByUserIdAsync(userId, companyId);

                }
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BTTicket>> GetTicketsByUserIdAsync(string userId, int companyId)
        {
            BTUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            List<BTTicket> tickets = new();
            try
            {
                if (await _rolesService.IsUserInRoleAsync(user, Roles.Admin.ToString()))
                {
                    tickets = (await _projectService.GetAllProjectsByCompany(companyId))
                                                    .SelectMany(p => p.Tickets)
                                                    .ToList();
                    
                }
                else if (await _rolesService.IsUserInRoleAsync(user, Roles.Developer.ToString()))
                {
                    tickets = (await _projectService.GetAllProjectsByCompany(companyId))
                                                    .SelectMany(p => p.Tickets)
                                                    .Where(t => t.DeveloperUserId == userId)
                                                    .ToList();
                    
                }
                else if (await _rolesService.IsUserInRoleAsync(user, Roles.Submitter.ToString()))
                {
                    tickets = (await _projectService.GetAllProjectsByCompany(companyId))
                                                    .SelectMany(p => p.Tickets)
                                                    .Where(t => t.OwnerUserId == userId)
                                                    .ToList();
                   
                }
                else if (await _rolesService.IsUserInRoleAsync(user, Roles.ProjectManager.ToString()))
                {
                    tickets = (await _projectService.GetUserProjectsAsync(userId))
                                                    .SelectMany(t => t.Tickets)
                                                    .ToList();
                }
                return tickets;
            }
            catch (Exception)
            {

                throw;
            }
        }


        // "LOOKUP" Methods
        public async Task<int?> LookupTicketPriorityIdAsync(string priorityName)
        {
            try
            {
                BTTicketPriority priority = await _context.TicketPriorities.FirstOrDefaultAsync(p => p.Name == priorityName);
                return priority?.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int?> LookupTicketStatusIdAsync(string statusName)
        {
            try
            {
                BTTicketStatus status = await _context.TicketStatuses.FirstOrDefaultAsync(p => p.Name == statusName);
                return status?.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int?> LookupTicketTypeIdAsync(string typeName)
        {
            try
            {
                BTTicketType type = await _context.TicketTypes.FirstOrDefaultAsync(p => p.Name == typeName);
                return type?.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
