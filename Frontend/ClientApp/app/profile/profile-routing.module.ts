import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Profile } from 'selenium-webdriver/firefox';
import { ProfileComponent } from "./components/profile/profile.component";
import { PersonalConfigurationComponent } from "./components/personal-configuration/personal-configuration.component";

const routes: Routes = [
  {
    path:'view',
    component:ProfileComponent,
    data: {
      typeForm: 'view'
    }
  },
  {
    path:'edit',
    component: ProfileComponent,
    data: {
      typeForm: 'edit'
    }
  },
  {
    path: 'personal-configuration',
    component: PersonalConfigurationComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfileRoutingModule { }
