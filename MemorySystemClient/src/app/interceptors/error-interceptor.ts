import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpEvent, HttpHandler, HttpErrorResponse } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Router } from '@angular/router';

import { ShareAuthService } from '../share/services/share-auth-service';
import { IdentityService } from '../services/identity/identity.service';

import { ToastrService } from 'ngx-toastr';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(
        private router: Router, 
        private shareAuthService: ShareAuthService,
        private identityService: IdentityService,
        private toastrService: ToastrService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
            .pipe(catchError((error: HttpErrorResponse) => {
                if (error.status === 401) {
                    this.identityService.logout();
                    this.shareAuthService.updatedDataSelection(this.identityService.isLoggedIn());
                    this.router.navigate(['/login']);

                    this.toastrService.error('Login In Your Account');
                }

                return throwError(error);
            }))
    }
}
