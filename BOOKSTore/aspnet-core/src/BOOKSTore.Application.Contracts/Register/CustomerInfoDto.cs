using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BOOKSTore.Register
{
    public class CustomerInfoDto : AuditedEntityDto<Guid>
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string CourseName { get; set; }
    }
}
