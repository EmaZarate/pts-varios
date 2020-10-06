import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { FacebookService } from './services/facebook.service';
import { GoogleService } from './services/google.service';
import { MicrosoftGraphService } from './services/microsoft-graph.service';
import { FactoryLogin } from './factories/factory-login';
import { LoginComponent } from './components/login.component';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    LoginRoutingModule
  ],
  providers:[
    FacebookService, 
    GoogleService, 
    MicrosoftGraphService, 
    FactoryLogin
  ]
})
export class LoginModule { }
