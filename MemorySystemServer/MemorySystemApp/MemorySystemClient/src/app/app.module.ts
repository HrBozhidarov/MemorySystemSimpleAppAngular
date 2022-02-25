import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/profile/login/login.component';
import { RegisterComponent } from './components/profile/register/register.component';
import { NavigationBarComponent } from './components/navigation-bar/navigation-bar.component';
import { HomeComponent } from './components/home/home.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';

import { TokenInterceptor } from './interceptors/token-Interceptor';
import { ErrorInterceptor } from './interceptors/error-interceptor';

import { AuthorizedGuard } from './guards/authorized.guard';

import { ToastrModule } from 'ngx-toastr';

import {NgxPaginationModule} from 'ngx-pagination'; 

import { MemoryCreateComponent } from './components/memory/memory-create/memory-create.component';
import { MemoryDetailsComponent } from './components/memory/memory-details/memory-details.component';
import { MyMemoriesComponent } from './components/memory/my-memories/my-memories.component';

import { MemoryService } from './services/memory/memory.service';
import { UserService } from './services/users/user.service';
import { ShareAuthService } from './share/services/share-auth-service';
import { AccountService } from './services/account/account.service';
import { LocalStorageService } from './share/services/local-storage.service';
import { MemoryCategoriesComponent } from './components/memory/memory-categories/memory-categories.component';
import { NonAuthorizedGuard } from './guards/nonAuthorized.guard';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavigationBarComponent,
    HomeComponent,
    FooterComponent,
    HeaderComponent,
    MemoryCreateComponent,
    MemoryDetailsComponent,
    MyMemoriesComponent,
    MemoryCategoriesComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    NgxPaginationModule,
    ToastrModule.forRoot(),
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    ShareAuthService,
    AccountService,
    MemoryService,
    UserService,
    LocalStorageService,
    AuthorizedGuard,
    NonAuthorizedGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
