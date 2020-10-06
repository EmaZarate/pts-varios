import { Component, OnInit, Input, ViewChildren, Output, EventEmitter, OnDestroy } from '@angular/core';

import { SectorsService } from '../../../core/services/sectors.service';

import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';

declare var $:any;

@Component({
  selector: 'app-plant-sector-form',
  templateUrl: './plant-sector-form.component.html',
  styleUrls: ['./plant-sector-form.component.css']
})

export class PlantSectorFormComponent implements OnInit, OnDestroy {
  
  @Input() sectors: Array<any> = [];
  @Input() disabled: boolean;
  @Input() plant;
  @Output() sectorsSelectedEmitter = new EventEmitter<Array<any>>();
  @ViewChildren('sectorsChecks') sectorsChecks;
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  sectorSelected = [];
  allSectors =[];
  constructor(
    private _sectorsService: SectorsService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this._sectorsService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.allSectors = res;
        //console.log(res);
        this.blockUI.stop();
      })

  }

  ngAfterViewInit(){
    this.sectorsChecks.changes
    .takeUntil(this.ngUnsubscribe)
    .subscribe(() => this.patchSectors())
  }

  patchSectors(){
    this.sectorSelected = [];

    this.unselectChecks(this.disabled);
    this.selectChecks();
    
  }

  selectChecks(){
    $('.sectorCheck').each((index, el) => {
      if(!this.sectors) return;
      let sec = this.sectors.find((sector) => sector.sectorId == $(el).val() || sector.id ==  $(el).val())
      
      if(sec != null){
          el.checked = true;
          if(sec.id == null){
            sec.id = sec.sectorId
          }
          sec.type = 'sector';
          this.sectorSelected.push(sec);
      }
    })
  }

  unselectChecks(disabled){
    $('.sectorCheck').each((index, el) => {
      el.disabled = disabled
      el.checked = false;

      let $formCheck = $(el).parents('.form-check');
      if (disabled) {
        $formCheck.removeClass('enabled');
      } 
      else {
        $formCheck.addClass('enabled');
      }
    })
  }
  
  ngOnChanges(){
    this.patchSectors();
  }

  checkboxClicked(ev, sector){

    if(ev.target.checked){
        //Addclaim
        sector.type = 'sector';
        if(sector.sectorId != null){
          sector.id = sector.sectorId;
        }

        this.sectorSelected.push(sector);
    }
    else{
        //Remove claim
        let indexSectorToDelete = this.sectorSelected.findIndex(x => x.sectorId == sector.sectorId || x.id == sector.sectorId);
        this.sectorSelected.splice(indexSectorToDelete, 1);
    }

    this.sectorsSelectedEmitter.emit(this.sectorSelected);

    //console.log(this.sectorSelected);
}

ngOnDestroy(){
  this.ngUnsubscribe.next();
  this.ngUnsubscribe.complete();
}
}
