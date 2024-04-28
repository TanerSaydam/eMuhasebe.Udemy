import { Pipe, PipeTransform } from '@angular/core';
import { CustomerDetailModel } from '../models/customer-detail.model';

@Pipe({
  name: 'customerDetail',
  standalone: true
})
export class CustomerDetailPipe implements PipeTransform {

  transform(value: CustomerDetailModel[], search:string): CustomerDetailModel[] {
    if(!search) return value;

    return value.filter(p=> 
      p.description.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    );
  }

}
