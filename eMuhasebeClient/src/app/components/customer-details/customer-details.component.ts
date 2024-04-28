import { Component } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { CustomerDetailPipe } from '../../pipes/customer-detail.pipe';
import { CustomerModel } from '../../models/customer.model';
import { HttpService } from '../../services/http.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-customer-details',
  standalone: true,
  imports: [SharedModule, CustomerDetailPipe],
  templateUrl: './customer-details.component.html',
  styleUrl: './customer-details.component.css'
})
export class CustomerDetailsComponent {
  customer: CustomerModel = new CustomerModel();  
  customerId: string = "";
  search:string = ""; 

  constructor(
    private http: HttpService,    
    private activated: ActivatedRoute
  ){
    this.activated.params.subscribe(res=> {
      this.customerId = res["id"];

      this.getAll();      
    })
  }

  getAll(){
    this.http.post<CustomerModel>("CustomerDetails/GetAll",
    {customerId: this.customerId},(res)=> {
      this.customer = res;
    });
  }  
}
