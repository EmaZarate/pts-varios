import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import{ FormsModule } from '@angular/forms';
import { HandleErrorService } from './services/handle-error.service';
import { LocalStorageService, StorageService } from './services/storage.service';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    HttpModule,
    HttpClientModule
  ],
  providers:[
    {provide: ErrorHandler, useClass: HandleErrorService},
    {provide: StorageService, useClass: LocalStorageService},
  ]
})
export class CoreModule { }
