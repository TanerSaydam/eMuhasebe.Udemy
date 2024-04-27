import { CashRegisterModel } from "./cash-register.model";

export class CashRegisterDetailModel{
    id: string = ""
    cashRegisterId: string = "";
    date: string = "";
    type: number = 0;
    amount: number = 0;
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    cashRegisterDetailId: string = "";    
    oppositeCashRegisterId: string | any = "";    
    oppositeCashRegister: CashRegisterModel = new CashRegisterModel();
    description: string = "";
    oppositeAmount: number = 0;
    recordType: number = 0;
}