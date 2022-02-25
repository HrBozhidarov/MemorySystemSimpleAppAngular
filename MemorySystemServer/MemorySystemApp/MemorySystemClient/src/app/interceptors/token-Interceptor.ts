import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';

import { Observable } from 'rxjs';

import { ShareAuthService } from '../share/services/share-auth-service';
import { AccountService } from '../services/account/account.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private accountService: AccountService,
    private shareAuthService: ShareAuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const cloneRequest = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${this.accountService.getToken()}`),
    })

    this.shareAuthService.updatedDataSelection(this.accountService.isLoggedIn());

    return next.handle(cloneRequest);
  }
}
