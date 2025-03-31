using ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class Budget : BaseAuditableEntity
    {
        [ForeignKey("ApplicationUser")]
        public required string UserId { get; set; }
        [ForeignKey("Category")]
        public required Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
