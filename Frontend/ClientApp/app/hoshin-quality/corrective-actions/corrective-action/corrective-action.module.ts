import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScheduleCorrectiveActionsComponent } from './schedule-corrective-actions/schedule-corrective-actions.component';
import { ConfigurationCorrectiveActionRoutingModule } from './corrective-action-routing.module';
import { ListCorrectiveActionsComponent } from './list-corrective-actions/list-corrective-actions.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'ClientApp/app/shared/shared.module';
import { GenerateAcComponent } from './generate-ac/generate-ac.component';
import { FishboneModule } from '../../fishbone/fishbone.module';
import { FishboneComponent } from './fishbone/fishbone.component';
import { ActionPlanComponent, PaginatorEspañol } from './action-plan/action-plan.component';
import { EvaluateAcComponent } from './evaluate-ac/evaluate-ac.component';
import { EditWorkgroupComponent } from './edit-workgroup/edit-workgroup.component';
import { TagInputModule } from 'ngx-chips';
import { DetailCorrectiveActionComponent } from './detail-corrective-action/detail-corrective-action.component';
import { MatPaginatorModule, MatPaginatorIntl } from '@angular/material';
import { ExtendDueDateComponent } from './extend-due-date/extend-due-date.component';
import { OverduedPlanningDateComponent } from './overdued-planning-date/overdued-planning-date.component';
import { OverduedEvaluateDateACComponent } from './overdued-evaluate-date-ac/overdued-evaluate-date-ac.component';
import { ReassingCorrectiveActionsComponent } from './reassing-corrective-actions/reassing-corrective-actions.component';


@NgModule({
  declarations: [
    ScheduleCorrectiveActionsComponent,
    ListCorrectiveActionsComponent,
    FishboneComponent,
    GenerateAcComponent,
    EditWorkgroupComponent,
    ActionPlanComponent,
    EvaluateAcComponent,
    DetailCorrectiveActionComponent,
    ExtendDueDateComponent,
    OverduedPlanningDateComponent,
    OverduedEvaluateDateACComponent,
    ReassingCorrectiveActionsComponent
  ],
  imports: [
    CommonModule,
    ConfigurationCorrectiveActionRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    FishboneModule,
    TagInputModule,
    MatPaginatorModule,
  ],
  providers: [{ provide: MatPaginatorIntl, useClass: PaginatorEspañol}]
})
export class ConfigurationCorrectiveActionModule {}
