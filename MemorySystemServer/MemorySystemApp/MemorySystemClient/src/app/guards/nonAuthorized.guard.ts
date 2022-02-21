import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, CanActivate } from '@angular/router';

import { IdentityService } from '../services/identity/identity.service';

@Injectable()
export class NonAuthorizedGuard implements CanActivate {
  constructor(private identityService: IdentityService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.identityService.isLoggedIn();
  }
}
