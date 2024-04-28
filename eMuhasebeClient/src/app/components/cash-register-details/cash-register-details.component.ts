import { Component, ElementRef, ViewChild } from '@angular/core';
import { CashRegisterModel } from '../../models/cash-register.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { NgForm } from '@angular/forms';
import { CashRegisterDetailModel } from '../../models/cash-register-detail.model';
import { ActivatedRoute } from '@angular/router';
import { SharedModule } from '../../modules/shared.module';
import { CashRegisterDetailPipe } from '../../pipes/cash-register-detail.pipe';
import { DatePipe } from '@angular/common';
import { BankModel } from '../../models/bank.model';
import { CustomerModel } from '../../models/customer.model';

@Component({
  selector: 'app-cash-register-details',
  standalone: true,
  imports: [SharedModule, CashRegisterDetailPipe],
  templateUrl: './cash-register-details.component.html',
  styleUrl: './cash-register-details.component.css',
  providers: [DatePipe]
})
export class CashRegisterDetailsComponent {
  cashRegister: CashRegisterModel = new CashRegisterModel();
  cashRegisters: CashRegisterModel[] = [];
  banks: BankModel[] = [];
  customers: CustomerModel[] = [];
  cashRegisterId: string = "";
  search:string = "";
  startDate: string = "";
  endDate: string = "";

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel:CashRegisterDetailModel = new CashRegisterDetailModel();
  updateModel:CashRegisterDetailModel = new CashRegisterDetailModel();

  constructor(
    private http: HttpService,
    private swal: SwalService,
    private activated: ActivatedRoute,
    private date: DatePipe
  ){
    this.activated.params.subscribe(res=> {
      this.cashRegisterId = res["id"];
      this.startDate = this.date.transform(new Date(), 'yyyy-MM-dd') ?? "";
      this.endDate = this.date.transform(new Date(), 'yyyy-MM-dd') ?? "";
      this.createModel.date = this.date.transform(new Date(), 'yyyy-MM-dd') ?? "";
      this.createModel.cashRegisterId = this.cashRegisterId;

      this.getAll();
      this.getAllCashRegisters();
      this.getAllBanks();
      this.getAllCustomers();
    })
  }

  getAll(){
    this.http.post<CashRegisterModel>("CashRegisterDetails/GetAll",
    {cashRegisterId: this.cashRegisterId, startDate: this.startDate, endDate: this.endDate},(res)=> {
      this.cashRegister = res;
    });
  }

  getAllCashRegisters(){
    this.http.post<CashRegisterModel[]>("CashRegisters/GetAll",{},(res)=> {
      this.cashRegisters = res.filter(p=> p.id != this.cashRegisterId);
    });
  }

  getAllBanks(){
    this.http.post<BankModel[]>("Banks/GetAll",{},(res)=> {
      this.banks = res;
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
        this.createModel.oppositeBankId = null;
        this.createModel.oppositeCustomerId = null;      
      }else if(this.createModel.recordType == 2){
        this.createModel.oppositeCashRegisterId = null; 
        this.createModel.oppositeCustomerId = null;      
      }else if(this.createModel.recordType == 3){
        this.createModel.oppositeCashRegisterId = null; 
        this.createModel.oppositeBankId = null;  
      }

      if(this.createModel.oppositeAmount === 0) this.createModel.oppositeAmount = this.createModel.amount;
      
      this.http.post<string>("CashRegisterDetails/Create",this.createModel,(res)=> {
        this.swal.callToast(res);
        this.createModel = new CashRegisterDetailModel();
        this.createModel.date = this.date.transform(new Date(), 'yyyy-MM-dd') ?? "";
        this.createModel.cashRegisterId = this.cashRegisterId;

        this.createModalCloseBtn?.nativeElement.click();        
        this.getAll();
      });
    }
  }

  deleteById(model: CashRegisterDetailModel){
    this.swal.callSwal("Hasa hareketini Sil?",`${model.date} tarihteki ${model.description} açıklamalı hareketi silmek istiyor musunuz?`,()=> {
      this.http.post<string>("CashRegisterDetails/DeleteById",{id: model.id},(res)=> {
        this.getAll();
        this.swal.callToast(res,"info");
      });
    })
  }

  get(model: CashRegisterDetailModel){
    this.updateModel = {...model};
    this.updateModel.amount = this.updateModel.depositAmount + this.updateModel.withdrawalAmount;
    this.updateModel.type = this.updateModel.depositAmount > 0 ? 0 : 1;
  }

  update(form: NgForm){    
    if(form.valid){
      this.http.post<string>("CashRegisterDetails/Update",this.updateModel,(res)=> {
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

  setOppositeCashRegister(){    
    const cash = this.cashRegisters.find(p=> p.id === this.createModel.oppositeCashRegisterId);
    
    if(cash){
      this.createModel.oppositeCashRegister = cash;
    }
  }

  setOppositeBank(){
    const bank = this.banks.find(p=> p.id === this.createModel.oppositeBankId);
    
    if(bank){
      this.createModel.oppositeBank = bank;
    }
  }
}
