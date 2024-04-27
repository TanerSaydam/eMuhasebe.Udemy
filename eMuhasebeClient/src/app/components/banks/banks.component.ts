import { Component, ElementRef, ViewChild } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { BankPipe } from '../../pipes/bank.pipe';
import { RouterLink } from '@angular/router';
import { BankModel } from '../../models/bank.model';
import { CurrencyTypes } from '../../models/currency-type.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-banks',
  standalone: true,
  imports: [SharedModule, BankPipe, RouterLink],
  templateUrl: './banks.component.html',
  styleUrl: './banks.component.css'
})
export class BanksComponent {
  banks: BankModel[] = [];
  search:string = "";

  currencyTypes = CurrencyTypes;

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel:BankModel = new BankModel();
  updateModel:BankModel = new BankModel();

  constructor(
    private http: HttpService,
    private swal: SwalService
  ){}

  ngOnInit(): void {
    this.getAll();
  }

  getAll(){
    this.http.post<BankModel[]>("Banks/GetAll",{},(res)=> {
      this.banks = res;
    });
  }

  create(form: NgForm){    
    if(form.valid){
      this.http.post<string>("Banks/Create",this.createModel,(res)=> {
        this.swal.callToast(res);
        this.createModel = new BankModel();
        this.createModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  deleteById(model: BankModel){
    this.swal.callSwal("Bankayı Sil?",`${model.name} bankasını silmek istiyor musunuz?`,()=> {
      this.http.post<string>("Banks/DeleteById",{id: model.id},(res)=> {
        this.getAll();
        this.swal.callToast(res,"info");
      });
    })
  }

  get(model: BankModel){
    this.updateModel = {...model};
    this.updateModel.currencyTypeValue = this.updateModel.currencyType.value;
  }

  update(form: NgForm){
    if(form.valid){
      this.http.post<string>("Banks/Update",this.updateModel,(res)=> {
        this.swal.callToast(res,"info");
        this.updateModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  changeCurrencyNameToSymbol(name: string){
    if(name === "TL") return "₺";
    else if(name === "USD") return "$";
    else if(name === "EURO") return "€";
    else return "";
  }
}
