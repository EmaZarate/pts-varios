import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [

  {
    path: '',
    loadChildren: './task/task.module#TaskModule'
  },
  {
    path: 'config',
    loadChildren: './config/taskConfig.module#TaskConfigModule'
  }
];
@NgModule({
        imports: [RouterModule.forChild(routes)],
        exports: [RouterModule]
      })
export class TasksRoutingModule {}