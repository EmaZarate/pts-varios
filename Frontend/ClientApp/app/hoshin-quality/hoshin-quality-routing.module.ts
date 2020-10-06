import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    {
      path: 'finding',
      loadChildren: './findings/findings.module#FindingsModule'
    },
    {
      path: 'audits',
      loadChildren: './audits/audits.module#AuditsModule'
    },
    {
      path: 'corrective-actions',
      loadChildren: './corrective-actions/corrective-actions.module#CorrectiveActionsModule'
    },
    {
      path: 'tasks',
      loadChildren: './tasks/tasks.module#TasksModule'
    }
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

export class HoshinQualityRoutingModule { }
