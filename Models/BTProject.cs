using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheBugTracker.Models
{
    public class BTProject
    {
        //Primary Key
        public int Id { get; set; }


        [DisplayName("Company")]
        public string? CompanyId { get; set; } //Foreign Key

        [Required]
        [StringLength(50)]
        [DisplayName("Project Name")]
        public string Name { get; set; }
        
        [DisplayName("Project Description")]
        public string Description { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTimeOffset StartDate { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTimeOffset? EndDate { get; set; }

        [DisplayName("Priority")]
        public int? BTProjectPriorityId { get; set; }


        // Image properties
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile ImageFormFile { get; set; }
        
        [DisplayName("File Name")]
        public string ImageFileName { get; set; }
        
        public byte[] ImageFileData { get; set; }

        [DisplayName("File Extension")]
        public string ImageContentType { get; set; }

        [DisplayName("Archived")]
        public bool Archived { get; set; }

        
        // Navigation properties
        public virtual BTCompany Company { get; set; }
        public virtual BTProjectPriority Priority { get; set; }
        
        public virtual ICollection<BTUser> Members { get; set; } = new HashSet<BTUser>();
        public virtual ICollection<BTTicket> Tickets { get; set; } = new HashSet<BTTicket>();

    }
}
