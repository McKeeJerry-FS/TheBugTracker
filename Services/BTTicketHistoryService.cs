using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Data;
using TheBugTracker.Models;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Services
{
    public class BTTicketHistoryService : IBTTicketHistoryService
    {
        //Fields
        private readonly ApplicationDbContext _context;
        //Constructor
        public BTTicketHistoryService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddHistoryAsync(BTTicket oldTicket, BTTicket newTicket, string userId)
        {
            //New Ticket has been added
            if(oldTicket == null && newTicket != null)
            {
                //Creating a TicketHistory Object and assigning values inline
                BTTicketHistory history = new()
                {
                    TicketId = newTicket.Id,
                    Property = "",
                    OldValue = "",
                    NewValue = "",
                    Created = DateTimeOffset.Now,
                    UserId = userId,
                    Description = "New Ticket Created"

                };

                try
                {
                    await _context.TicketHistories.AddAsync(history);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }

            }
            else
            {
                // Checking Ticket Title
                if (oldTicket.Title != newTicket.Title)
                {
                    BTTicketHistory history = new()
                    {
                        TicketId = newTicket.Id,
                        Property = "Title",
                        OldValue = oldTicket.Title,
                        NewValue = newTicket.Title,
                        Created = DateTimeOffset.Now,
                        UserId = userId,
                        Description = $"New Ticket Title: {newTicket.Title}"

                    };

                    await _context.TicketHistories.AddAsync(history);
                }

                // Checking Ticket Description
                if (oldTicket.Description != newTicket.Description)
                {
                    BTTicketHistory history = new()
                    {
                        TicketId = newTicket.Id,
                        Property = "Description",
                        OldValue = oldTicket.Description,
                        NewValue = newTicket.Description,
                        Created = DateTimeOffset.Now,
                        UserId = userId,
                        Description = $"New Ticket Description: {newTicket.Description}"

                    };

                    await _context.TicketHistories.AddAsync(history);
                }

                // Checking Ticket Priority
                if (oldTicket.TicketPriorityId != newTicket.TicketPriorityId)
                {
                    BTTicketHistory history = new()
                    {
                        TicketId = newTicket.Id,
                        Property = "TicketPriority",
                        OldValue = oldTicket.TicketPriority.Name,
                        NewValue = newTicket.TicketPriority.Name,
                        Created = DateTimeOffset.Now,
                        UserId = userId,
                        Description = $"New Ticket Priority: {newTicket.TicketPriority.Name}"

                    };

                    await _context.TicketHistories.AddAsync(history);
                }
                
                // Checking Ticket Status
                if (oldTicket.TicketStatusId != newTicket.TicketStatusId)
                {
                    BTTicketHistory history = new()
                    {
                        TicketId = newTicket.Id,
                        Property = "TicketStatus",
                        OldValue = oldTicket.TicketStatus.Name,
                        NewValue = newTicket.TicketStatus.Name,
                        Created = DateTimeOffset.Now,
                        UserId = userId,
                        Description = $"New Ticket Status: {newTicket.TicketStatus.Name}"
                    };

                    await _context.TicketHistories.AddAsync(history);
                }

                // Checking Ticket Type
                if (oldTicket.TicketTypeId != newTicket.TicketTypeId)
                {
                    BTTicketHistory history = new()
                    {
                        TicketId = newTicket.Id,
                        Property = "TicketType",
                        OldValue = oldTicket.TicketType.Name,
                        NewValue = newTicket.TicketType.Name,
                        Created = DateTimeOffset.Now,
                        UserId = userId,
                        Description = $"New Ticket Type: {newTicket.TicketType.Name}"

                    };

                    await _context.TicketHistories.AddAsync(history);
                }

                // Checking Ticket Developer
                if (oldTicket.DeveloperUserId != newTicket.DeveloperUserId)
                {
                    BTTicketHistory history = new()
                    {
                        TicketId = newTicket.Id,
                        Property = "Title",
                        OldValue = oldTicket.DeveloperUser?.FullName ?? "Not Assigned",
                        NewValue = newTicket.DeveloperUser?.FullName,
                        Created = DateTimeOffset.Now,
                        UserId = userId,
                        Description = $"New Ticket Title: {newTicket.Title}"

                    };

                    await _context.TicketHistories.AddAsync(history);
                }
                
                try
                {
                    // Save changes to the TicketHistories Database
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            
        }

        public async Task<List<BTTicketHistory>> GetCompanyTicketsHistoriesAsync(int companyId)
        {
            try
            {
                List<BTProject> projects = (await _context.Companies
                                                         .Include(c => c.Projects)
                                                            .ThenInclude(p => p.Tickets)
                                                                .ThenInclude(t => t.History)
                                                                    .ThenInclude(h => h.User)
                                                         .FirstOrDefaultAsync(c => c.Id == companyId)).Projects.ToList();
                
                List<BTTicket> tickets = projects.SelectMany(p => p.Tickets).ToList();

                List<BTTicketHistory> ticketHistories = tickets.SelectMany(t => t.History).ToList();
                return ticketHistories;

                                                         
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BTTicketHistory>> GetProjectTicketHistoriesAsync(int projectId, int companyId)
        {
            try
            {
                BTProject project = await _context.Projects.Where(p => p.CompanyId == companyId)
                                                           .Include(p => p.Tickets)
                                                            .ThenInclude(t => t.History)
                                                                .ThenInclude(h => h.User)
                                                           .FirstOrDefaultAsync(p => p.Id == projectId);

                List<BTTicketHistory> ticketHistory = project.Tickets.SelectMany(t => t.History).ToList();
                return ticketHistory;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
