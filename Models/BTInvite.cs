using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TheBugTracker.Models
{
    public class BTInvite
    {
        //Primary Key
        public int Id { get; set; }

        [DisplayName("Date Sent")]
        public DateTimeOffset InviteDate { get; set; }
        [DisplayName("Join Date")]
        public DateTimeOffset JoinDate { get; set; }

        [DisplayName("Code")]
        public Guid CompanyToken { get; set; }

        
        
        //Foreign Keys
        [DisplayName("Company")]
        public int CompanyId { get; set; }
        
        [DisplayName("Project")]
        public int ProjectId { get; set; }
        
        [DisplayName("Invitor")]
        public string InvitorId { get; set; }
        
        [DisplayName("Invitee")]
        public string InviteeId { get; set; }


        public string InviteeEmail { get; set; }
        public string InviteeFirstName { get; set; }
        public string InviteeLastName { get; set; }


        // Determines if an invite is still valid
        public bool IsValid { get; set; }

        
        // Navigation properties
        public virtual BTCompany Company { get; set; }
        public virtual BTUser Invitor { get; set; }
        public virtual BTUser Invitee { get; set; }
        public virtual BTProject Project { get; set; }
    }
}
