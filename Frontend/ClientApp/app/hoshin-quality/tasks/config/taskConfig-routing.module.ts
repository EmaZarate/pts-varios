import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {TaskStateNewEditComponent} from './task-state-new-edit/task-state-new-edit.component';
import {StateTaskComponent} from './state-task/state-task.component';

const routes: Routes = [

                {
                        path: 'task-states',
                        component: StateTaskComponent
                      },
                      {
                        path: 'task-state-new-edit/:id/edit',
                        component: TaskStateNewEditComponent,
                        data: {
                          typeForm: 'edit'
                        }
                      },
                      {
                        path: 'task-state-new-edit/new',
                        component: TaskStateNewEditComponent,
                        data: {
                          typeForm: 'new'
                        }
                      },
      ];

@NgModule({
        imports: [RouterModule.forChild(routes)],
        exports: [RouterModule]
      })
      export class TaskConfigRoutingModule { }
