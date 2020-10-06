import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRouteModule } from './app-route.module';
import { SharedModule } from './shared/shared.module';
import { ToastrModule } from 'ng6-toastr-notifications';
import { LockScreenModule } from "./lock-screen/lock-screen.module";
import { AppComponent } from './app.component';
import { httpInterceptorProviders } from './http-interceptors/index';
import { CoreModule } from './core/core.module';
import { LoginModule } from './login/login.module';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';



@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        CoreModule,
        SharedModule,
        BrowserModule,
        HttpClientModule,
        AppRouteModule,
        ReactiveFormsModule, 
        FormsModule,
        HttpModule,
        BrowserAnimationsModule,
        LockScreenModule,  
        ToastrModule.forRoot(),
        LoginModule,
        ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
    ],
    providers: [
        httpInterceptorProviders,
        
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
