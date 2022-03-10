import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginUserComponent } from './components/user/login/login-user.component';
import { CreateUserComponent } from './components/user/create/create-user.component';
import { HomeComponent } from './components/home/home.component';
import { MemoryCreateComponent } from './components/memory/memory-create/memory-create.component';
import { MyMemoriesComponent } from './components/memory/my-memories/my-memories.component';
import { UpdateUserComponent } from './components/user/update/update-user.component';

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
  { path: 'login-user', component: LoginUserComponent, canActivate: [AuthorizedGuard] },
  { path: 'create-user', component: CreateUserComponent, canActivate: [AuthorizedGuard] },
  { path: 'update-user', component: UpdateUserComponent, canActivate: [NonAuthorizedGuard] },
  { path: 'memory-create', component: MemoryCreateComponent, canActivate: [NonAuthorizedGuard] },
  { path: 'my-memories', component: MyMemoriesComponent, canActivate: [NonAuthorizedGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
