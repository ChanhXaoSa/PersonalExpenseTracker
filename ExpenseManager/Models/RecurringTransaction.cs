using ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class RecurringTransaction : BaseAuditableEntity
    {
        [ForeignKey("ApplicationUser")]
        public required string UserId { get; set; }
        public required string Type { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public required string Frequency { get; set; }
        public DateTime NextOrcurrence { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
