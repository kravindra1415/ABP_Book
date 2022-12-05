import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookComponent } from './book.component';
import { PaymentstatusComponent } from './paymentstatus/paymentstatus.component';

const routes: Routes = [{ path: '', component: BookComponent, canActivate: [AuthGuard, PermissionGuard] },
{ path: 'paymentstatus', component: PaymentstatusComponent, canActivate: [AuthGuard, PermissionGuard] }


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookRoutingModule { }
