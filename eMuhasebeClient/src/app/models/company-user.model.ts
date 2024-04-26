import { CompanyModel } from "./company.model";

export class CompanyUserModel{
    appUserId: string = "";
    companyId: string = "";
    company: CompanyModel = new CompanyModel();
}