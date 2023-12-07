import { Component,OnInit,Input, ViewChild, EventEmitter  } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CustomerDto } from '../models/CustomerDto';
import { CustomerService } from '../services/customer.service';
import { Output } from '@angular/core';
import { CustomerToReturnDto } from '../models/CustomerToReturnDto';

@Component({
  selector: 'app-submit-customer',
  templateUrl: './submit-customer.component.html'
})
export class SubmitCustomerComponent implements OnInit  {
  formDTO: CustomerDto = <CustomerDto>{};

  @ViewChild("customerForm")
  customerForm!: NgForm;
  isSubmitted: boolean = false;
  @Output() loadList = new EventEmitter<any>();

  constructor(private toastr: ToastrService, private customerService: CustomerService) {

  }

  ngOnInit() {
  }

clearForm()
{
  this.formDTO=<CustomerDto>{};
}
  submitForm(isValid:any) {
    this.isSubmitted = true;
    if (isValid) { 
      if(this.formDTO.id!>0)
      {
        this.customerService.UpdateCustomer(this.formDTO)
        .subscribe({
          next: (item) => {
           
              this.toastr.success('customer submitted successfully!', 'Success');
              this.loadList.emit(1);
              this.formDTO = <CustomerDto>{};
            
          },
          error: (error) => {
            // Handle the error message
            this.toastr.error(error.message, 'Error');
          }
        })
      }
      else
      {
        this.formDTO.id=0;
      this.customerService.CreateCustomer(this.formDTO)
      .subscribe({
        next: (customer: CustomerDto) => {
          if (customer) {
            this.toastr.success('Customer submitted successfully!', 'Success');
            this.loadList.emit(customer);
            this.formDTO = <CustomerDto>{};
          } else {
            // This "else" block likely won't execute because observable's "next" usually signifies success
            this.toastr.error('Unexpected error', 'Error');
          }
        }
      })
    }
  }
    else{
      this.toastr.error('Please fill in all required fields and enter a valid fields.', 'Error');
      return;
    }
  }
  

}


