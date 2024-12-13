import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Books } from '../types/Books';
import { URL } from '../url.base';
import { CommonModule, NgFor } from '@angular/common';
import { ModalComponent } from "../modal/modal/modal.component";
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-listar',
  imports: [NgFor, CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './listar.component.html',
  styleUrl: './listar.component.css'
})
export class ListarComponent {

  constructor(public http:HttpClient, private fb: FormBuilder, public toastr: ToastrService) {
    
    this.formGroup =
     this.fb.group({ id: [0], title: [''], description: [''], pageCount: [0], excerpt: [''], publishDate: [''] }); 
     this.loadData();
  }
  formValues: any;
  public formGroup:FormGroup;
  public books:Books[] =[];
  public bookData:Books[] = [];
  public selectedBook:Books = {
    id:0,
    title:"",
    description:"",
    pageCount: 0,
    excerpt: "",
    publishDate: ""
  }
  public name:string="add";
  public index:number = 0;
  
    public loadData():void{
      this.formGroup.valueChanges.subscribe(values => { this.formValues = values; });
      this.http.get<Books[]>(URL + "/get")
      .subscribe((b:Books[])=> 
        {
          this.books = b
          this.sliceBooks();
        }); 
    }

    onSubmit($event:Event):void{}

    clearForm() { this.formGroup.reset({ id: 0, title: '', description: '', pageCount: 0, excerpt: '', publishDate: '' }); }
    
    allFieldsHaveValue(): boolean {
       const formValues = this.formGroup.value; 
       return Object.keys(formValues).every(key => { 
        if (key === 'id') { return true; }
        const value = formValues[key]; 
        if (typeof value === 'number') { return value >= 0; // Los valores numéricos deben ser mayores o iguales a 0
         } return value !== null && value !== ''; // Los valores no numéricos no deben estar vacíos 
      }); 
    }
          
    public add():void{
      this.name = "add";
      this.clearForm();
      
    }
    public edit():void{
      this.name ="edit";
    }
    public del():void{
      if(this.selectedBook.id > 0 ){
        this.http.delete(URL + "/delete/"+ this.selectedBook.id)
        .subscribe(r=>
        {
          this.toastr.warning("El libro con el id " + this.selectedBook.id + " ha sido eliminado!")
        }
        );
      }else{
        this.toastr.error("Seleccione un libro para ser eliminado!")
      }
    }

    public save():void{
        if(this.name == "add")
        {
          if(this.allFieldsHaveValue()){
          const formValues = this.formGroup.value; 
          formValues.id = 0;
          const jsonString = JSON.stringify(formValues);
          this.http.post(URL + "/add", {params:jsonString })
          .subscribe({ next: (response: any) => {
            if (response)
              { 
               this.toastr.success('Cambios guardados!', 'Excelente!');
              } }, 
            error: (error) => { console.error('Error en la petición:', error); } 
           });
          
          }else{
            this.toastr.error("Verifique los campos por favor!")
          }
        }
        else if(this.name =="edit")
        {
          if(this.allFieldsHaveValue()){
            const formValues = this.formGroup.value; 
          const jsonString = JSON.stringify(formValues);
          this.http.put(URL + "/update"+ this.selectedBook.id, {params:jsonString })
          .subscribe({ next: (response: any) => {
            if (response)
              { 
               this.toastr.success('Cambios guardados!', 'Excelente!');
              } }, 
            error: (error) => { console.error('Error en la petición:', error); } 
           });
          
          }else{
            this.toastr.error("Verifique los campos por favor!")
          }
        }
    }
 

    public selectBook(obj:Books):void{
      this.selectedBook =  obj;
      this.formGroup.get("id")?.setValue(obj.id);
      this.formGroup.get("title")?.setValue(obj.title);
      this.formGroup.get("description")?.setValue(obj.description); 
      this.formGroup.get("pageCount")?.setValue(obj.pageCount);
      this.formGroup.get("excerpt")?.setValue(obj.excerpt); 
      this.formGroup.get("publishDate")?.setValue(obj.publishDate.split("T")[0]);
    }
    public sliceBooks():void{
      this.bookData = this.books.slice(this.index * 10, (this.index + 1) * 10);
    }
    public next():void{
      if(this.books.length > ((this.index + 1) * 10))
        {
          this.index ++;
          this.sliceBooks();
        }
    }
    public before():void{
      if(this.index > 0)
      {
        this.index --;
        this.sliceBooks();
      }
    }

}
