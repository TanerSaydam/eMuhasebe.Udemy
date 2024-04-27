import { BankModel } from "./bank.model";

export class BankDetailModel{
    id: string = ""
    bankId: string = "";
    date: string = "";
    type: number = 0;
    amount: number = 0;
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    bankDetailId: string = "";    
    oppositeBankId: string | any = "";    
    oppositeBank: BankModel = new BankModel();
    description: string = "";
    oppositeAmount: number = 0;
    recordType: number = 0;
}