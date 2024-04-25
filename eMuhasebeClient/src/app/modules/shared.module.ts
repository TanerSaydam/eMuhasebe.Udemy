import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlankComponent } from '../components/blank/blank.component';
import { SectionComponent } from '../components/section/section.component';
import { FormsModule } from '@angular/forms';
import { TrCurrencyPipe } from 'tr-currency';
import { FormValidateDirective } from 'form-validate-angular';



@NgModule({
  declarations: [    
  ],
  imports: [
    CommonModule,
    BlankComponent, 
    SectionComponent,
    FormsModule,
    TrCurrencyPipe,
    FormValidateDirective
  ],
  exports: [
    CommonModule,
    BlankComponent, 
    SectionComponent,
    FormsModule,
    TrCurrencyPipe,
    FormValidateDirective
  ]
})
export class SharedModule { }
