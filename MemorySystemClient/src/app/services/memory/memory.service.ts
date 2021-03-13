import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { URLS } from 'src/app/constants/constants';

@Injectable()
export class MemoryService {
  private readonly createUrl = URLS.DOMAIN_URL + 'pictures/create';
  private readonly myMemoriesUrl = URLS.DOMAIN_URL + 'users/myMemories';
  private readonly likePictureUrl = URLS.DOMAIN_URL + 'pictures/like?id=';

  constructor(private http: HttpClient) { }

  public create(payload: any): Observable<any> {
    return this.http.post(this.createUrl, payload);
  }

  public myMemories(category: string): Observable<any> {
    let params = new HttpParams();
    params = params.append('category', category);
    
    return this.http.get(`${this.myMemoriesUrl}`, { params: params });
  }

  public likePicture(id: number) {
    return this.http.post(this.likePictureUrl + id, null);
  }
}
