import { Component, OnInit, ElementRef, ViewChildren, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControlName } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';

import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { EditExpirationdateFindingService } from '../../edit-expirationdate-finding.service';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

import { setValidatedDate } from 'ClientApp/app/shared/util/dates/set-validated-date.function';

import * as moment from 'moment';

@Component({
  selector: 'app-edit-expirationdate-finding',
  templateUrl: './edit-expirationdate-finding.component.html',
  styleUrls: ['./edit-expirationdate-finding.component.css'],
})
export class EditExpirationdateFindingComponent implements OnInit, OnDestroy {
  
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  editExpirationDateFindingForm: FormGroup;
  _finding: any;
  
  idLabel: Number;
  descriptionLabel: String;

  get expirationDate() { return this.editExpirationDateFindingForm.get('expirationDate'); }

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _editExpirationDateFindingService: EditExpirationdateFindingService,
    private _authService: AuthService
) { }


  ngOnInit() {
    this.blockUI.start();
    this.editExpirationDateFindingForm = this.modelCreate();
    this._route.params.subscribe((res)=> {
        this._editExpirationDateFindingService.get(res.id)
              .takeUntil(this.ngUnsubscribe)
              .subscribe((f: any)=> {
                this._finding = f;
                this.idLabel = this._finding.id;
                this.descriptionLabel = this._finding.description;
                this.patchFinding();
                this.blockUI.stop();
              })
    });
  }
  
  patchFinding() {
    setValidatedDate(this.expirationDate, this._finding.expirationDate);
  }

  modelCreate() {
      return this.fb.group({
          expirationDate: ['', Validators.required]
      });
  }

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  onSubmit() {
    if(!this.editExpirationDateFindingForm.valid) return;
    
    if (this._finding.findingState.name != 'Vencido') {
      this._toastrManager.errorToastr("El estado del hallazgo debe ser vencido", "Se produjo un error", {position: 'bottom-right'});
      return;
    }
    if (moment(this.expirationDate.value) <= moment(Date.now())) {
      this._toastrManager.errorToastr("La fecha de vencimiento debe ser mayor a la fecha actual", "Se produjo un error", {position: 'bottom-right'});
      return;
    }
    
    this.blockUI.start();
    this._finding.expirationDate = this.expirationDate.value;
    this._finding.createdByUserId = this._authService.getUserLogged().id;
    
    this._editExpirationDateFindingService.editExpirationdateFinding(this._finding)
    .takeUntil(this.ngUnsubscribe)  
    .subscribe((res) => {
        this._toastrManager.successToastr('La fecha de vencimiento del hallazgo se ha actualizado correctamente', 'Éxito');
        this._router.navigate(['/quality/finding']);
        this.blockUI.stop();
      });
  }
}
