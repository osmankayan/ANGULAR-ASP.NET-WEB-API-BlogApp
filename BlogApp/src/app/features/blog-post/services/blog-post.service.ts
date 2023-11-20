import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UpdateBlogPost } from '../models/update-blog-post.model';
import { DeferBlockTemplateDependency } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http: HttpClient) { }

  addBlogPost(model: AddBlogPost): Observable<BlogPost> {
    return this.http.post<BlogPost>(`${environment.baseApiUrl}/api/BlogPost`,model);
  }
  listBlogPost():Observable<BlogPost[]>{
    return this.http.get<BlogPost[]>(`${environment.baseApiUrl}/api/BlogPost`)
  }
  getBlogPostById(id:string):Observable<BlogPost>{
    return this.http.get<BlogPost>(`${environment.baseApiUrl}/api/BlogPost/${id}`)
  }
  getBlogPostByUrlHandle(urlHandle:string):Observable<BlogPost>{
    return this.http.get<BlogPost>(`${environment.baseApiUrl}/api/BlogPost/${urlHandle}`)
  }
  updateBlogPost(id:string,updatedBlogPost:UpdateBlogPost):Observable<BlogPost>{
    return this.http.put<BlogPost>(`${environment.baseApiUrl}/api/BlogPost/${id}`,updatedBlogPost)
  }
  deleteBlogPost(id:string):Observable<BlogPost>{
    return this.http.delete<BlogPost>(`${environment.baseApiUrl}/api/BlogPost/${id}`)
  }
}
