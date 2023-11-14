import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { blogImage } from '../../models/blog-image.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

selectedImage:BehaviorSubject<blogImage> = new BehaviorSubject<blogImage>({
  id:'',
  fileExtenstion:'',
  fileName:'',
  title:'',
  url:''
})
  constructor(private http:HttpClient) { }

  getAllImages():Observable<blogImage[]>{
    return this.http.get<blogImage[]>(`${environment.baseApiUrl}/api/Images`)
  }

  uploadImage(file:File,fileName:string,title:string):Observable<blogImage>{
    const formData=new FormData();
    formData.append('file',file)
    formData.append('fileName',fileName)
    formData.append('title',title)
    return this.http.post<blogImage>(`${environment.baseApiUrl}/api/Images`,formData)
  }

  selectImage(image:blogImage):void{
    this.selectedImage.next(image);
  }

  onSelectImage():Observable<blogImage>{
    return this.selectedImage.asObservable()
  }
}
