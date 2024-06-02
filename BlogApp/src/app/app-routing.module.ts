import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { CategoryAddComponent } from './features/category/category-add/category-add.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { BlogpostListComponent } from './features/blog-post/blogpost-list/blogpost-list.component';
import { AddBlogpostComponent } from './features/blog-post/add-blogpost/add-blogpost.component';
import { EditBlogpostComponent } from './features/blog-post/edit-blogpost/edit-blogpost/edit-blogpost.component';
import { HomeComponent } from './features/public/home/home.component';
import { BlogDetailsComponent } from './features/public/blog-details/blog-details.component';
import { LoginComponent } from './features/auth/login/login.component';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'login', component:LoginComponent},
  {path:'blog/:url', component:BlogDetailsComponent},
  {path:'admin/categories', component:CategoryListComponent},
  {path:'admin/categories/add',component:CategoryAddComponent},
  {path:'admin/categories/:id',component:EditCategoryComponent},
  {path:'admin/blogposts',component:BlogpostListComponent},
  {path:'admin/blogposts/add',component:AddBlogpostComponent},
  {path:'admin/blogposts/:id',component:EditBlogpostComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
