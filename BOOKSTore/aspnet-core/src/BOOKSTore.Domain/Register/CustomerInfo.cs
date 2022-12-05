using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace BOOKSTore.Register
{
    public class CustomerInfo : AuditedAggregateRoot<Guid>
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string CourseName { get; set; }

    }
}
