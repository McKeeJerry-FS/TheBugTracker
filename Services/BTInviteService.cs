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
    public class BTInviteService : IBTInviteService
    {
        private readonly ApplicationDbContext _context;

        // Dependency Injection
        public BTInviteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AcceptInviteAsync(Guid? token, string userId, int companyId)
        {
            BTInvite invite = await _context.Invites.FirstOrDefaultAsync(i => i.CompanyToken == token);
            if (invite == null)
            {
                return false;
            }
            
            try
            {
                invite.IsValid = false;
                invite.InviteeId = userId;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddNewInviteAsync(BTInvite invite)
        {
            try
            {
                await _context.Invites.AddAsync(invite);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AnyInviteAsync(Guid token, string email, int companyId)
        {
            try
            {
                bool result = await _context.Invites.Where(i => i.CompanyId == companyId)
                                                    .AnyAsync(i => i.CompanyToken == token && i.InviteeEmail == email);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BTInvite> GetInviteAsync(int inviteId, int companyId)
        {
            try
            {
                BTInvite invite = await _context.Invites.Where(i => i.CompanyId == companyId)
                                                        .Include(i => i.Company)
                                                        .Include(i => i.Project)
                                                        .Include(i => i.Invitor)
                                                        .FirstOrDefaultAsync(i => i.Id == inviteId);

                return invite;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BTInvite> GetInviteASync(Guid token, string email, int companyId)
        {
            try
            {
                BTInvite invite = await _context.Invites.Where(i => i.CompanyId == companyId)
                                                        .Include(i => i.Company)
                                                        .Include(i => i.Project)
                                                        .Include(i => i.Invitor)
                                                        .FirstOrDefaultAsync(i => i.CompanyToken == token && i.InviteeEmail == email);

                return invite;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ValidateInviteCodeAsync(Guid? token)
        {
            if (token == null)
            {
                return false;
            }
            bool result = false;

            BTInvite invite = await _context.Invites.FirstOrDefaultAsync(i => i.CompanyToken == token);

            if (invite != null)
            {
                // Determine invite Date
                DateTime inviteDate = invite.InviteDate.DateTime;

                bool validDate = (DateTime.Now - inviteDate).TotalDays <= 7;

                if (validDate)
                {
                    result = invite.IsValid;
                }
            }
            return result;
        }
    }
}
