import { Component, ElementRef, ViewChild } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { InvoicePipe } from '../../pipes/invoice.pipe';
import { InvoiceModel } from '../../models/invoice.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { NgForm } from '@angular/forms';
import { CustomerModel } from '../../models/customer.model';
import { ProductModel } from '../../models/product.model';
import { InvoiceDetailModel } from '../../models/invoice-detail.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-invoices',
  standalone: true,
  imports: [SharedModule, InvoicePipe],
  templateUrl: './invoices.component.html',
  styleUrl: './invoices.component.css',
  providers: [DatePipe]
})
export class InvoicesComponent {
  invoices: InvoiceModel[] = [];
  customers: CustomerModel[] = [];
  products: ProductModel[] = [];
  search:string = "";
  p: number = 1;
  
  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel:InvoiceModel = new InvoiceModel();
  updateModel:InvoiceModel = new InvoiceModel();

  constructor(
    private http: HttpService,
    private swal: SwalService,
    private date: DatePipe
  ){
    this.createModel.date = this.date.transform(new Date(),"yyyy-MM-dd") ?? "";
  }

  ngOnInit(): void {
    this.getAll();
    this.getAllCustomers();
    this.getAllProducts();
  }

  getAll(){
    this.http.post<InvoiceModel[]>("Invoices/GetAll",{},(res)=> {
      this.invoices = res;
    });
  }

  getAllCustomers(){
    this.http.post<CustomerModel[]>("Customers/GetAll",{},(res)=> {
      this.customers = res;
    });
  }

  getAllProducts(){
    this.http.post<ProductModel[]>("Products/GetAll",{},(res)=> {
      this.products = res;
    });
  }

  create(form: NgForm){
    if(form.valid){
      this.http.post<string>("Invoices/Create",this.createModel,(res)=> {
        this.swal.callToast(res);
        this.createModel = new InvoiceModel();
        this.createModel.date = this.date.transform(new Date(),"yyyy-MM-dd") ?? "";
        this.createModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  deleteById(model: InvoiceModel){
    this.swal.callSwal("Faturayı Sil?",`${model.invoiceNumber} numaralı faturayı silmek istiyor musunuz?`,()=> {
      this.http.post<string>("Invoices/DeleteById",{id: model.id},(res)=> {
        this.getAll();
        this.swal.callToast(res,"info");
      });
    })
  }

  get(model: InvoiceModel){
    this.updateModel = {...model};
    this.updateModel.typeValue = this.updateModel.type.value;
  }

  update(form: NgForm){
    if(form.valid){
      this.http.post<string>("Invoices/DeleteById",{id: this.updateModel.id},(res)=> {
        this.http.post<string>("Invoices/Create",this.updateModel,(res)=> {
          this.swal.callToast(res, "info");          
          this.updateModalCloseBtn?.nativeElement.click();
          this.getAll();
        });
      });
    }
  }

  addDetail(){
    const detail: InvoiceDetailModel = {
      price: this.createModel.price,
      quantity: this.createModel.quantity,
      productId: this.createModel.productId,
      id: "",
      invoiceId: "",
      product: this.products.find(p=> p.id == this.createModel.productId) ?? new ProductModel()
    };

    this.createModel.details.push(detail);

    this.createModel.productId = "";
    this.createModel.quantity = 0;
    this.createModel.price = 0;
  }

  removeDetailItem(index: number){
    this.createModel.details.splice(index,1);
  }

  addDetailForUpdate(){
    const detail: InvoiceDetailModel = {
      price: this.updateModel.price,
      quantity: this.updateModel.quantity,
      productId: this.updateModel.productId,
      id: "",
      invoiceId: "",
      product: this.products.find(p=> p.id == this.updateModel.productId) ?? new ProductModel()
    };

    this.updateModel.details.push(detail);

    this.updateModel.productId = "";
    this.updateModel.quantity = 0;
    this.updateModel.price = 0;
  }

  removeDetailItemForUpdate(index: number){
    this.updateModel.details.splice(index,1);
  }
}
