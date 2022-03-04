using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Data;

namespace TheBugTracker.Services
{
    public class BTInviteService
    {
        private readonly ApplicationDbContext _context;

        // Dependency Injection
        public BTInviteService(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
