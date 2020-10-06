import { NgModule } from '@angular/core';

import {
  MatButtonModule,
  MatInputModule,
  MatToolbarModule,
  MatCardModule,
  MatDividerModule,
  MatIconModule,
  MatListModule,
  MatSidenavModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatGridListModule,
  MatSelectModule,
  MatTooltipModule,
  MatTableModule,
  MatCheckboxModule
} from '@angular/material';
import { MccColorPickerModule } from 'material-community-components';
import { UploadFileComponent } from './components/upload-file/upload-file.component';
import { CommonModule } from '@angular/common';
import { BlockTemplateComponent } from './templates/block-template/block-template.component';
import { BlockUIModule } from 'ng-block-ui';
import { SweetAlert2Module } from '@toverux/ngx-sweetalert2';
import { TranslateClaimsPipe } from './pipes/translate-claims.pipe';
import { FieldErrorDisplayComponent } from './components/field-error-display/field-error-display.component';
import { ExportPdfComponent } from './components/export-pdf/export-pdf.component';

import { Ng7LargeFilesUploadLibModule, Ng7LargeFilesUploadLibComponent } from 'ng7-large-files-upload-lib';
import { UploadLargeFilesComponent } from './components/upload-large-files/upload-large-files.component';
import { InvalidmessageDirective } from './directives/invalidmessage.directive';
import { InvalidTypeDirective } from './directives/invalidType.directive';


import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { AppDateAdapter, APP_DATE_FORMATS } from './util/dates/date.adapter';
import { ValidateDatePipe } from './pipes/validate-date.pipe';
import { ExtendOverdureDateComponent } from './components/extend-overdure-date/extend-overdure-date.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TableModule } from 'primeng/table';
import {DropdownModule} from 'primeng/primeng';

@NgModule({
  imports: [
      CommonModule,
      ReactiveFormsModule,
      MatButtonModule,
      MatInputModule,
      MatToolbarModule,
      MatCardModule,
      MatDividerModule,
      MatIconModule,
      MatListModule,
      MatSidenavModule,
      MatDatepickerModule,
      MatNativeDateModule,
      MatGridListModule,
      MatSelectModule,
      MatTooltipModule,
      MatTableModule,
      MatCheckboxModule,
      MccColorPickerModule.forRoot({
        empty_color: '#00FFFFFF'
      }),

      Ng7LargeFilesUploadLibModule.forRoot('http://localhost:5000/api/finding/uploadfiles'),
      BlockUIModule.forRoot({
        template: BlockTemplateComponent
      }),
      SweetAlert2Module.forRoot({
        buttonsStyling: false,
        customClass: 'modal-content',
        confirmButtonClass: 'btn btn-primary',
        cancelButtonClass: 'btn'
      }),
      TableModule,
      DropdownModule
    ],
  exports: [
      MatButtonModule,
      MatInputModule,
      MatToolbarModule,
      MatCardModule,
      MatDividerModule,
      MatIconModule,
      MatListModule,
      MatSidenavModule,
      MatDatepickerModule,
      MatNativeDateModule,
      MatGridListModule,
      MatSelectModule,
      MatTooltipModule,
      MatTableModule,
      MatCheckboxModule,
      MccColorPickerModule,
      UploadFileComponent,
      BlockUIModule,
      SweetAlert2Module,
      TranslateClaimsPipe,
      ExportPdfComponent,
      FieldErrorDisplayComponent,
      Ng7LargeFilesUploadLibComponent,
      UploadLargeFilesComponent,
      InvalidTypeDirective,
      InvalidmessageDirective,
      ValidateDatePipe,
      ExtendOverdureDateComponent,
      TableModule,
      DropdownModule
    ],
  declarations: [
    BlockTemplateComponent,
    UploadFileComponent,
    TranslateClaimsPipe,
    ExportPdfComponent,
    FieldErrorDisplayComponent,
    UploadLargeFilesComponent,
    InvalidTypeDirective,
    InvalidmessageDirective,
    ValidateDatePipe,
    ExtendOverdureDateComponent
  ],
  entryComponents: [
    BlockTemplateComponent
  ],
  providers:[
    {
      provide: DateAdapter, useClass: AppDateAdapter
  },
  {
      provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
  }
  ]
})
export class SharedModule { }