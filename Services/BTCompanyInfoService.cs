using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Models;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Services
{
    public class BTCompanyInfoService : IBTCompanyInfoService
    {
        public Task<BTUser> GetAllMembersAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<BTProject> GetAllProjectsAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<BTTicket> GetAllTicketsAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<BTCompany> GetCompanyInfoByIdAsync(int? companyId)
        {
            throw new NotImplementedException();
        }
    }
}
