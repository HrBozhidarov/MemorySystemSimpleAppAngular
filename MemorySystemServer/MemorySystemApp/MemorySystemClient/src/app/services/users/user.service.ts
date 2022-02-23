import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { URLS } from 'src/app/constants/constants';

@Injectable()
export class UserService {
  private readonly registerUrl = URLS.DOMAIN_URL + 'users/register';

  constructor(private http: HttpClient) { }

  public register(payload: any): Observable<any> {
    return this.http.post(this.registerUrl, payload);
  }
}
