import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';

import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { PasswordValidation } from '../../../shared/validators/password-validator.validator';

import { PlantsService } from '../../../core/services/plants.service';
import { UsersService } from '../../../core/services/users.service';
import { RolesService } from '../../../core/services/roles.service';
import { User } from '../../../shared/models/user';



@Component({
  selector: 'app-create-edit-user',
  templateUrl: './create-edit-user.component.html',
  styleUrls: ['./create-edit-user.component.css']
})
export class CreateEditUserComponent implements OnInit, OnDestroy {

  public ngUnsubscribe: Subject<void> = new Subject<void>();

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  @BlockUI() blockUI: NgBlockUI;

  userForm: FormGroup;

  _user: any = {};
  allPlants = [];
  sectorsOfPlant = [];
  roles = [];
  basicRole = [];

  isCreate = true;

  jobsOfSectorPlant = [];

  get email() { return this.userForm.get('email'); }
  get password() { return this.userForm.get('password'); }
  get repeatPassword() { return this.userForm.get('repeatPassword'); }
  get firstname() { return this.userForm.get('firstname'); }
  get surname() { return this.userForm.get('surname'); }
  get plant() { return this.userForm.get('plant'); }
  get sector() { return this.userForm.get('sector'); }
  get job() { return this.userForm.get('job'); }
  get role() { return this.userForm.get('role')}
  get active() { return this.userForm.get('active')}


  constructor(
    private _route: ActivatedRoute,
    private fb: FormBuilder,
    private _router : Router,
    private _toastrManager: ToastrManager,
    private _userService: UsersService,
    private _roleService: RolesService,
    private _plantsService: PlantsService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.userForm = this.modelCreate();  
    this.sector.disable();
    this.job.disable();
    
    this._plantsService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        //console.log(res);
        this.allPlants = res;

        if(this._route.snapshot.data.typeForm == 'edit'){
          this.password.clearValidators();
          this.repeatPassword.clearValidators();
          this.password.updateValueAndValidity();
          this.repeatPassword.updateValueAndValidity();
          this.userForm.clearValidators();
          this.userForm.updateValueAndValidity();

          this._userService.get(this._route.snapshot.params.id)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            this._user = res;
            this._user.repeatPassword = this._user.password;
            this.patchUser(res);
            this.isCreate = false;
            this.blockUI.stop();
          })
        }else{
          this.blockUI.stop();
        }
      });

    this._roleService.getAllRolesActive()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        this.roles = res;
        console.log(this.roles)
        this.findBasicRole();
      });
  }

  findBasicRole(){
    this.basicRole = this.roles.filter((rol, index, array)=>{
       return rol.basic == true
    })
    this.roles = this.roles.filter((rol, index, array)=>{
      return rol.basic == false
   })
  }

  patchUser(user){
    this.email.patchValue(user.username);
    this.firstname.patchValue(user.name);
    this.surname.patchValue(user.surname);
    this.changeSelectedPlant(user.plantID);
    this.changeSelectedSector(user.sectorID);
    this.job.patchValue(user.jobID);
    this.active.patchValue(user.active);
    this.role.patchValue(user.roles);
  }

  modelCreate(){
    return this.fb.group({
      email: ['', [Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]],
      password:['', Validators.required],
      repeatPassword:['', Validators.required],
      firstname:['', Validators.required],
      surname:['', Validators.required],
      plant:['', Validators.required],
      sector:['', Validators.required],
      job:['', Validators.required],
      active:[true, Validators.required],
      role: [[]]
    },{
      validator: PasswordValidation.MatchPassword
    });
  }

  isFieldValid(form: FormGroup, field: string) {
    return !form.get(field).valid && form.get(field).touched;
  }

  isFormValid(form: FormGroup, field: string) {
    return !(form.errors && form.errors.MatchPassword) && form.get(field).touched;
  }

  changeSelectedPlant(val){
    let pl = this.allPlants.find(x => x.plantID == val);
    this.plant.patchValue(val);
    this.sectorsOfPlant = pl.sectors;
    this.sector.enable();
  }

  changeSelectedSector(val){
    let sec = this.sectorsOfPlant.find(x => x.id == val);
    this.sector.patchValue(val);
    this.jobsOfSectorPlant = sec.jobs;
    this.job.enable();
  }

  onSubmit(){
    if(this.password.value != this.repeatPassword.value && this._route.snapshot.data.typeForm == 'new') return;
    if(!this.userForm.valid) return;

    this.blockUI.start();
    
    // let user = {
    //   id: "",
    //   username: this.email.value,
    //   password: this.password.value,
    //   firstname: this.firstname.value,
    //   surname: this.surname.value,
    //   plantId: this.plant.value,
    //   sectorId: this.sector.value,
    //   jobId: this.job.value,
    //   roles: this.role.value,
    // }
    
    this._user["username"] = this.email.value;
    this._user["password"] = this.password.value;
    this._user["firstname"] = this.firstname.value;
    this._user["surname"] = this.surname.value;
    this._user["plantId"] = this.plant.value;
    this._user["sectorId"] = this.sector.value;
    this._user["jobId"] = this.job.value;
    this._user["active"] = this.active.value;
    let arrayRoles: String[] = this.role.value
    arrayRoles.push(this.basicRole[0].name);
    this._user["roles"] = arrayRoles;

    if(this.isCreate){
      this._userService.add(this._user)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this._toastrManager.successToastr('El usuario se ha creado correctamente', 'Éxito');
        this._router.navigate(['/core/users']);
        this.blockUI.stop();
      });
    }
    else{
      this._user.id = this._route.snapshot.params.id;
      this._userService.update(this._user)
       .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          console.log(res);
          this._toastrManager.successToastr('El usuario se ha actualizado correctamente', 'Éxito');
          this._router.navigate(['/core/users']);
          this.blockUI.stop();
        }) 
    }

    //console.log(user);
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
