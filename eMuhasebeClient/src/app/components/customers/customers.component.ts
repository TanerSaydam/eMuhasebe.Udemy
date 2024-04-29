import { Component, ElementRef, ViewChild } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { CustomerPipe } from '../../pipes/customer.pipe';
import { CustomerModel, CustomerTypes } from '../../models/customer.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-customers',
  standalone: true,
  imports: [SharedModule, CustomerPipe, RouterLink],
  templateUrl: './customers.component.html',
  styleUrl: './customers.component.css'
})
export class CustomersComponent {
  customers: CustomerModel[] = [];
  search:string = "";
  p: number = 1;
  
  customerTypes = CustomerTypes;

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel:CustomerModel = new CustomerModel();
  updateModel:CustomerModel = new CustomerModel();

  constructor(
    private http: HttpService,
    private swal: SwalService
  ){}

  ngOnInit(): void {
    this.getAll();
  }

  getAll(){
    this.http.post<CustomerModel[]>("Customers/GetAll",{},(res)=> {
      this.customers = res;
    });
  }

  create(form: NgForm){    
    if(form.valid){
      this.http.post<string>("Customers/Create",this.createModel,(res)=> {
        this.swal.callToast(res);
        this.createModel = new CustomerModel();
        this.createModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  deleteById(model: CustomerModel){
    this.swal.callSwal("Cariyi Sil?",`${model.name} carisini silmek istiyor musunuz?`,()=> {
      this.http.post<string>("Customers/DeleteById",{id: model.id},(res)=> {
        this.getAll();
        this.swal.callToast(res,"info");
      });
    })
  }

  get(model: CustomerModel){
    this.updateModel = {...model};
    this.updateModel.typeValue = this.updateModel.type.value;
  }

  update(form: NgForm){
    if(form.valid){
      this.http.post<string>("Customers/Update",this.updateModel,(res)=> {
        this.swal.callToast(res,"info");
        this.updateModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  } 
}
