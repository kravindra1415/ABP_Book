import type { EmailData } from './models';
import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EmailService {
  apiName = 'Default';
  

  sendEmailByEmailData = (emailData: EmailData) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/app/email/send-email',
      body: emailData,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
