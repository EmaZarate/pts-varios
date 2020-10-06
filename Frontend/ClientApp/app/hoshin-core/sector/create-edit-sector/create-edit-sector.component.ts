import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { Sector } from '../../models/Sector';

import { SectorsService } from '../../../core/services/sectors.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-create-edit-sector',
  templateUrl: './create-edit-sector.component.html',
  styleUrls: ['./create-edit-sector.component.css']
})

export class CreateEditSectorComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  sectorForm: FormGroup;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  get name() { return this.sectorForm.get('name'); }
  get code() { return this.sectorForm.get('code'); }
  get description() { return this.sectorForm.get('description'); }

  isCreate: boolean;
  _sector = new Sector();

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _sectorService: SectorsService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.sectorForm = this.modelCreate();

    this._route.data
    .takeUntil(this.ngUnsubscribe)    
    .subscribe((data) => {
      if(data.typeForm == 'new'){
        this.isCreate = true;
        this.blockUI.stop();
      }
      else {
        this.isCreate = false;
        this._route.params
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res)=> {
          this._sectorService.getOne(res.id)
            .takeUntil(this.ngUnsubscribe)
            .subscribe((res: Sector) => {
              this._sector = res;
              this.name.patchValue(this._sector.name);
              this.code.patchValue(this._sector.code);
              this.description.patchValue(this._sector.description);
              this.blockUI.stop();
            });
        });
      }
    });
  }

  modelCreate() {
    return this.fb.group({
      name: ['', Validators.required],
      code:['', Validators.required],
      description:['']
    });
  }

  onSubmit() {
    if(this.sectorForm.valid) {
      this.blockUI.start();
      this._sector.name = this.name.value;
      this._sector.code = this.code.value;
      this._sector.description = this.description.value;
      if(this.isCreate) {
        this._sector.active = true;
        this._sectorService.add(this._sector)
          .takeUntil(this.ngUnsubscribe)
          .subscribe(() => {
            this._toastrManager.successToastr('El sector se ha creado correctamente', 'Éxito');
            this._router.navigate(['/core/sectors']);
            this.blockUI.stop();
          });
      }
      else {
        this._sectorService.update(this._sector)
          .takeUntil(this.ngUnsubscribe)
          .subscribe(() =>{
            this._toastrManager.successToastr('El sector se ha actualizado correctamente', 'Éxito');
            this._router.navigate(['/core/sectors']);
            this.blockUI.stop();
          });
      }
    }
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
  
}
