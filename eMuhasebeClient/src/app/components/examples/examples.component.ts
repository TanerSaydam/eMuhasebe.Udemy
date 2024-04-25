import { Component, ElementRef, ViewChild } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { ExamplePipe } from '../../pipes/example.pipe';
import { ExampleModel } from '../../models/example.model';
import { HttpService } from '../../services/http.service';
import { SwalService } from '../../services/swal.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-examples',
  standalone: true,
  imports: [SharedModule, ExamplePipe],
  templateUrl: './examples.component.html',
  styleUrl: './examples.component.css'
})
export class ExamplesComponent {
  examples: ExampleModel[] = [];
  search:string = "";

  @ViewChild("createModalCloseBtn") createModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModalCloseBtn") updateModalCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  createModel:ExampleModel = new ExampleModel();
  updateModel:ExampleModel = new ExampleModel();

  constructor(
    private http: HttpService,
    private swal: SwalService
  ){}

  ngOnInit(): void {
    this.getAll();
  }

  getAll(){
    this.http.post<ExampleModel[]>("Examples/GetAll",{},(res)=> {
      this.examples = res;
    });
  }

  create(form: NgForm){
    if(form.valid){
      this.http.post<string>("Examples/Create",this.createModel,(res)=> {
        this.swal.callToast(res);
        this.createModel = new ExampleModel();
        this.createModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }

  deleteById(model: ExampleModel){
    this.swal.callSwal("Veriyi Sil?",`${model.field1} verisini silmek istiyor musunuz?`,()=> {
      this.http.post<string>("Examples/DeleteById",{id: model.id},(res)=> {
        this.getAll();
        this.swal.callToast(res,"info");
      });
    })
  }

  get(model: ExampleModel){
    this.updateModel = {...model};
  }

  update(form: NgForm){
    if(form.valid){
      this.http.post<string>("Examples/Update",this.updateModel,(res)=> {
        this.swal.callToast(res,"info");
        this.updateModalCloseBtn?.nativeElement.click();
        this.getAll();
      });
    }
  }
}
