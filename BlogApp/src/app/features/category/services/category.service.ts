import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { addCategoryRequest } from '../models/add-category-request.model';
import { Observable} from 'rxjs';
import { listcategory } from '../models/list-category.model';
import { environment } from 'src/environments/environment';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient) { }

  addCategory(model:addCategoryRequest): Observable <void> 
  {
    return this.http.post<void>(`${environment.baseApiUrl}/api/categories`,model)
  }
  ListCategory():Observable<listcategory[]>
  {
    return this.http.get<listcategory[]>(`${environment.baseApiUrl}/api/categories`);
  }
  ListCategoryById(id:string):Observable<listcategory>
  {
    return this.http.get<listcategory>(`${environment.baseApiUrl}/api/categories/${id}`);
  }
  UpdateCategory(id:string,model:UpdateCategoryRequest)
  {
    return this.http.put<listcategory>(`${environment.baseApiUrl}/api/categories/${id}`,model);
  }
  DeleteCategory(id:string)
  {
    return this.http.delete<listcategory>(`${environment.baseApiUrl}/api/categories/${id}`);
  }
}
