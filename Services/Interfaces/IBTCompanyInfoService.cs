using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Models;

namespace TheBugTracker.Services.Interfaces
{
    public interface IBTCompanyInfoService
    {
        public Task<BTCompany> GetCompanyInfoByIdAsync(int? companyId);
        public Task<List<BTUser>> GetAllMembersAsync(int companyId);
        public Task<List<BTProject>> GetAllProjectsAsync(int companyId);
        public Task<List<BTTicket>> GetAllTicketsAsync(int companyId);

    }
}
