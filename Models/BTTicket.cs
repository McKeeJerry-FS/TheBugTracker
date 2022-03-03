using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBugTracker.Models
{
    public class BTTicket
    {
        //Primary Key
        public int Id { get; set; }

        [Required] //Makes the property required
        [StringLength(50)] //Limits the size of the string
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)] //Specifies the DataType
        [DisplayName("Created")]
        public DateTimeOffset Created { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated")]
        public DateTimeOffset? Updated { get; set; } // "?" == Nullable

        [DisplayName("Archived")]
        public bool Archived { get; set; }

        [DisplayName("Project")]
        public int ProjectID { get; set; }


        // Foreign Keys - Lookup Tables
        [DisplayName("Ticket Type")]
        public int TicketTypeId { get; set; } 

        [DisplayName("Ticket Priority")]
        public int TicketPriorityId { get; set; } 

        [DisplayName("Ticket Status")]
        public int TicketStatusId { get; set; }


        // BTUser Foreign Keys
        [DisplayName("Ticket Owner")]
        public string OwnerUserId { get; set; }

        [DisplayName("Ticket Developer")]
        public string DeveloperUserId { get; set; }

        
        // Navigation properties
        public virtual BTProject Project { get; set; }
        public virtual BTTicketType TicketType { get; set; }
        public virtual BTTicketPriority TicketPriority { get; set; }
        public virtual BTTicketStatus TicketStatus { get; set; }
        public virtual BTUser OwnerUser { get; set; }
        public virtual BTUser DeveloperUser { get; set; }


        // ICollection Navigation properties
        public virtual ICollection<BTTicketComment> Comments { get; set; } = new HashSet<BTTicketComment>();
        public virtual ICollection<BTTicketAttachment> Attachments { get; set; } = new HashSet<BTTicketAttachment>();
        public virtual ICollection<BTNotification> Notifications { get; set; } = new HashSet<BTNotification>();
        public virtual ICollection<BTTicketHistory> History { get; set; } = new HashSet<BTTicketHistory>();
    }
}
