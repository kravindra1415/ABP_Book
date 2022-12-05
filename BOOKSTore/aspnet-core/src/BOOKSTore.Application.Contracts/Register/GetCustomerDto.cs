using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BOOKSTore.Register
{
    public class GetCustomerDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
