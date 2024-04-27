import { Pipe, PipeTransform } from '@angular/core';
import { CashRegisterDetailModel } from '../models/cash-register-detail.model';

@Pipe({
  name: 'cashRegisterDetail',
  standalone: true
})
export class CashRegisterDetailPipe implements PipeTransform {

  transform(value: CashRegisterDetailModel[], search:string): CashRegisterDetailModel[] {
    if(!search) return value;

    return value.filter(p=> 
      p.description.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    );
  }

}
