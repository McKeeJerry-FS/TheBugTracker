using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TheBugTracker.Models
{
    public class BTTicketStatus
    {
        public int Id { get; set; }
        
        [DisplayName("Status Name")]
        public string Name { get; set; }
    }
}
