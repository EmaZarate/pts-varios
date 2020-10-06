import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '../../../../../../node_modules/@angular/forms';
import { trigger, transition, animate, state, style } from '@angular/animations';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';



@Component({
  selector: 'app-edit-workgroup',
  templateUrl: './edit-workgroup.component.html',
  styleUrls: ['./edit-workgroup.component.css'],
  animations: [
    trigger('hasWorkgroup_off', [
      state('show', style({
        opacity: 100
      })),
      state('hide', style({
        opacity: 0
      })),
      transition('show <=> hide', [
        animate('0.2s')
      ]),
    ]),
    trigger('hasWorkgroup_on', [
      state('show', style({
        opacity: 100
      })),
      state('hide', style({
        opacity: 0
      })),
      transition('true <=> false', [
        animate('0.3s')
      ]),
    ]),
  ],
})
export class EditWorkgroupComponent implements OnInit, OnDestroy {
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  @Input() usersSectorPlant;
  @Input() usersArray;
  @Output() usersOutput = new EventEmitter()



  currentState_off;
  currentState_on;

  usersSelected = [];
  displayedUsers = [];

  get hasWorkgroup() { return this.workgroupForm.get('hasWorkgroup') }

  workgroupForm: FormGroup;

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.workgroupForm = this.modelCreate(this.usersArray.length>0);
    this.initializeAnimationValues();

    this.usersSelected = this.usersArray.map((el) => {
      return {
        id: el.userID,
        display: el.users.firstName + " " + el.users.lastName
      }
    })
    
    this.hasWorkgroup.valueChanges
      .takeUntil(this.ngUnsubscribe)
      .subscribe(this.removeUsersFromWorkgroup)
  }

  initializeAnimationValues() {
    this.currentState_on =  this.usersArray.length>0 ? 'show' : 'hide';
    this.currentState_off =  this.usersArray.length>0 ? 'hide' : 'show';
  }

  removeUsersFromWorkgroup = (value) => {
    if (!value) {
      this.usersSelected = [];
      this.change();
    }
  }

  change() {
    this.usersOutput.emit(this.usersSelected)
  }

  ngOnChanges(changes) {
    if (changes.usersSectorPlant && !changes.usersSectorPlant.firstChange) {
      this.mapToDisplayUsers(changes.usersSectorPlant.currentValue);
    }
  }

  mapToDisplayUsers(userSectorPlantArray: Array<any>) {
    const usersSelectedIds = this.usersSelected.map((el) => el.id);
    this.displayedUsers = userSectorPlantArray
      .filter((user) => !usersSelectedIds.includes(user.id))
  }

  changeState() {
    this.currentState_off = this.currentState_off === 'hide' ? 'show' : 'hide';
    this.currentState_on = this.currentState_on === 'hide' ? 'show' : 'hide';
  }




  modelCreate(initialValue) {
    return this.fb.group({
      hasWorkgroup: [initialValue]
    });
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
