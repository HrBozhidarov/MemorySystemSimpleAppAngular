import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './components/profile/login/login.component';
import { RegisterComponent } from './components/profile/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { MemoryCreateComponent } from './components/memory/memory-create/memory-create.component';
import { MyMemoriesComponent } from './components/memory/my-memories/my-memories.component';

import { AuthorizedGuard } from './guards/authorized.guard';
import { NonAuthorizedGuard } from './guards/nonAuthorized.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: HomeComponent
  },
  { path: 'login', component: LoginComponent, canActivate: [AuthorizedGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [AuthorizedGuard] },
  { path: 'memory-create', component: MemoryCreateComponent, canActivate: [NonAuthorizedGuard] },
  { path: 'my-memories', component: MyMemoriesComponent, canActivate: [NonAuthorizedGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
