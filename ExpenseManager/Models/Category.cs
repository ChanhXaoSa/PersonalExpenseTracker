using ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class Category : BaseAuditableEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("ApplicationUser")]
        public string? UserId { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
