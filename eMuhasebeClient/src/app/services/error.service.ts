import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SwalService } from './swal.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(
    private swal: SwalService
  ) { }

  errorHandler(err:HttpErrorResponse){
    console.log(err);

    switch (err.status) {
      case 0:
        this.swal.callToast("API adresine ulaşılamıyor","error");
        break;        
      
      case 403:
        let errorMessage = "";
        for(const e of err.error.ErrorMessages){
          errorMessage += e + "\n";
        }

        this.swal.callToast(errorMessage,"error");
        break;
    
      case 404:
        this.swal.callToast("API adresi bulunamadı","error")
        break;
        
      case 500:
        this.swal.callToast(err.error.errorMessages[0], "error");
        break;

      
    }    
  }
}
