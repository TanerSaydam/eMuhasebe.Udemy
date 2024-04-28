import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LayoutsComponent } from './components/layouts/layouts.component';
import { HomeComponent } from './components/home/home.component';
import { inject } from '@angular/core';
import { AuthService } from './services/auth.service';
import { ExamplesComponent } from './components/examples/examples.component';
import { UsersComponent } from './components/users/users.component';
import { ConfirmEmailComponent } from './components/confirm-email/confirm-email.component';
import { CompaniesComponent } from './components/companies/companies.component';
import { CashRegistersComponent } from './components/cash-registers/cash-registers.component';
import { CashRegisterDetailsComponent } from './components/cash-register-details/cash-register-details.component';
import { BanksComponent } from './components/banks/banks.component';
import { BankDetailsComponent } from './components/bank-details/bank-details.component';
import { CustomersComponent } from './components/customers/customers.component';
import { CustomerDetailsComponent } from './components/customer-details/customer-details.component';
import { ProductsComponent } from './components/products/products.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { InvoicesComponent } from './components/invoices/invoices.component';

export const routes: Routes = [
    {
        path: "login",
        component: LoginComponent
    },
    {
        path: "confirm-email/:email",
        component: ConfirmEmailComponent
    },
    {
        path: "",
        component: LayoutsComponent,
        canActivateChild: [()=> inject(AuthService).isAuthenticated()],
        children: [
            {
                path: "",
                component: HomeComponent
            },
            {
                path: "users",
                component: UsersComponent
            },
            {
                path: "companies",
                component: CompaniesComponent
            },
            {
                path: "cash-registers",
                children: [
                    {
                        path: "",
                        component: CashRegistersComponent
                    },
                    {
                        path: "details/:id",
                        component: CashRegisterDetailsComponent
                    }
                ]
                
            },
            {
                path: "banks",
                children: [
                    {
                        path: "",
                        component: BanksComponent
                    },
                    {
                        path: "details/:id",
                        component: BankDetailsComponent
                    }
                ]
            },
            {
                path: "customers",
                children: [
                    {
                        path: "",
                        component: CustomersComponent
                    },
                    {
                        path: "details/:id",
                        component: CustomerDetailsComponent
                    }
                ]
            },
            {
                path: "products",
                children: [
                    {
                        path: "",
                        component: ProductsComponent
                    },
                    {
                        path: "details/:id",
                        component: ProductDetailsComponent
                    }
                ]
            },
            {
                path: "invoices",
                component: InvoicesComponent
            }
        ]
    }
];
