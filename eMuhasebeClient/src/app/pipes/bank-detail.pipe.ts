import { Pipe, PipeTransform } from '@angular/core';
import { BankDetailModel } from '../models/bank-detail.model';

@Pipe({
  name: 'bankDetail',
  standalone: true
})
export class BankDetailPipe implements PipeTransform {

  transform(value: BankDetailModel[], search:string): BankDetailModel[] {
    if(!search) return value;

    return value.filter(p=> 
      p.description.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    );
  }

}
