import { BankModel } from "./bank.model";
import { CashRegisterModel } from "./cash-register.model";

export class BankDetailModel{
    id: string = ""
    bankId: string = "";
    date: string = "";
    type: number = 0;
    amount: number = 0;
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    bankDetailId: string = "";  
    cashRegisterDetailId:string = "";
    customerDetailId :string = "";
    oppositeCashRegisterId: string | any = "";
    oppositeBankId: string | any = "";    
    oppositeCustomerId: string | any = "";    
    oppositeBank: BankModel = new BankModel();
    oppositeCash: CashRegisterModel = new CashRegisterModel();
    description: string = "";
    oppositeAmount: number = 0;
    recordType: number = 0;
}