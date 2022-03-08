import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { URLS } from 'src/app/constants/constants';

@Injectable()
export class UserService {
  private readonly createProfileUrl = URLS.DOMAIN_URL + 'user/createProfile';
  private readonly editProfileUrl = URLS.DOMAIN_URL + 'user/editProfile';
  private readonly profileUrl = URLS.DOMAIN_URL + 'user/profile';

  constructor(private http: HttpClient) { }

  public create(payload: any): Observable<any> {
    return this.http.post(this.createProfileUrl, payload);
  }

  public profile(): Observable<any> {
    return this.http.get(this.profileUrl);
  }

  public edit(payload: any): Observable<any> {
    return this.http.post(this.editProfileUrl, payload);
  }
}
