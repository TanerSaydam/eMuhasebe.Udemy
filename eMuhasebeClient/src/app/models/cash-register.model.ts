import { CurrencyTypeModel } from "./currency-type.model";

export class CashRegisterModel{
    id: string = "";
    name: string = "";
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    balanceAmount: number = 0;
    currencyType: CurrencyTypeModel = new CurrencyTypeModel();
    currencyTypeValue: number = 1;
}