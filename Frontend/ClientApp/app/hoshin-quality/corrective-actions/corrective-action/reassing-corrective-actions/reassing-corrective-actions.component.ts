import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { FormGroup, FormControlName, Validators, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';
import { AuthService } from "../../../../core/services/auth.service";
import { CorrectiveActionService } from '../../corrective-action.service';
import { CorrectiveAction } from '../../models/CorrectiveAction';

@Component({
  selector: 'app-reassing-corrective-actions',
  templateUrl: './reassing-corrective-actions.component.html',
  styleUrls: ['./reassing-corrective-actions.component.css']
})
export class ReassingCorrectiveActionsComponent implements OnInit {
  @BlockUI() blockUI: NgBlockUI;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  reassingCorrectiveActionForm: FormGroup;
  _correctiveActions: any
  _users: any
  _userLogged: any;
  _correctiveAction: CorrectiveAction = new CorrectiveAction();
  get user() { return this.reassingCorrectiveActionForm.get('user'); }

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _correctiveActionsService: CorrectiveActionService,
    private _authService: AuthService,
  ) { }

  ngOnInit() {
    this.blockUI.start();
    const that = this;
    this.reassingCorrectiveActionForm = this.modelCreate();
    this._route.params.subscribe((res) => {
      this._correctiveActionsService.getOne(res.id)
      .takeUntil(this.ngUnsubscribe)
        .subscribe((res: CorrectiveAction) => {
          this._correctiveActions = res; 
          //console.log(this._correctiveActions); 
          this._correctiveActionsService.getAllUsers(this._correctiveActions.sectorTreatmentID, this._correctiveActions.plantTreatmentID)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res: any[]) => {
            this._users = res.filter((el) => el.id != this._correctiveActions.responsibleUserID && el.active);
            this._userLogged = this._authService.getUserLogged();
            this.blockUI.stop();
          })
        })
    })
  }

  modelCreate() {
    return this.fb.group({
      user: ['', Validators.required]
    });
  }

  onSubmit() {
    if(!this.reassingCorrectiveActionForm.valid) return;
    this.blockUI.start();
    this._correctiveActions.lastResponsibleUserID = this._correctiveActions.responsibleUserID;
    this._correctiveActions.responsibleUserID = this.user.value;
    this._correctiveActionsService.UpdateACReassign(this._correctiveActions)
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res:any) =>{
      this._toastrManager.successToastr('La accion correctiva se ha reasignado correctamente', 'Éxito');
      this._router.navigate(['/quality/corrective-actions']);
      this.blockUI.stop();
    });
  }
  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
