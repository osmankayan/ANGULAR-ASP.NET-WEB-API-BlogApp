import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Observable, Subscribable, Subscription } from 'rxjs';
import { BlogPost } from '../../models/blog-post.model';
import { BlogPostService } from '../../services/blog-post.service';
import { listcategory } from 'src/app/features/category/models/list-category.model';
import { CategoryService } from 'src/app/features/category/services/category.service';
import { UpdateBlogPost } from '../../models/update-blog-post.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy {

  model?: BlogPost
  id: string | null = null

  routesubscription?: Subscription
  updateBlogPostSubscription?: Subscription
  getBlogListSubscription?: Subscription
  deleteSubscription?: Subscription
  imageSelectSubscription?:Subscription

  categories$?: Observable<listcategory[]>
  selectedCategories?: string[]
  isImageSelectorVisible: boolean = false;

  constructor(private route: ActivatedRoute,
    private blogPostService: BlogPostService,
    private categoryService: CategoryService,
    private router: Router,
    private imageService: ImageService
  ) { }

  ngOnInit(): void {
    this.categories$ = this.categoryService.ListCategory()
    //we got the id from route
    this.routesubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id')
      }
    })
    // get from api
    if (this.id)
      this.getBlogListSubscription = this.blogPostService.getBlogPostById(this.id).subscribe({
        next: (response) => {
          this.model = response
          this.selectedCategories = response.categories.map(x => x.id)
        }
      })

     this.imageSelectSubscription= this.imageService.onSelectImage().subscribe({
        next:(response)=>{
          if(this.model){
            this.model.featuredImageUrl=response.url;
            this.isImageSelectorVisible=false;
          }
        }
      })


  }
  onFormSubmit(): void {

    //model to request

    if (this.model && this.id) {
      var updateBlogPost: UpdateBlogPost = {
        author: this.model.author,
        content: this.model.content,
        shortDescription: this.model.shortDescription,
        featuredImageUrl: this.model.featuredImageUrl,
        isVisible: this.model.isVisible,
        publishedDate: this.model.publishedDate,
        title: this.model.title,
        urlHandle: this.model.urlHandle,
        categories: this.selectedCategories ?? []

      }
      this.updateBlogPostSubscription = this.blogPostService.updateBlogPost(this.id, updateBlogPost).subscribe({
        next: (response) =>
          this.router.navigateByUrl("admin/blogposts")
      })
    }

  }
  onDelete(): void {
    if (this.id) {
      this.deleteSubscription = this.blogPostService.deleteBlogPost(this.id).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/blogposts')
        }
      })
    }
  }
  imageSelector(): void {
    this.isImageSelectorVisible = true;
  }
  closeImageSelector(): void {
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    this.routesubscription?.unsubscribe()
    this.updateBlogPostSubscription?.unsubscribe()
    this.getBlogListSubscription?.unsubscribe()
    this.deleteSubscription?.unsubscribe()
    this.imageSelectSubscription?.unsubscribe()
  }

}
