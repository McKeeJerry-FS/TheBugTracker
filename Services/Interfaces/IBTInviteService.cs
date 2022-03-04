using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Models;

namespace TheBugTracker.Services.Interfaces
{
    public interface IBTInviteService
    {
        public Task<bool> AcceptInviteAsync(Guid? token, string userId, int companyId);
        public Task AddNewInviteAsync(BTInvite invite);
        public Task<bool> AnyInviteAsync(Guid token, string email, int companyId);
        public Task<BTInvite> GetInviteAsync(int inviteId, int companyId);
        public Task<BTInvite> GetInviteASync(Guid token, string email, int companyId);
        public Task<bool> ValidateInviteCodeAsync(Guid? token);
    }
}
