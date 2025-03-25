using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public DateTime Created { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public String? LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        protected BaseAuditableEntity()
        {
            Created = DateTime.Now;
            IsDeleted = false;
        }
    }
}
