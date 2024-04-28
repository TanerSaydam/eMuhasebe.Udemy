import { ProductModel } from "./product.model";

export class InvoiceDetailModel{
    id: string = "";
    invoiceId: string = "";
    productId: string = "";
    product: ProductModel = new ProductModel();
    quantity: number = 0;
    price: number = 0;
}