import { Pipe, PipeTransform } from '@angular/core';
import { CompanyModel } from '../models/company.model';

@Pipe({
  name: 'company',
  standalone: true
})
export class CompanyPipe implements PipeTransform {

  transform(value: CompanyModel[], search:string): CompanyModel[] {
    if(!search) return value;

    return value.filter(p=> 
      p.name.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
      p.fullAddress.toLocaleLowerCase().includes(search.toLocaleLowerCase()) || 
      p.taxDepartment.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
      p.taxNumber.toString().includes(search) ||
      p.database.server.toLocaleLowerCase().includes(search.toLocaleLowerCase()) ||
      p.database.databaseName.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    );
  }

}
