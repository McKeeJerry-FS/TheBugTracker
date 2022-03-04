using Microsoft.AspNetCore.Identity.UI.Services;
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
    public class BTNotificationService : IBTNotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IBTRolesService _rolesService;

        // Constructor
        public BTNotificationService(ApplicationDbContext context,
                                     IEmailSender emailSender,
                                     IBTRolesService rolesService)
        {
            _context = context;
            _emailSender = emailSender;
            _rolesService = rolesService;
        }

        public async Task AddNotificationAsync(BTNotification notification)
        {
            try
            {
                await _context.AddAsync(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BTNotification>> GetRecievedNotificationsAsync(string userId)
        {
            try
            {
                List<BTNotification> notifications = await _context.Notifications
                                                                   .Include(n => n.Recipient)
                                                                   .Include(n => n.Sender)
                                                                   .Include(n => n.Ticket)
                                                                        .ThenInclude(t => t.Project)
                                                                   .Where(n => n.RecipientId == userId).ToListAsync();

                return notifications;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BTNotification>> GetSentNotificationsAsync(string userId)
        {
            try
            {
                List<BTNotification> notifications = await _context.Notifications
                                                                   .Include(n => n.Recipient)
                                                                   .Include(n => n.Sender)
                                                                   .Include(n => n.Ticket)
                                                                        .ThenInclude(t => t.Project)
                                                                   .Where(n => n.SenderId == userId).ToListAsync();

                return notifications;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> SendEmailNotificationAsync(BTNotification notification, string emailSubject)
        {
            BTUser bTUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == notification.RecipientId);
           
            if (bTUser != null)
            {
                string btUserEmail = bTUser.Email;
                string message = notification.Message;

                // Send Email
                try
                {
                    await _emailSender.SendEmailAsync(btUserEmail, emailSubject, message);
                    return true;
                }
                catch (Exception)
                {

                    throw;
                }
            
            }
            else
            {
                return false;
            }
            
        }

        public async Task SendEmailNotificationsByRoleAsync(BTNotification notification, int companyId, string role)
        {
            try
            {
                List<BTUser> members = await _rolesService.GetUsersInRolesAsync(role, companyId);

                foreach (BTUser bTUser in members)
                {
                    notification.RecipientId = bTUser.Id;
                    await SendEmailNotificationAsync(notification, notification.Title);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendMembersEmailNotificationAsync(BTNotification notification, List<BTUser> members)
        {
            try
            {
                foreach (BTUser bTUser in members)
                {
                    notification.RecipientId = bTUser.Id;
                    await SendEmailNotificationAsync(notification, notification.Title);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
