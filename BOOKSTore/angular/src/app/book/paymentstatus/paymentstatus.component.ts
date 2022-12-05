import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaymentOrderDetailsResponse } from '@proxy/instamojo-api';
import { Payment_ServicesService } from '@proxy/payment';

@Component({
  selector: 'app-paymentstatus',
  templateUrl: './paymentstatus.component.html',
  styleUrls: ['./paymentstatus.component.scss']
})
export class PaymentstatusComponent implements OnInit {

  constructor(private rout: ActivatedRoute, private payment: Payment_ServicesService,) { }

  transaction_id: string;
  paymentstatus:PaymentOrderDetailsResponse;
  ngOnInit(): void {
    this.rout.queryParams.subscribe(params => {
      this.transaction_id = params.transaction_id
    });

    this.payment.callback(this.transaction_id).subscribe({ next: (status) => { this.paymentstatus = status } })

  }

}

