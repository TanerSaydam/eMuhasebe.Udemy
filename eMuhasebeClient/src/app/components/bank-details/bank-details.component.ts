import { Component, ElementRef, ViewChild } from '@angular/core';
import { BankModel } from '../../models/bank.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { NgForm } from '@angular/forms';
import { BankDetailModel } from '../../models/bank-detail.model';
import { ActivatedRoute } from '@angular/router';
import { SharedModule } from '../../modules/shared.module';
import { BankDetailPipe } from '../../pipes/bank-detail.pipe';
import { DatePipe } from '@angular/common';
import { CashRegisterModel } from '../../models/cash-register.model';
import { CustomerModel } from '../../models/customer.model';

@Component({
  selector: 'app-bank-details',
  standalone: true,
  imports: [SharedModule, BankDetailPipe],
  templateUrl: './bank-details.component.html',
  styleUrl: './bank-details.component.css',
  providers: [DatePipe]
})
export class BankDetailsComponent {
  bank: BankModel = new BankModel();
  banks: BankModel[] = [];
  cashRegisters: CashRegisterModel[] = [];
  customers: CustomerModel[] = [];
  bankId: string = "";
  search:string = "";
  startDate: string = "";
  endDate: string = "";

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel:BankDetailModel = new BankDetailModel();
  updateModel:BankDetailModel = new BankDetailModel();

  constructor(
    private http: HttpService,
    private swal: SwalService,
    private activated: ActivatedRoute,
    private date: DatePipe
  ){
    this.activated.params.subscribe(res=> {
      this.bankId = res["id"];
      this.startDate = this.date.transform(new Date(), 'yyyy-MM-dd') ?? "";
      this.endDate = this.date.transform(new Date(), 'yyyy-MM-dd') ?? "";
      this.createModel.date = this.date.transform(new Date(), 'yyyy-MM-dd') ?? "";
      this.createModel.bankId = this.bankId;

      this.getAll();
      this.getAllBanks();
      this.getAllCashRegisters();
      this.getAllCustomers();
    })
  }

  getAll(){
    this.http.post<BankModel>("BankDetails/GetAll",
    {bankId: this.bankId, startDate: this.startDate, endDate: this.endDate},(res)=> {
      this.bank = res;
    });
  }

  getAllBanks(){
    this.http.post<BankModel[]>("Banks/GetAll",{},(res)=> {
      this.banks = res.filter(p=> p.id != this.bankId);
    });
  }

  getAllCashRegisters(){
    this.http.post<CashRegisterModel[]>("CashRegisters/GetAll",{},(res)=> {
      this.cashRegisters = res;
    });
  }

  getAllCustomers(){
    this.http.post<CustomerModel[]>("Customers/GetAll",{},(res)=> {
      this.customers = res;
    });
  }

  create(form: NgForm){        
    if(form.valid){
      this.createModel.amount = +this.createModel.amount;
      this.createModel.oppositeAmount = +this.createModel.oppositeAmount;
      
      if(this.createModel.recordType == 0) {
        this.createModel.oppositeBankId = null;  
        this.createModel.oppositeCashRegisterId = null;      
        this.createModel.oppositeCustomerId = null;
      }else if(this.createModel.recordType == 1){
        this.createModel.oppositeCashRegisterId = null;
        this.createModel.oppositeCustomerId = null;
      }else if(this.createModel.recordType == 2){
        this.createModel.oppositeBankId = null; 
        this.createModel.oppositeCustomerId = null;
      }else if(this.createModel.recordType == 3){
        this.createModel.oppositeBankId = null; 
        this.createModel.oppositeCashRegisterId = null;
      }

      if(this.createModel.oppositeAmount === 0) this.createModel.oppositeAmount = this.createModel.amount;

      this.http.post<string>("BankDetails/Create",this.createModel,(res)=> {
        this.swal.callToast(res);
        this.createModel = new BankDetailModel();
        this.createModel.date = this.date.transform(new Date(), 'yyyy-MM-dd') ?? "";
        this.createModel.bankId = this.bankId;

        this.createModalCloseBtn?.nativeElement.click();        
        this.getAll();
      });
    }
  }

  deleteById(model: BankDetailModel){
    this.swal.callSwal("Hasa hareketini Sil?",`${model.date} tarihteki ${model.description} açıklamalı hareketi silmek istiyor musunuz?`,()=> {
      this.http.post<string>("BankDetails/DeleteById",{id: model.id},(res)=> {
        this.getAll();
        this.swal.callToast(res,"info");
      });
    })
  }

  get(model: BankDetailModel){
    this.updateModel = {...model};
    this.updateModel.amount = this.updateModel.depositAmount + this.updateModel.withdrawalAmount;
    this.updateModel.type = this.updateModel.depositAmount > 0 ? 0 : 1;
  }

  update(form: NgForm){    
    if(form.valid){
      this.http.post<string>("BankDetails/Update",this.updateModel,(res)=> {
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

  setOppositeBank(){    
    const bank = this.banks.find(p=> p.id === this.createModel.oppositeBankId);
    
    if(bank){
      this.createModel.oppositeBank = bank;
    }
  }

  setOppositeCash(){    
    const cash = this.cashRegisters.find(p=> p.id === this.createModel.oppositeCashRegisterId);
    
    if(cash){
      this.createModel.oppositeCash = cash;
    }
  }
}
