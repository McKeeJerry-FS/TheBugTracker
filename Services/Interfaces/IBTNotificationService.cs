using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Models;

namespace TheBugTracker.Services.Interfaces
{
    public interface IBTNotificationService
    {
        public Task AddNotificationAsync(BTNotification notification);
        public Task<List<BTNotification>> GetRecievedNotificationsAsync(string userId);
        public Task<List<BTNotification>> GetSentNotificationsAsync(string userId);
        public Task SendEmailNotificationsByRoleAsync(BTNotification notification, int companyId, string role);
        public Task SendMembersEmailNotificationAsync(BTNotification notification, List<BTUser> members);
        public Task<bool> SendEmailNotificationAsync(BTNotification notification, string emailSubject);

    }
}
