using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Models;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Services
{
    public class BTNotificationService : IBTNotificationService
    {
        public Task AddNotificationAsync(BTNotification notification)
        {
            throw new NotImplementedException();
        }

        public Task<List<BTNotification>> GetRecievedNotificationsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BTNotification>> GetSentNotificationsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailNotificationAsync(BTNotification notification, string emailSubject)
        {
            throw new NotImplementedException();
        }

        public Task SendEmailNotificationsByRoleAsync(BTNotification notification, int companyId, string role)
        {
            throw new NotImplementedException();
        }

        public Task SendMembersEmailNotificationAsync(BTNotification notification, List<BTUser> members)
        {
            throw new NotImplementedException();
        }
    }
}
