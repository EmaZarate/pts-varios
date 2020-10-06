import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FindingRoutingModule } from './finding-routing.module';

import { ApproveFindingComponent } from './approve-finding/approve-finding.component';
import { ApproveReassignmentComponent } from './approve-reassignment/approve-reassignment.component';
import { CloseFindingComponent } from './close-finding/close-finding.component';
import { CreateFindingComponent } from './create-finding/create-finding.component';
import { EditExpirationdateFindingComponent } from './edit-expirationdate-finding/edit-expirationdate-finding.component';
import { ReadComponent } from './read/read.component';
import { ReassingFindingComponent } from './reassing-finding/reassing-finding.component';
import { RejectFindingComponent } from './reject-finding/reject-finding.component';
import { RejectReassignmentComponent } from './reject-reassignment/reject-reassignment.component';
import { UpdateApprovedFindingComponent } from './update-approved-finding/update-approved-finding.component';
import { ViewFindingDetailComponent } from './view-finding-detail/view-finding-detail.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'ClientApp/app/shared/shared.module';
// import { TableModule } from 'primeng/table';
// import {DropdownModule} from 'primeng/primeng';



@NgModule({
  declarations: [
    ApproveFindingComponent,
    ApproveReassignmentComponent,
    CloseFindingComponent,
    CreateFindingComponent,
    EditExpirationdateFindingComponent,
    ReadComponent,
    ReassingFindingComponent,
    RejectFindingComponent,
    RejectReassignmentComponent,
    UpdateApprovedFindingComponent,
    ViewFindingDetailComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    FindingRoutingModule,
    // TableModule,
    // DropdownModule
  ]
})
export class FindingModule { }
