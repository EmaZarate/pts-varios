import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InsideRoutingModule } from './inside-routing.module';
import { SharedModule } from '../shared/shared.module';

import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { InsideRoutingComponent } from './inside-routing.component';
import { HeaderComponent } from './components/header/header.component';
import { HomeComponent } from './components/home/home.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { FooterComponent } from './components/footer/footer.component';
import { BlockTemplateComponent } from '../shared/templates/block-template/block-template.component';
import { from } from 'rxjs';
//import { FormsModule } from '@angular/forms';

/*import {FlexLayoutModule} from '@angular/flex-layout';*/

@NgModule({
  imports: [
    CommonModule,
    HttpModule,
    HttpClientModule,
    InsideRoutingModule,
    SharedModule,
    //FormsModule,
  ],
  providers: [
  ],
  declarations: [
  InsideRoutingComponent,
  HeaderComponent,
  HomeComponent,
  SidebarComponent,
  FooterComponent
  ],
  entryComponents: [
    BlockTemplateComponent
  ]
})
export class InsideModule {}