import { CompanyUserModel } from "./company-user.model";

export class UserModel{
    id: string = "";
    name: string = "";
    firstName: string = "";
    lastName: string = "";
    fullName: string = "";
    password: string | null = "";
    userName: string = "";
    email: string = "";
    companyIds: string[] = []; 
    companyUsers: CompanyUserModel[] = [];
}