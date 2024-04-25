import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { api } from '../constants';
import { ResultModel } from '../models/result.model';
import { AuthService } from './auth.service';
import { ErrorService } from './error.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(
    private http: HttpClient,
    private auth: AuthService,
    private error: ErrorService,
    private spinner: NgxSpinnerService
  ) { }

  post<T>(apiUrl:string, body:any, callBack:(res:T)=> void,errorCallBack?:()=> void ){
    this.spinner.show();
    this.http.post<ResultModel<T>>(`${api}/${apiUrl}`,body,{
      headers: {
        "Authorization": "Bearer " + this.auth.token
      }
    }).subscribe({
      next: (res)=> {
        if(res.data){
          callBack(res.data);
          this.spinner.hide();
        }        
      },
      error: (err:HttpErrorResponse)=> {
        this.spinner.hide();
        this.error.errorHandler(err);
        
        if(errorCallBack){
          errorCallBack();
        }
      }
    })
  }
}
