import { Pipe, PipeTransform } from '@angular/core';
import { ProductDetailModel } from '../models/product-detail.model';

@Pipe({
  name: 'productDetail',
  standalone: true
})
export class ProductDetailPipe implements PipeTransform {

  transform(value: ProductDetailModel[], search:string): ProductDetailModel[] {
    if(!search) return value;

    return value.filter(p=> 
      p.description.toLocaleLowerCase().includes(search.toLocaleLowerCase())
    );
  }

}
