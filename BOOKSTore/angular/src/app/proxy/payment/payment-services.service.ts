import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { Instamojo, PaymentOrderDetailsResponse } from '../instamojo-api/models';
import type { Paymentreturn } from '../payment/models';

@Injectable({
  providedIn: 'root',
})
export class Payment_ServicesService {
  apiName = 'Default';
  

  callback = (tranid: string) =>
    this.restService.request<any, PaymentOrderDetailsResponse>({
      method: 'POST',
      url: '/api/app/payment_Services/callback',
      params: { tranid },
    },
    { apiName: this.apiName });
  

  createPaymentOrderByObjClassAndID = (objClass: Instamojo, ID: string) =>
    this.restService.request<any, Paymentreturn>({
      method: 'POST',
      url: '/api/app/payment_Services/payment-order',
      params: { id: ID },
      body: objClass,
    },
    { apiName: this.apiName });
  

  start = (id: string) =>
    this.restService.request<any, Paymentreturn>({
      method: 'POST',
      url: `/api/app/payment_Services/${id}/start`,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
