import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { listcategory } from '../../category/models/list-category.model';
import { CategoryService } from '../../category/services/category.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnInit  {

  subscription?: Subscription

  model: AddBlogPost;
  categories$?:Observable<listcategory[]>

  constructor(private blogPostService: BlogPostService, private router: Router,private categoryService:CategoryService) {
    this.model = {
      title: '',
      shortDescription: '',
      urlHandle: '',
      content: '# Osman',
      featuredImageUrl: '',
      author: '',
      isVisible: true,
      publishedDate: new Date(),
      categories:[]
    }
  }
  ngOnInit(): void {
    this.categories$=this.categoryService.ListCategory();
  }

  onFormSubmit(): void {
    console.log(this.model)
    // this.subscription = 
    this.blogPostService.addBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/admin/blogposts')
      }
    })
    console.log(this.model)
  }

  // ngOnDestroy(): void {
  //   this.subscription?.unsubscribe();
  // }

}
