using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheBugTracker.Models;

namespace TheBugTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<BTUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BTCompany> Companies { get; set; }
        public DbSet<BTInvite> Invites { get; set; }
        public DbSet<BTProject> Projects { get; set; }
        public DbSet<BTTicket> Tickets { get; set; }
        public DbSet<BTNotification> Notifications { get; set; }
        public DbSet<BTProjectPriority> ProjectPriorities { get; set; }
        public DbSet<BTTicketAttachment> TicketAttachments { get; set; }
        public DbSet<BTTicketComment> TicketComments { get; set; }
        public DbSet<BTTicketHistory> TicketHistories { get; set; }
        public DbSet<BTTicketPriority> TicketPriorities { get; set; }
        public DbSet<BTTicketStatus> TicketStatuses { get; set; }
        public DbSet<BTTicketType> TicketTypes { get; set; }

        // In order to see these models added to the Database
        // use "add-migration "{name of migration}", first.
        // Then use "update-database"
        // These commands are don in the Package Manager Console

        // These tables can be seen in pgAdmin
    }
}
