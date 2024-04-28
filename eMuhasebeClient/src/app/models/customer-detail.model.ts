export class CustomerDetailModel{
    id: string = "";
    type: CustomerDetailTypeEnum = new CustomerDetailTypeEnum();
    date: string = "";
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    description: string = "";
    bankDetailId: string = "";
}

export class CustomerDetailTypeEnum{
    name: string = "";
    value: number = 0;
}