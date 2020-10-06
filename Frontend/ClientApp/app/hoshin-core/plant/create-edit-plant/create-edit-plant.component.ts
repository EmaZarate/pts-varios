import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { Plant } from '../../models/Plant';

import { PlantsService } from '../../../core/services/plants.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-create-edit-plant',
  templateUrl: './create-edit-plant.component.html',
  styleUrls: ['./create-edit-plant.component.css']
})
export class CreateEditPlantComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  plantForm: FormGroup;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  get name() { return this.plantForm.get('name'); }
  get country() { return this.plantForm.get('country'); }

  isCreate: boolean;
  _plant = new Plant();


  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _plantService: PlantsService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.plantForm = this.modelCreate();

    this._route.data
    .takeUntil(this.ngUnsubscribe)
    .subscribe((data) => {
      if (data.typeForm == 'new') {
        this.isCreate = true;
        this.blockUI.stop();
      }
      else {
        this.isCreate = false;
        this._route.params
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          this.getPlant(res.id);
        });
      }
    });
  }

  getPlant(id) {
    this._plantService.getOne(id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: Plant) => {
        this._plant = res;
        this.name.patchValue(this._plant.name);
        this.country.patchValue(this._plant.country);
        this.blockUI.stop();
      });
  }

  modelCreate() {
    return this.fb.group({
      name: ['', Validators.required],
      country: ['', Validators.required]
    });
  }

  addPlant(){
    this._plantService.add(this._plant)
    .takeUntil(this.ngUnsubscribe)
    .subscribe(() => {
      this._toastrManager.successToastr('La planta se ha creado correctamente', 'Éxito');
      this._router.navigate(['/core/plants']);
      this.blockUI.stop();
    });
  }

  updatePlant(){
    this._plantService.update(this._plant)
    .takeUntil(this.ngUnsubscribe)
    .subscribe(() => {
      this._toastrManager.successToastr('La planta se ha actualizado correctamente', 'Éxito');
      this._router.navigate(['/core/plants']);
      this.blockUI.stop();
    });
  }

  onSubmit() {
    if (this.plantForm.valid) {
      this.blockUI.start();
      this._plant.name = this.name.value;
      this._plant.country = this.country.value;
      if (this.isCreate) {
        this._plant.active = true;
        this.addPlant();
      }
      else {
        this.updatePlant();
      }
    }
  }


  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
