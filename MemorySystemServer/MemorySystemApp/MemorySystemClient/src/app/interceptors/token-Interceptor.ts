import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';

import { Observable } from 'rxjs';

import { IdentityService } from '../services/identity/identity.service';
import { ShareAuthService } from '../share/services/share-auth-service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(
        private identityService: IdentityService,
        private shareAuthService: ShareAuthService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const cloneRequest = req.clone({
            headers: req.headers.set('Authorization', `Bearer ${this.identityService.getToken()}`),
        })

        this.shareAuthService.updatedDataSelection(this.identityService.isLoggedIn());

        return next.handle(cloneRequest);
    }
}
