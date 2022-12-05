import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { EmailData } from '../email/models';

@Injectable({
  providedIn: 'root',
})
export class EmailService {
  apiName = 'Default';
  

  sendEmailByEmailData = (emailData: EmailData) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/Email',
      body: emailData,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
