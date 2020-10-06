import { Routes } from '@angular/router';

import { FishboneComponent } from './fishbone.component';

export const FishBoneRoutes: Routes = [
    {

      path: '',
      children: [ {
        path: '',
        component: FishboneComponent      
    }]
}];
