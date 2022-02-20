import { Component } from '@angular/core';

import { Router } from '@angular/router';

import { ShareAuthService } from 'src/app/share/services/share-auth-service';
import { IdentityService } from 'src/app/services/identity/identity.service';
import { LocalStorageService } from 'src/app/share/services/local-storage.service';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent {
  public isLogin: boolean;
  public userProfileUrl: string;

  constructor(
    public shareAuthService: ShareAuthService,
    public identityService: IdentityService,
    private localStorageService: LocalStorageService,
    public router: Router) { 
    this.shareAuthService.data.subscribe(data => {
      this.isLogin = data;
      this.userProfileUrl = this.localStorageService.getItem('user-profile-picture');
    });
  }

  public logout() {
    this.identityService.logout();
    this.localStorageService.removeItem('user-profile-picture');
    this.localStorageService.removeItem('my-memory-category-key');
    this.shareAuthService.updatedDataSelection(this.identityService.isLoggedIn());
    this.router.navigate(['/home']);
  }
}
