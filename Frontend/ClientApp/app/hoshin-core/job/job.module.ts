import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JobRoutingModule } from './job-routing.module';

import { CreateEditJobComponent } from './create-edit-job/create-edit-job.component';
import { ReadJobsComponent } from './read-jobs/read-jobs.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'ClientApp/app/shared/shared.module';

@NgModule({
  declarations: [
    CreateEditJobComponent,
    ReadJobsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    JobRoutingModule
  ]
})
export class JobModule { }
