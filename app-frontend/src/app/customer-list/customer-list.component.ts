import { Component, OnInit, ViewChild } from '@angular/core';
import { CustomerService } from '../services/customer.service';
import { CustomerToReturnDto } from '../models/CustomerToReturnDto';
import { SubmitCustomerComponent } from '../submit-customer/submit-customer.component';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cutomer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.scss']
})
export class CustomerListComponent implements OnInit {
  customerList!: CustomerToReturnDto[];
  formBuilder: any; 
  @ViewChild(SubmitCustomerComponent) submitCustomerComponent!: SubmitCustomerComponent;

  constructor(private customerService: CustomerService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadListCustomers(1);

  }

  loadListCustomers(input:any) {
    this.customerService.getAllCustomers().subscribe({
      next: customerforAccount => {
        this.customerList = customerforAccount;
      }
    });
  }

  editCustomer(customer: CustomerToReturnDto) {
    this.submitCustomerComponent.formDTO={ ...customer };
  }

  deleteCustomer(customerId: number) {
    if(confirm('Are you sure you want to delete this customer?')) {
      this.customerService.DeleteCustomer(customerId).subscribe({
        next: () => {
          this.toastr.success('Customer deleted successfully');
          // Customer deleted successfully, reload the list
          this.loadListCustomers(1);
        },
        error: error => {
          // Handle error scenario
          this.toastr.error('Failed to delete customer', 'Error');
          console.error(error);
        }
      });
    }
  }
}
