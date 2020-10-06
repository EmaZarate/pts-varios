import { Component, OnInit, ElementRef, ViewChildren, Output, Input, EventEmitter } from '@angular/core';
import {
  FormGroup,
  FormControlName,
  FormBuilder,
  Validators
} from '@angular/forms';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';
import {RequestExtension} from '../../models/requestExtension';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-extend-overdure-date',
  templateUrl: './extend-overdure-date.component.html',
  styleUrls: ['./extend-overdure-date.component.css']
})
export class ExtendOverdureDateComponent implements OnInit {
  @BlockUI() blockUI: NgBlockUI;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  @Input() router: Router;
  @Output()   requestExtension = new EventEmitter<any>();
  overdueForm: FormGroup;

  get newDate() {
    return this.overdueForm.get('newDate');
  }
  get observation() {
    return this.overdueForm.get('observation');
  }
  constructor(
    private _toastrManager: ToastrManager,
    private _fb: FormBuilder,
    private _route : Router
  ) {}
  ngOnInit() {
    this.overdueForm = this.modelCreate();
  }

  modelCreate() {
    return this._fb.group({
      newDate: ['', Validators.required],
      observation: ['', Validators.required]
    });
  }

   Submit() {
     const currentDate = new Date();
     if (this.overdueForm.valid) {
    const date = this.newDate.value;
    if (date > currentDate) {
        const observation = this.observation.value;
        const dataRequestExtension = new RequestExtension();
        dataRequestExtension.date = date;
        dataRequestExtension.observation = observation;
        this.requestExtension.emit( dataRequestExtension);
        return;
      }
    this._toastrManager.errorToastr('Debe seleccionar una fecha posterior al dia de hoy');
    }
  }

  goBack() {
    this._route.navigate([this.router]);
  }
}
