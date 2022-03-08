import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './components/user/login/login.component';
import { CreateProfileComponent } from './components/user/createProfile/create-profile.component';
import { HomeComponent } from './components/home/home.component';
import { MemoryCreateComponent } from './components/memory/memory-create/memory-create.component';
import { MyMemoriesComponent } from './components/memory/my-memories/my-memories.component';
import { EditProfileComponent } from './components/user/editProfile/edit-profile.component';

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
  { path: 'create-profile', component: CreateProfileComponent, canActivate: [AuthorizedGuard] },
  { path: 'edit-profile', component: EditProfileComponent, canActivate: [NonAuthorizedGuard] },
  { path: 'memory-create', component: MemoryCreateComponent, canActivate: [NonAuthorizedGuard] },
  { path: 'my-memories', component: MyMemoriesComponent, canActivate: [NonAuthorizedGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
