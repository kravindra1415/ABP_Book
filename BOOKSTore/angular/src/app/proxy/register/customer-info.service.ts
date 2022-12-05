import type { CreateUpdateCustomerDto, CustomerInfoDto, GetCustomerDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { EmailData } from '../email/models';

@Injectable({
  providedIn: 'root',
})
export class CustomerInfoService {
  apiName = 'Default';
  

  create = (input: CreateUpdateCustomerDto) =>
    this.restService.request<any, CustomerInfoDto>({
      method: 'POST',
      url: '/api/app/customer-info',
      body: input,
    },
    { apiName: this.apiName });
  

  getCustomersByInput = (input: GetCustomerDto) =>
    this.restService.request<any, PagedResultDto<CustomerInfoDto>>({
      method: 'GET',
      url: '/api/app/customer-info/customers',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  sendEmailByEmailData = (emailData: EmailData) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/customer-info/send-email',
      body: emailData,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
