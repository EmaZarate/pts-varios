import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AspectService } from '../../aspect.service';
import { Aspect } from '../../models/Aspect';
import { FindingsTypeService } from 'ClientApp/app/core/services/findings-type.service';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AuditStandardAspectService } from '../../audit-standard-aspect.service';
import { AuditStandardAspectFinding } from '../../models/AuditStandardAspectFinding';
import { FindingsService } from 'ClientApp/app/core/services/findings.service';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-add-finding',
  templateUrl: './add-finding.component.html',
  styleUrls: ['./add-finding.component.css']
})
export class AddFindingComponent implements OnInit, OnDestroy {

  private aspectId;
  private standardId;
  auditId;
  private findingId;

  aspect: Aspect;
  findingTypes;

  findingSelected;

  isEdit: boolean;

  findingForm: FormGroup;

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  
  constructor(
    private _route: ActivatedRoute,
    private _aspectService: AspectService,
    private _findingTypeService: FindingsTypeService,
    private _auditStandardAspectService: AuditStandardAspectService,
    private _findingService: FindingsService,
    private _fb: FormBuilder,
    private _router: Router
  ) { }

  get description() { return this.findingForm.get('description'); }
  get findingType() { return this.findingForm.get('findingType'); }

  ngOnInit() {
    this.blockUI.start();
    this.findingForm = this.createModel();
    this.getFindingTypes();

    if (this.isEditing()) {
      this.isEdit = true;
      this.findingId = this._route.snapshot.params.finding;
      this.getFinding();
    }
    else {
      this.getRouteParams();
      this.getAspectService();
    }
  }

  getFinding() {
    this._findingService.get(this.findingId)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(res => {
        this.findingSelected = res;
        this.patchValues();
        this.getAspectService();
      })
  }

  patchValues() {
    this.description.patchValue(this.findingSelected.description);
    this.findingType.patchValue(this.findingSelected.findingTypeID);
    this.aspectId = this.findingSelected.aspectID;
    this.auditId = this.findingSelected.auditID;
    this.standardId = this.findingSelected.standardID;
  }

  getRouteParams() {
    this.aspectId = this._route.snapshot.params.aspect;
    this.standardId = this._route.snapshot.params.standard;
    this.auditId = this._route.snapshot.params.id;

  }

  isEditing() {
    return this._route.snapshot.url.toString().includes('edit');
  }

  getAspectService() {
    this._aspectService.get(this.standardId, this.aspectId)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(res => {
        this.aspect = res;
        this.blockUI.stop();
      });
  }

  getFindingTypes() {
    this._findingTypeService.getAllForAudit()
      .takeUntil(this.ngUnsubscribe)
      .subscribe(res => {
        this.findingTypes = res;
        console.log(this.findingTypes);
      })
  }

  createModel() {
    return this._fb.group({
      description: ['', Validators.required],
      findingType: ['', Validators.required]
    });
  }

  onSubmit() {
    if(!this.findingForm.valid) return;
    this.blockUI.start("Guardando...");
    const finding: AuditStandardAspectFinding = {
      aspectID: this.aspectId,
      auditID: this.auditId,
      standardID: this.standardId,
      findingTypeID: this.findingType.value,
      description: this.description.value,
      findingID: this.findingId == null ? 0 : this.findingId
    };

    if (this.isEdit) {
      this._findingService.update(finding)
        .takeUntil(this.ngUnsubscribe)
        .subscribe(res => {
          this.goBack();
        })
    }
    else {
      this._auditStandardAspectService.addFinding(finding)
        .takeUntil(this.ngUnsubscribe)
        .subscribe(res => {
          this.goBack();
        });
    }
  }

  goBack(){
    this._router.navigate(['/quality/audits/',this.auditId,'report']);
    this.blockUI.stop();
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
