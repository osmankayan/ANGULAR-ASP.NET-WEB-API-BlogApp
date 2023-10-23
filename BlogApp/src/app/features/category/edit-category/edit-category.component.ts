import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { listcategory } from '../models/list-category.model';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit,OnDestroy {
  constructor(private route:ActivatedRoute,private categoryservice:CategoryService,private router:Router){}
  
  subscribe?:Subscription;
  editCategorySubscription?:Subscription;
  

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

  onFormSubmit():void{
    const updateCategoryRequest:UpdateCategoryRequest={
      name:this.category?.name ?? '',
      urlHandle:this.category?.urlHandle ?? ''
    }
    //pass these to service
    if(this.id)
    this.editCategorySubscription= this.categoryservice.UpdateCategory(this.id,updateCategoryRequest).subscribe({
    next:(response)=>{
    this.router.navigateByUrl('/admin/categories')
    }
    })
  }

  ngOnDestroy(): void {
    this.subscribe?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
  }

}
