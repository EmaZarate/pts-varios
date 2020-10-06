import { Component, OnInit, OnDestroy, AfterViewInit, ViewChildren, ElementRef, Injector } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControlName } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrManager } from 'ng6-toastr-notifications';
import { AuthService } from '../core/services/auth.service';
import { UsersService } from "../core/services/users.service";
import { LockScreenService } from '../core/services/lock-screen.service';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';
declare var $: any

@Component({
  selector: 'app-lock-screen',
  templateUrl: './lock-screen.component.html',
  styleUrls: ['./lock-screen.component.css']
})
export class LockScreenComponent implements OnInit, OnDestroy, AfterViewInit {

  @BlockUI() blockUI: NgBlockUI;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  paramUnlockedForm: FormGroup;
  get passwordUnlock() { return this.paramUnlockedForm.get('passwordUnlock'); }
  private ngUnsubscribe: Subject<void> = new Subject<void>();


  public userLogged: any;
  public validPassword: boolean = false;
  public errorMsg: string;
  public displayError: boolean;
  public urlImage: string;
  public randomImages: any = ['./assets/img/lock.jpg',
    './assets/img/bg-pricing.jpg',
    './assets/img/bg3.jpg',
    './assets/img/bg9.jpg',
    './assets/img/card-1.jpg',
    './assets/img/card-2.jpg',
    './assets/img/header-doc.jpg',
    './assets/img/register.jpg',
    './assets/img/login.jpg'
  ];

  constructor(private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _router: Router,
    private _AuthService: AuthService,
    private _userService: UsersService,
    private _lockScreenService: LockScreenService) {
    let token = localStorage.getItem("auth_token");
    if (!token) {
      this._router.navigate(['/login']);
    }
  }

  user;

  ngAfterViewInit(): void {

  }

  ngOnInit() {
    
      this._userService.user$
      .takeUntil(this.ngUnsubscribe)
      .subscribe(user =>{this.user = user})
      this.paramUnlockedForm = this.modelParamLocked();
      const body = document.getElementsByTagName('body')[0];
      body.classList.add('lock-page');
      body.classList.add('off-canvas-sidebar');
      const card = document.getElementsByClassName('card')[0];
      setTimeout(function () {
        // after 1000 ms we add the class animated to the login/register card
        card.classList.remove('card-hidden');
      }, 700);
  
      let locked = JSON.parse(localStorage.getItem("locked"));
      locked.isLocked = true;
      localStorage.setItem("locked", JSON.stringify(locked));
      this.getRandomImage();
      this.userLogged = this._AuthService.getUserLogged();
  }

  modelParamLocked() {
    return this.fb.group({
      passwordUnlock: ['', Validators.required]
    });
  }

  unLockedUser() {
    if (!this.paramUnlockedForm.get("passwordUnlock").valid
      && this.paramUnlockedForm.get("passwordUnlock").touched) {
      return;
    }

    this.blockUI.start();
    this._lockScreenService.checkPassword(this.userLogged.sub, this.passwordUnlock.value).subscribe(result => {
      if (result) {
        var locked = JSON.parse(localStorage.getItem("locked"));
        locked.isLocked = false;
        localStorage.setItem("locked", JSON.stringify(locked));
        this._router.navigate([locked.previousUrl || '/home']);
      }
    }, err => {
      //this._toastrManager.errorToastr('Contraseña incorrecta', "Error");
    });

    /*if (this.passwordUnlock.value == "admin") {
      
      var locked = JSON.parse(localStorage.getItem("locked"));
      locked.isLocked = false;
      localStorage.setItem("locked", JSON.stringify(locked));
      this._router.navigate([locked.previousUrl || '/home']);
    }
    else {
      this._toastrManager.errorToastr('Contraseña incorrecta', "Error");
    }*/
  }

  getRandomImage() {
    var num = Math.floor(Math.random() * this.randomImages.length);
    this.urlImage = this.randomImages[num];
    $("#imgLock").css("background-image", "url('" + this.urlImage + "')");
    $("#imgLock").css("background-size", "cover");
    $("#imgLock").css("background-position", "top center");
  }

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
    const body = document.getElementsByTagName('body')[0];
    body.classList.remove('lock-page');
    body.classList.remove('off-canvas-sidebar');
  }

  isFieldValid(form: FormGroup, field: string) {
    return !form.get(field).valid && form.get(field).touched;
  }

  passwordValidation(e) {
    if (e.length <= 0) {
      this.validPassword = false;
    }
    else {
      this.validPassword = true;
    }
  }

}
