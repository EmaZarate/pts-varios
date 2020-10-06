import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CorrectiveAction } from '../../models/CorrectiveAction';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrManager } from 'ng6-toastr-notifications';
import { CorrectiveActionService } from '../../corrective-action.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-extend-due-date',
  templateUrl: './extend-due-date.component.html',
  styleUrls: ['./extend-due-date.component.css']
})
export class ExtendDueDateComponent implements OnInit, OnDestroy {

  private ngUnsubscribe: Subject<void> = new Subject<void>();
  
  constructor(private _route: ActivatedRoute,private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _correctiveActionService: CorrectiveActionService,
    private _router: Router
    ) { }

  get newDate() { return this.extendDueDateForm.get('newDate') };

  date
  correctiveAction: CorrectiveAction;
  extendDueDateForm: FormGroup
  typeDate: string;
  isUpdatePlanning: boolean = false;

  ngOnInit() {
    this.correctiveAction = this._route.snapshot.data.correctiveAction;
    console.log(this.correctiveAction)
    if(this.correctiveAction.correctiveActionState.code == 'EFP'){
      this.typeDate = 'planificación'
      this.isUpdatePlanning = true;
    }
    else{
      this.typeDate = 'evaluación'
    }

    this.extendDueDateForm = this.modelCreate();
  }

  modelCreate() {
    return this.fb.group({
      newDate: ['', Validators.required]
    });
  }

  validDate() {
    if (this.extendDueDateForm.valid) {
      let newDate = new Date(this.newDate.value);
      newDate.setHours(0, 0, 0, 0);
      let today = new Date();
      if (newDate.getTime() > today.getTime()) {
        return true;
      }
      this._toastrManager.errorToastr('Ingrese una fecha mayor a la de hoy', 'Error');
      return false;
    }
    else {
      this._toastrManager.errorToastr('Ingrese una fecha', 'Error');
    }
  }

  onSubmit(){
    if(this.validDate()){
      if(this.isUpdatePlanning){
        this.correctiveAction.deadlineDatePlanification = this.newDate.value;
      }
      else{
        this.correctiveAction.deadlineDateEvaluation = this.newDate.value;
      }
        this._correctiveActionService.extendDueDate(this.correctiveAction)
        .takeUntil(this.ngUnsubscribe)
        .subscribe(() =>{
          this._toastrManager.successToastr(`Se ha extendido la fecha de ${this.isUpdatePlanning ? 'planificación' : 'evaluación'} , Éxito`);
          this._router.navigate(['/quality/corrective-actions/list']);
        });
    }
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
