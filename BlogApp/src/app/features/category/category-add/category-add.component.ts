import { Component, OnDestroy } from '@angular/core';
import { addCategoryRequest } from 'src/app/features/category/models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category-add',
  templateUrl: './category-add.component.html',
  styleUrls: ['./category-add.component.css']
})
export class CategoryAddComponent {
  model:addCategoryRequest
  private addCategorySubscription?:Subscription
  constructor(private categoryService:CategoryService, private router:Router)
  {
    this.model=
    {
      name:'...',
      urlHandle:''
    };
  }
 
  onFormSubmit()
  {
    this.addCategorySubscription= this.categoryService.addCategory(this.model).subscribe({
      next: (response)=>{ console.log('succesfull')
      this.router.navigateByUrl('/admin/categories')
    }
      
    })
   
  }

  ngOnDestroy(): void {
    this.addCategorySubscription?.unsubscribe()
  }

}
