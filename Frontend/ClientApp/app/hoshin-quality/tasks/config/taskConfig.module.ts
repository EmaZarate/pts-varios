import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import {TaskStateNewEditComponent} from './task-state-new-edit/task-state-new-edit.component';
import {StateTaskComponent} from './state-task/state-task.component';

import {TaskConfigRoutingModule} from '../config/taskConfig-routing.module';

import { SharedModule } from '../../../shared/shared.module';



@NgModule({
        declarations: [
                StateTaskComponent,
                TaskStateNewEditComponent
        ],
        imports: [
          CommonModule,
          TaskConfigRoutingModule,
          ReactiveFormsModule,
          FormsModule,
          SharedModule
        ]
      })
      export class TaskConfigModule { }