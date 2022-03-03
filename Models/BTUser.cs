using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheBugTracker.Models
{
    public class BTUser : IdentityUser
    {
        [Required] // States that the item is required for adding a new record to the Database
        [Display(Name = "First Name")] // This annotation is for the "View"
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [NotMapped] // This particular property will not be added to the Database
        [Display(Name = "Full Name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        // Avatar properties
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile AvatarFormFile { get; set; }

        [DisplayName("Avatar")]
        public string AvatarFileName { get; set; }

        public byte[] AvatarFileData { get; set; }

        [DisplayName("File Extension")]
        public string AvatarContentType { get; set; }


        public int? CompanyId { get; set; } //Foreign Key

        //Naigation properties
        public virtual BTCompany Company { get; set; }
        public virtual ICollection<BTProject> Projects { get; set; }
    }
}
