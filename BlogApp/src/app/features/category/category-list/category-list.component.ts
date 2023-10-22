import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { listcategory } from '../models/list-category.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {

  list$?:Observable<listcategory[]>
  constructor(private service:CategoryService){}

  ngOnInit(): void {
  this.list$= this.service.ListCategory()
   
  }

}
