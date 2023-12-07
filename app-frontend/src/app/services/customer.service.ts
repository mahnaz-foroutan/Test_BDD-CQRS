import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { CustomerToReturnDto } from '../models/CustomerToReturnDto';
import { CustomerDto } from '../models/CustomerDto';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  private handleError(error: HttpErrorResponse) {
    let errorMsg = 'An unknown error occurred!';
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMsg = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      if (error.status === 0) {
        // A network error occurred
        errorMsg = `Network error: Please check your connection and try again.`;
      } else if (error.error?.message) {
        // Here we are assuming the server API will return a JSON object with a 'message' property
        errorMsg = `Error ${error.status}: ${error.error.message}`;
      }
    }

    console.error(errorMsg);
    return throwError(() => new Error(errorMsg));
  }

  getCustomerByIdAsync(customer_id: number) {
    return this.http.get<CustomerToReturnDto>(this.baseUrl + customer_id);
  }

  getAllCustomers() {
    return this.http.get<CustomerToReturnDto[]>(this.baseUrl);
  }

  CreateCustomer(customerDTO: CustomerDto): Observable<CustomerDto> {
    return this.http.post<CustomerDto>(this.baseUrl, customerDTO)
      .pipe(catchError(this.handleError));
    
  }

  UpdateCustomer(customerDTO: CustomerDto): Observable<CustomerDto> {
    return this.http.put<CustomerDto>(this.baseUrl , customerDTO)
    .pipe(catchError(this.handleError));
  }

  DeleteCustomer(customer_id: number) {
    return this.http.delete(this.baseUrl + customer_id)
    .pipe(catchError(this.handleError));
  }

}
