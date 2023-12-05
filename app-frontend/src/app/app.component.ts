import { Component } from '@angular/core';
import { CustomerToReturnDto } from './models/CustomerToReturnDto';
import { CustomerService } from './services/customer.service';
import { SubmitCustomerComponent } from './submit-customer/submit-customer.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent   {
  title = 'accounting';
  
  constructor(private customerService: CustomerService) { }

}
