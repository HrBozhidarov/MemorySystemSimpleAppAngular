import { Component } from '@angular/core';

import { Router } from '@angular/router';

import { ShareAuthService } from 'src/app/share/services/share-auth-service';
import { LocalStorageService } from 'src/app/share/services/local-storage.service';
import { AccountService } from '../../services/account/account.service';

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
    public accountService: AccountService,
    private localStorageService: LocalStorageService,
    public router: Router) { 
    this.shareAuthService.data.subscribe(data => {
      this.isLogin = data;
      this.userProfileUrl = this.localStorageService.getItem('user-profile-picture');
    });
  }

  public logout() {
    this.accountService.logout();
    this.shareAuthService.updatedDataSelection(this.accountService.isLoggedIn());
    this.router.navigate(['/home']);
  }
}
