import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { listcategory } from '../../category/models/list-category.model';
import { CategoryService } from '../../category/services/category.service';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnInit  {

  subscription?: Subscription
  imageSelectorSubscription?:Subscription

  isImageSelectorVisible: boolean = false;
  model: AddBlogPost;
  categories$?:Observable<listcategory[]>

  constructor(private blogPostService: BlogPostService,
     private router: Router,
     private categoryService:CategoryService,
     private imageService:ImageService
     ) {
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

   this.imageSelectorSubscription= this.imageService.onSelectImage().subscribe({
      next:(selectedImage)=>{
        this.model.featuredImageUrl=selectedImage.url
        this.closeImageSelector()
      }
    })
  }

  onFormSubmit(): void {
    console.log(this.model)
    this.subscription = 
    this.blogPostService.addBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/admin/blogposts')
      }
    })
    console.log(this.model)
  }

  imageSelector(): void {
    this.isImageSelectorVisible = true;
  }
  closeImageSelector(): void {
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
    this.imageSelectorSubscription?.unsubscribe();
  }

}
