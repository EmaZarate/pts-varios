import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';



const routes: Routes = [
  {
    path: '',
    loadChildren: './finding/finding.module#FindingModule'
  },
  {
    path: 'config',
    loadChildren: './configuration/configuration.module#ConfigurationModule'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FindingsRoutingModule { }
