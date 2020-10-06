import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { TaskRoutingModule } from './task-routing.module';

import { EditTaskComponent } from './edit-task/edit-task.component';
import { ListTaskComponent } from './list-task/list-task.component';
import { DetailTaskComponent } from './detail-task/detail-task.component';

import { SharedModule } from '../../../shared/shared.module';
import { OverdueTaskComponent } from './overdue-task/overdue-task.component';
import { ExtendDueDateTaskComponent } from './extend-due-date-task/extend-due-date-task.component';


@NgModule({
  declarations: [
    EditTaskComponent,
    ListTaskComponent,
    DetailTaskComponent,
    OverdueTaskComponent,
    ExtendDueDateTaskComponent
  ],
  imports: [
    CommonModule,
    TaskRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule
  ]
})
export class TaskModule { }
