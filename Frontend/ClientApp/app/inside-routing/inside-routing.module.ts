import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AuthService as RoleGuard } from '../core/services/auth.service';

import { InsideRoutingComponent } from './inside-routing.component';
import { HomeComponent } from './components/home/home.component';

const insideRoutes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'quality',
    canActivate: [RoleGuard],
    component: InsideRoutingComponent,
    loadChildren: '../hoshin-quality/hoshin-quality.module#HoshinQualityModule'
  },
  {
    path: 'core',
    canActivate: [RoleGuard],
    component: InsideRoutingComponent,
    loadChildren:'../hoshin-core/hoshin-core.module#HoshinCoreModule'
  },
  {
    path: 'profile',
    canActivate: [RoleGuard],
    component: InsideRoutingComponent,
    loadChildren: '../profile/profile.module#ProfileModule'
  },
  // {
  //   path: 'task',
  //   component: InsideRoutingComponent,
  //   loadChildren: '../task/task.module#TaskModule'
  // },
  {
    path: 'home',
    component: InsideRoutingComponent,
    canActivate: [RoleGuard],
    data: {
      expectedRole: 'user'
    },
    children: [
      {
        path: '',
        component: HomeComponent
      }
    ]
   }
];

@NgModule({
  imports: [
    RouterModule.forChild(insideRoutes),
    CommonModule
  ],
  exports: [
    RouterModule
  ],
  declarations: [
  ]
})
export class InsideRoutingModule { }
