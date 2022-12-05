import { Component, OnInit } from '@angular/core';
import { EmailService } from '@proxy/email';
import { iemail } from './iemail';

@Component({
  selector: 'app-email-sender',
  templateUrl: './email-sender.component.html',
  styleUrls: ['./email-sender.component.scss']
})
export class EmailSenderComponent implements OnInit {
admin:iemail={
  emailToId: '',
  emailToName: '',
  emailSubject: '',
  emailBody: ''
}
  constructor(
    private email: EmailService
   
  ) {}

  ngOnInit(): void {
  }
sendEmail()
{
this.email.sendEmailByEmailData(this.admin).subscribe(data=>console.log(data))
alert("Send Email")
}
}
