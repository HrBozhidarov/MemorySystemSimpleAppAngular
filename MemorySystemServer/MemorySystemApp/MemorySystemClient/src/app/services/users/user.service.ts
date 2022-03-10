import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { URLS } from 'src/app/constants/constants';

@Injectable()
export class UserService {
  private readonly createUrl = URLS.DOMAIN_URL + 'user/create';
  private readonly updateUrl = URLS.DOMAIN_URL + 'user/update';
  private readonly detailsUrl = URLS.DOMAIN_URL + 'user/details';

  constructor(private http: HttpClient) { }

  public create(payload: any): Observable<any> {
    return this.http.post(this.createUrl, payload);
  }

  public details(): Observable<any> {
    return this.http.get(this.detailsUrl);
  }

  public update(payload: any): Observable<any> {
    return this.http.post(this.updateUrl, payload);
  }
}
