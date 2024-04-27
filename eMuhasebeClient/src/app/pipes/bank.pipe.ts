import { Pipe, PipeTransform } from '@angular/core';
import { BankModel } from '../models/bank.model';

@Pipe({
  name: 'bank',
  standalone: true
})
export class BankPipe implements PipeTransform {

  transform(value: BankModel[], search:string): BankModel[] {
    if(!search) return value;

    return value.filter(p=> 
      p.name.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
      p.iban.includes(search)
    );
  }

}
