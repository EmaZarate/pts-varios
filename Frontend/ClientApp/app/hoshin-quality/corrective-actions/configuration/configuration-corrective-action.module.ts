import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'ClientApp/app/shared/shared.module';

import { ConfigurationCorrectiveActionRoutingModule } from './configuration-corrective-action-routing.module';


import { CreateEditStateComponent } from './states/create-edit-state/create-edit-state.component';
import { ReadStateComponent } from './states/read-state/read-state.component';

import { ReadFishboneCategoriesComponent } from './fishbone-category/read-fishbone-categories/read-fishbone-categories.component';
import { NewFishBoneCategoryComponent } from './fishbone-category/new-fishbone-category/new-fishbone-category.component';
import { ReadParametrizationComponent } from './parametrization/read-parametrization/read-parametrization.component';
import { NewParametrizationComponent } from './parametrization/new-parametrization/new-parametrization.component';
@NgModule({
  declarations: [
    CreateEditStateComponent,
    ReadStateComponent,
    ReadFishboneCategoriesComponent,
    NewFishBoneCategoryComponent,
    ReadParametrizationComponent,
    NewParametrizationComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    ConfigurationCorrectiveActionRoutingModule
  ]
})
export class ConfigurationCorrectiveActionModule { }
