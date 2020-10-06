import { Component, OnInit, ElementRef, ViewChildren, OnDestroy } from '@angular/core';
import { FishBoneC } from '../../../models/fishBoneState';
//import { Fishbone } from '../../../models/Fishbone';
import { FormGroup, FormControlName, FormBuilder, Validators } from '@angular/forms';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Router, ActivatedRoute } from '@angular/router';
import { FishboneService } from '../../../Fishbone.service';
import { AuthService } from 'ClientApp/app/core/services/auth.service';
import { Subject } from 'rxjs';
import * as PERMISSIONS from "../../../../../core/permissions/index";


@Component({
  selector: 'app-new-fishbone-category',
  templateUrl: './new-fishbone-category.component.html',
  styleUrls: ['./new-fishbone-category.component.css']
})
export class NewFishBoneCategoryComponent implements OnInit, OnDestroy {
  @BlockUI() blockUI: NgBlockUI;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  fishBoneForm: FormGroup;
  _fishBone = new FishBoneC();
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  get name() { return this.fishBoneForm.get('name') }
  get color() { return this.fishBoneForm.get('color') }
  get active() { return this.fishBoneForm.get('active') }

  isCreate: boolean;

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private fb: FormBuilder,
    private _FishBoneService: FishboneService,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    
    this.blockUI.start();
    this.fishBoneForm = this.modelCreate();
    this._route.data
    .takeUntil(this.ngUnsubscribe)
    .subscribe((data) => {
      if (data.typeForm == 'new') {
        this.isCreate = true;
        this.blockUI.stop();
      } else {
        this.isCreate = false;
        this._route.params.subscribe((res) => {
          this._FishBoneService.get(res.id)
             .takeUntil(this.ngUnsubscribe)
            .subscribe((res: FishBoneC) => {
              this._fishBone = res;
              this.color.patchValue(this._fishBone.color);
              this.name.patchValue(this._fishBone.name);
              this.active.patchValue(this._fishBone.active);
              this.blockUI.stop();

            })
        });
      }
    });
    this.setPermissions();
  }

  setPermissions() {
    if(!this._canEdit()){
      this.active.disable();
    }
  }
  
  private _canEdit() {
    return this._authService.hasClaim(PERMISSIONS.ROLE.ADD_ROLE)
  }

  modelCreate() {
    return this.fb.group({
      name: ['', Validators.required],
      color: ['', Validators.required],
      active: [true]
    });
  }

  onSubmit() {
    
    this.blockUI.start();
    if (this.fishBoneForm.valid) {

      this._fishBone.active = this.active.value;
      this._fishBone.color = this.color.value;
      this._fishBone.name = this.name.value;

      if (this.isCreate) {
        this._fishBone.active = true
        this._FishBoneService.add(this._fishBone)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            this._toastrManager.successToastr('El estado se ha creado correctamente', 'Exito!')
            this._router.navigate(['/quality/corrective-actions/config/fish-bone']);
            this.blockUI.stop();
          })
      } else {
        this._FishBoneService.update(this._fishBone)
         .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            this._toastrManager.successToastr('El estado se ha actualizado correctamente', 'Éxito!');
            this._router.navigate(['/quality/corrective-actions/config/fish-bone']);
            this.blockUI.stop();
          })
      }
    } else {
      this.blockUI.stop();
    }
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
