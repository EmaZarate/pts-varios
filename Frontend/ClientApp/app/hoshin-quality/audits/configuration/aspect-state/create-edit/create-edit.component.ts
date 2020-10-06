import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrManager } from 'ng6-toastr-notifications';
import { AspectStateService } from "../../../aspect-state.service";
import { AspectsStates } from "../../../models/AspectsStates";
import { Subject } from 'rxjs';
import * as PERMISSIONS from "../../../../../core/permissions/index";
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-create-edit',
  templateUrl: './create-edit.component.html',
  styleUrls: ['./create-edit.component.css']
})
export class CreateEditComponent implements OnInit, OnDestroy {
  color: string;

  @BlockUI() blockUI: NgBlockUI;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  private ngUnsibscribe: Subject<void> = new Subject<void>();
  aspectsStatesForm: FormGroup;

  _aspectsState = new AspectsStates();

  get name() { return this.aspectsStatesForm.get('name') }
  get colour() { return this.aspectsStatesForm.get('colour') }
  get active() { return this.aspectsStatesForm.get('active') }

  isCreate: boolean;
  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private fb: FormBuilder,
    private _aspectStateService: AspectStateService,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.aspectsStatesForm = this.modelCreate();
    this.color = this.aspectsStatesForm.get('colour').value;
    debugger
    this._route.data
    .takeUntil(this.ngUnsibscribe)    
    .subscribe((data) => {
      debugger
      if(data.typeForm == 'new'){
        this.isCreate = true;
        this.blockUI.stop();
      }
      else{
        this.isCreate = false;
        this._route.params
        .takeUntil(this.ngUnsibscribe)
        .subscribe((res)=> {
          this._aspectStateService.get(res.id)
                 .takeUntil(this.ngUnsibscribe)
                .subscribe((res: AspectsStates)=>{
                  this._aspectsState = res;
                  this.name.patchValue(this._aspectsState.name);
                  this.colour.patchValue(this._aspectsState.colour);
                  this.active.patchValue(this._aspectsState.active);
                  this.blockUI.stop();
                })
        });
      }
    });
    this.setPermissions();
  }

  modelCreate(){
    return this.fb.group({
      name: ['', Validators.required],
      colour: ['', Validators.required],
      active:[false]
    });
  }

  onSubmit(){
    this.blockUI.start();
    debugger
    if(this.aspectsStatesForm.valid){
      //console.log(this.findingsStatesForm);
      this._aspectsState.name = this.name.value;
      this._aspectsState.colour = this.colour.value;
      this._aspectsState.active = this.active.value;
      if(this.isCreate){
        debugger
        this._aspectStateService.add(this._aspectsState)
          .takeUntil(this.ngUnsibscribe)
          .subscribe((res) => {
            //success
            ////console.log(res);
            this._toastrManager.successToastr('El estado se ha creado correctamente', 'Éxito!')
            this._router.navigate(['/quality/audits/config/aspect-states']);
            this.blockUI.stop();
          })
      }
      else{
        this._aspectStateService.update(this._aspectsState)
          .subscribe((res) =>{
            //success
            ////console.log(res);
            this._toastrManager.successToastr('El estado se ha actualizado correctamente', 'Éxito!')
            this._router.navigate(['/quality/audits/config/aspect-states']);
            this.blockUI.stop();
          })
      }
    }
    else {
      this.blockUI.stop();
    }
  }
  setPermissions() {
    if(!this._canEdit()){
      this.name.disable();
      this.active.disable();
    }
  }
  private _canEdit() {
    return this._authService.hasClaim(PERMISSIONS.ROLE.ADD_ROLE);
  }
  ngOnDestroy(){
    this.ngUnsibscribe.next();
    this.ngUnsibscribe.complete();
  }

}
