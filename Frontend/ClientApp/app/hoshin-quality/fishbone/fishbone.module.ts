import { NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FishboneComponent } from './fishbone.component';
import { SharedModule } from 'ClientApp/app/shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,       
]
,declarations: [FishboneComponent]
,exports:[FishboneComponent]

})

export class FishboneModule {

}