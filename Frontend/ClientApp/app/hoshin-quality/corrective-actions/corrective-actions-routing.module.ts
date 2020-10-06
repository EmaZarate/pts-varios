import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [

  {
    path: '',
    loadChildren:'./corrective-action/corrective-action.module#ConfigurationCorrectiveActionModule'
  },
  {
    path: 'config',
    loadChildren: './configuration/configuration-corrective-action.module#ConfigurationCorrectiveActionModule'
  }


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CorrectiveActionsRoutingModule { }
