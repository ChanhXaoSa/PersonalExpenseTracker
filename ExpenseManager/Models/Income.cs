using ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class Income : BaseAuditableEntity
    {
        [ForeignKey("ApplicationUser")]
        public required string UserId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string? Sourse { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
