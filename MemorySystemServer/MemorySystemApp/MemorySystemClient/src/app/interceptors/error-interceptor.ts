import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpEvent, HttpHandler, HttpErrorResponse } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Router } from '@angular/router';

import { ShareAuthService } from '../share/services/share-auth-service';

import { ToastrService } from 'ngx-toastr';
import { AccountService } from "../services/account/account.service";


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(
        private router: Router, 
        private shareAuthService: ShareAuthService,
        private accountService: AccountService,
        private toastrService: ToastrService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request)
            .pipe(catchError((error: HttpErrorResponse) => {
                if (error.status === 401) {
                    this.accountService.logout();
                    this.shareAuthService.updatedDataSelection(this.accountService.isLoggedIn());
                    this.router.navigate(['/login']);

                    this.toastrService.error('Login In Your Account');
                } else if(error.status === 403) {
                  this.toastrService.error('You don`t have permissions!');
                }

                return throwError(error);
            }))
    }
}
