import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';

import { AuthorizedGuard } from './guards/authorized.guard';
import { MemoryCreateComponent } from './components/memory/memory-create/memory-create.component';
import { MyMemoriesComponent } from './components/memory/my-memories/my-memories/my-memories.component';

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
  { path: 'memory-create', component: MemoryCreateComponent }, // add guard
  { path: 'my-memories', component: MyMemoriesComponent }, // add guard
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
