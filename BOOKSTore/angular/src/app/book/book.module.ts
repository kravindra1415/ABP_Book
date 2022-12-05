import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { BookRoutingModule } from './book-routing.module';
import { BookComponent } from './book.component';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { PaymentstatusComponent } from './paymentstatus/paymentstatus.component';

@NgModule({
  declarations: [BookComponent, PaymentstatusComponent],
  imports: [
    BookRoutingModule,
    SharedModule,
    NgbDatepickerModule
  ]
})
export class BookModule { }

