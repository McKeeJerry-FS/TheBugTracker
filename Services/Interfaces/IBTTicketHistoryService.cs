using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Models;

namespace TheBugTracker.Services.Interfaces
{
    public interface IBTTicketHistoryService
    {
        Task AddHistoryAsync(BTTicket oldTicket, BTTicket newTicket, string userId);
        Task<List<BTTicketHistory>> GetProjectTicketHistoriesAsync(int projectId, int companyId);
        Task<List<BTTicketHistory>> GetCompanyTicketsHistoriesAsync(int companyId);
    }
}
