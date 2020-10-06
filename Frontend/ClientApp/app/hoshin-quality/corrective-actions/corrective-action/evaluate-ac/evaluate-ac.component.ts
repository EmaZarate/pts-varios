import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ValueConverter } from '@angular/compiler/src/render3/view/template';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';
import { CorrectiveActionEvidence } from '../../models/CorrectiveActionEvidence';
import { CorrectiveAction } from '../../models/CorrectiveAction';
import { CorrectiveActionService } from '../../corrective-action.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-evaluate-ac',
  templateUrl: './evaluate-ac.component.html',
  styleUrls: ['./evaluate-ac.component.css']
})
export class EvaluateAcComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  evaluateForm: FormGroup;
  correctiveAction: CorrectiveAction = new CorrectiveAction();
  _correctiveActionsEvidences: CorrectiveActionEvidence[] = [];

  get evaluateCommentary() { return this.evaluateForm.get('evaluateCommentary'); }

  constructor(
    private formBuilder: FormBuilder,
    private _toastrManager: ToastrManager,
    private _correctiveActionService: CorrectiveActionService,
    private _route: ActivatedRoute,
    private _router: Router
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.correctiveAction.correctiveActionID = parseInt(this._route.snapshot.params.id);
    this.evaluateForm = this.modelCreate();
    this.blockUI.stop();
  }

  modelCreate() {
    return this.formBuilder.group({
      evaluateCommentary: ['', Validators.required]
    });
  }

  getAttachments(event): void {
    this._correctiveActionsEvidences = event;
  }

  submitEvaluation(isEffective: boolean) {
    if (this.evaluateForm.valid) {
      this.blockUI.start();
      this.correctiveAction.evaluationCommentary = this.evaluateCommentary.value;
      this.correctiveAction.Evidences = this._correctiveActionsEvidences.filter(x => x.isInsert);
      this.correctiveAction.isEffective = isEffective;
      this._correctiveActionService.evaluateCorrectiveAction(this.correctiveAction)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.blockUI.stop();
        const messageToastr = isEffective ? 'La evaluación eficaz fue exitosa' : 'La evaluación no eficaz fue exitosa';
        this._toastrManager.successToastr(messageToastr, 'Éxito');
        this._router.navigate(['/quality/corrective-actions/list']);
      });
    } else {
      this._toastrManager.errorToastr("Ingrese todos los campos obligatorios.")
    }
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
