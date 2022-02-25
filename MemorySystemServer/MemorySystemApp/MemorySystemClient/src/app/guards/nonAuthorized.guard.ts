import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';

import { AccountService } from '../services/account/account.service';

@Injectable()
export class NonAuthorizedGuard implements CanActivate {
  constructor(private accountService: AccountService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.accountService.isLoggedIn();
  }
}
