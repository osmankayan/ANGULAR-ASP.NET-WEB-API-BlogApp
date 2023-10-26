import { Injectable } from '@angular/core';
import { AddBlogPost } from '../../models/add-blog-post.model';
import { Observable } from 'rxjs';
import { BlogPost } from '../../models/blog-post.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

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
}
