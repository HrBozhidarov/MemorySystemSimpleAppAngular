import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Observable } from 'rxjs';

import { URLS } from 'src/app/constants/constants';

@Injectable()
export class MemoryService {
  private readonly createUrl = URLS.DOMAIN_URL + 'memory/create';
  private readonly userMemoriesUrl = URLS.DOMAIN_URL + 'memory/userMemories';
  private readonly likeMemoryUrl = URLS.DOMAIN_URL + 'memory/like?id=';

  constructor(private http: HttpClient) { }

  public create(payload: any): Observable<any> {
    return this.http.post(this.createUrl, payload);
  }

  public userMemories(category: string): Observable<any> {
    let params = new HttpParams();
    params = params.append('category', category);

    return this.http.get(`${this.userMemoriesUrl}`, { params: params });
  }

  public likeMemory(id: number) {
    return this.http.post(this.likeMemoryUrl + id, null);
  }
}
