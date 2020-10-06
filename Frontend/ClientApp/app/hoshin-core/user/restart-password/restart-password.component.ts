import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UsersService } from '../../../core/services/users.service';
import { PasswordValidation } from '../../../shared/validators/password-validator.validator';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { ToastrManager } from 'ng6-toastr-notifications';

@Component({
  selector: 'app-restart-password',
  templateUrl: './restart-password.component.html',
  styleUrls: ['./restart-password.component.css']
})
export class RestartPasswordComponent implements OnInit, OnDestroy{
  
  public ngUnsubscribe: Subject<void> = new Subject<void>();
  @BlockUI() blockUI: NgBlockUI;
  restartPasswordForm: FormGroup;
  _user: any = {};

  get password() { return this.restartPasswordForm.get('password'); }
  get repeatPassword() { return this.restartPasswordForm.get('repeatPassword'); }

  constructor(
    private _userService: UsersService,
    private fb: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.restartPasswordForm = this.modelCreate();
    this._userService.get(this._route.snapshot.params.id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe( (res) => {
        this._user = res;   
        console.log(this._user);   
        this.blockUI.stop();
      })
  }

  modelCreate() {
    return this.fb.group({
      password:['', Validators.required],
      repeatPassword:['', Validators.required]
    },
    {
      validator: PasswordValidation.MatchPassword
    });
  }

  onSubmit() {
    if(this.password.value != this.repeatPassword.value) return;
    if(!this.restartPasswordForm.valid) return;

    this.blockUI.start();

    this._user['password'] = this.password.value;
    this._user['repeatPassword'] = this.repeatPassword.value;

    this._userService.resetPassword(this._user)
    .takeUntil(this.ngUnsubscribe)
      .subscribe( (res) => {
        this._toastrManager.successToastr('La contraseña se ha cambiado correctamente', 'Éxito');
        this._router.navigate(['/core/users']);
        this.blockUI.stop();
      })
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
