import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { LocalStorageService } from 'src/app/share/services/local-storage.service';

import { URLS, ACCOUNT_KEYS } from 'src/app/constants/constants';

@Injectable()
export class AccountService {
  private readonly loginUrl = URLS.DOMAIN_URL + 'account/login';
  private readonly tokenKey: string = ACCOUNT_KEYS.TOKEN;
  private readonly userProfilePictureKey: string = ACCOUNT_KEYS.USER_PROFILE_PICTURE;
  private readonly roleKey = ACCOUNT_KEYS.ROLE;
  private readonly memoryCategoryKey = ACCOUNT_KEYS.MEMORY_CATEGORY;

  constructor(private http: HttpClient, private localStorageService: LocalStorageService) { }

  public login(payload: any): Observable<any> {
    return this.http.post<any>(this.loginUrl, payload).pipe(map(result => {
      this.localStorageService.setItem(this.tokenKey, result.data.token);
      this.localStorageService.setItem(this.userProfilePictureKey, result.data.profileUrl);
      this.localStorageService.setItem(this.roleKey, result.data.role);
    }));
  }

  public logout() {
    this.localStorageService.removeItem(this.tokenKey);
    this.localStorageService.removeItem(this.userProfilePictureKey);
    this.localStorageService.removeItem(this.memoryCategoryKey);
  }

  public getToken(): any {
    return this.localStorageService.getItem(this.tokenKey);
  }

  public isLoggedIn() {
    return this.getToken() != null;
  }
}
