using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Pin { get; set; }
    }
}
