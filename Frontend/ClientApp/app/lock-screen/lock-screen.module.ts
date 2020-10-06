import { NgModule, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LockScreenComponent } from './lock-screen.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule} from '@angular/material';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    MatFormFieldModule,
    MatInputModule,
    CommonModule,
    FormsModule,    
    ReactiveFormsModule,
    SharedModule
    
  ],
  exports: [  
    MatFormFieldModule,
    MatInputModule    
    ],
  declarations: [
    LockScreenComponent
  ]
  
})
export class LockScreenModule {
  
   

 }
