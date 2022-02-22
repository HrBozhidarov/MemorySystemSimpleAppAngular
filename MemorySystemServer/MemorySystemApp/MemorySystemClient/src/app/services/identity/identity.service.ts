import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { LocalStorageService } from 'src/app/share/services/local-storage.service';

import { URLS } from 'src/app/constants/constants';

@Injectable()
export class IdentityService {
  private readonly loginUrl = URLS.DOMAIN_URL + 'identity/login';

  constructor(private http: HttpClient, private localStorageService: LocalStorageService) { }

  public login(payload: any): Observable<any> {
    return this.http.post(this.loginUrl, payload);
  }

  public logout() {
    this.localStorageService.removeItem("token");
  }

  public getToken(): any {
    return this.localStorageService.getItem("token");
  }

  public isLoggedIn() {
    return this.getToken() != null;
  }
}
