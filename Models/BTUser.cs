using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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




    }
}
