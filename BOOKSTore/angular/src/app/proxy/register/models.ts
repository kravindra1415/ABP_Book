import type { AuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdateCustomerDto {
  customerName: string;
  customerEmail?: string;
  startDate?: string;
  endDate?: string;
  courseName: string;
}

export interface CustomerInfoDto extends AuditedEntityDto<string> {
  customerName?: string;
  customerEmail?: string;
  startDate?: string;
  endDate?: string;
  courseName?: string;
}

export interface GetCustomerDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}
