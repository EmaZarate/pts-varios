import { Component, OnInit, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';
import { StandardService } from '../../standard.service';
import { ToastrManager } from 'ng6-toastr-notifications';


@Component({
  selector: 'app-standard',
  templateUrl: './read-standard.component.html',
  styleUrls: ['./read-standard.component.css']
})
export class ReadStandardComponent implements OnInit,OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  _standards = [];

  constructor(private _standardService: StandardService,private _toastrManager: ToastrManager) { }

  ngOnInit() {
    this.blockUI.start();
    
    this._standardService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this._standards = res;
        this.blockUI.stop();
      })
  }

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  updateStandardActive(standard) {


    this.blockUI.start();    
    standard.active = !standard.active;
    this._standardService.update(standard)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(() => {

        this._toastrManager.successToastr('La norma se ha actualizado correctamente', 'Éxito');
        this.blockUI.stop();

       })
  }

  
}
