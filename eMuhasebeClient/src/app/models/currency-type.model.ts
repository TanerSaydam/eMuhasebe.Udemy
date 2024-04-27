export class CurrencyTypeModel{
    name: string = "";
    value: number = 1;
}

export const CurrencyTypes: CurrencyTypeModel[] = [
    {
        name: "TL",
        value: 1
    },
    {
        name: "USD",
        value: 2
    },
    {
        name: "EURO",
        value: 3
    }
]