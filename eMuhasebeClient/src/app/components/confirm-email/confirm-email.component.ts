import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../../services/http.service';

@Component({
  selector: 'app-confirm-email',
  standalone: true,
  imports: [],
  template: `
    <div style="height: 90vh; display:flex; align-items:center; justify-content:center; flex-direction:column">
      <h1>{{response}}</h1>      
      <a href="/login">Giriş sayfasına dönmek için tıklayın</a>
    </div>
  `
})
export class ConfirmEmailComponent {
  email: string = "";
  response: string = "Mail adresiniz onaylanıyor...";

  constructor(
    private activated: ActivatedRoute,
    private http: HttpService
  ) {

    this.activated.params.subscribe(res => {
      this.email = res["email"];
      this.confirm();
    });
  }

  confirm() {
    this.http.post<string>("Auth/ConfirmEmail", { email: this.email }, (res) => {
      this.response = res;
    });
  }
}
