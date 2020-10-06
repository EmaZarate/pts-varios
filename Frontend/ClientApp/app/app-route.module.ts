import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { LockScreenComponent } from './lock-screen/lock-screen.component';

const routes: Routes = [
  {
    path: 'login',
    loadChildren: './login/login.module#LoginModule'
  },
  {
    path: 'lock',
    component: LockScreenComponent
  },
  {
    path: '',
    loadChildren: './inside-routing/inside.module#InsideModule'
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes, {
      useHash: false,
      enableTracing: false
    })
  ],
  declarations: [],
  exports: [RouterModule]
})
export class AppRouteModule {}
