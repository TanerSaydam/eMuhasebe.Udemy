import { Component, ElementRef, ViewChild } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { LoginModel } from '../../models/login.model';
import { HttpService } from '../../services/http.service';
import { LoginResponseModel } from '../../models/login.response.model';
import { Router } from '@angular/router';
import { SwalService } from '../../services/swal.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  model: LoginModel = new LoginModel();
  email:string = "";

  @ViewChild("sendConfirmEmailModalCloseBtn") sendConfirmEmailModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  constructor(
    private http: HttpService,
    private swal: SwalService,
    private router: Router
  ){}

  signIn(){
    this.http.post<LoginResponseModel>("Auth/Login",this.model,(res)=> {
      localStorage.setItem("token", res.token);
      this.router.navigateByUrl("/");
    });
  }

  sendConfirmEmail(){
    this.http.post<string>("Auth/SendConfirmEmail",{email: this.email},(res)=> {
      this.swal.callToast(res,"info");
      this.sendConfirmEmailModalCloseBtn?.nativeElement.click();
      this.email = "";
    });
  }
}
