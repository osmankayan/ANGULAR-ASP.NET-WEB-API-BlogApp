import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { listcategory } from '../models/list-category.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit,OnDestroy {
  constructor(private route:ActivatedRoute,private categoryservice:CategoryService){}
  
  subscribe?:Subscription;

  id:string | null= null; //routeden aldıgımız ile aynı adda olmalı

  category?:listcategory

  ngOnInit(): void {
   this.subscribe= this.route.paramMap.subscribe({

      next:(params)=>{
        this.id=params.get('id');
        if(this.id)
        {

          this.categoryservice.ListCategoryById(this.id).subscribe({

            next:(response)=>{
              this.category=response
            }
          })

        }
      }
    })
  }

  onFromSubmit():void{
    console.log(this.category)
  }

  ngOnDestroy(): void {
    this.subscribe?.unsubscribe();
  }

}
