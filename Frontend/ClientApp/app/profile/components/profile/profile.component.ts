import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from "../../../core/services/auth.service";
import { UsersService } from "../../../core/services/users.service";
import { Subject } from 'rxjs';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { User } from "../../../shared/models/user";
import { ToastrManager } from 'ng6-toastr-notifications';



class ImageSnippet {
  constructor(public src: string, public file: File) {}
}

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit, OnDestroy {

  public ngUnsubscribe: Subject<void> = new Subject<void>();
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  @BlockUI() blockUI: NgBlockUI;

  constructor(
    private _route: ActivatedRoute,
    private fb : FormBuilder,
    private _authService: AuthService,
    private _usersService: UsersService,
    private _router: Router,
    private _toastrManager: ToastrManager,) { }

  isEdit = false;
  photoSelected = false;
  user = new User();
  img;
  
  selectedFile: ImageSnippet;

  profileForm: FormGroup;

  get firstname() { return this.profileForm.get('firstname');}
  get surname() { return this.profileForm.get('surname');}
  get email() { return this.profileForm.get('email');}
  get phone() { return this.profileForm.get('phone');}
  get address() { return this.profileForm.get('address');}
  get sector() { return this.profileForm.get('sector');}
  get plant() { return this.profileForm.get('plant');}
  get photo() { return this.profileForm.get('phone');}

  ngOnInit() {
    //this.blockUI.start('Cargando Perfil')
    this.profileForm = this.modelCreate();
    this._usersService.user$.subscribe(res => {
      debugger
      this.user = res
      this.patchUser(this.user);
    })
    // let userLogged = this._authService.getUserLogged();
    // this._usersService.get(userLogged.id)
    // .takeUntil(this.ngUnsubscribe)
    // .subscribe(user =>{
    //   this._usersService.setUser(user);
    //   this.user = user;
    //   this.img = this.user.base64Profile;
    //   this.patchUser(user);
    //   this.blockUI.stop()
    // })
    if(this._route.snapshot.data.typeForm == 'edit'){
       this.isEdit = true;
    }
    this.disabledForm(this.isEdit)
  }

  patchUser(user){
    this.img = this.user.base64Profile;
    this.firstname.patchValue(user.name);
    this.surname.patchValue(user.surname);
    this.email.patchValue(user.username);
    this.phone.patchValue(user.phoneNumber);
    this.address.patchValue(user.address);
    this.sector.patchValue(user.sector);
    this.plant.patchValue(user.plant);
  }

  disabledForm(edit){
    this.profileForm.get('firstname').disable()
    this.profileForm.get('surname').disable()
    this.profileForm.get('email').disable()
    this.profileForm.get('sector').disable()
    this.profileForm.get('plant').disable()
    if(!edit){
      this.profileForm.get('address').disable()
      this.profileForm.get('phone').disable()
    }
  }

  modelCreate(){
    return this.fb.group({
      firstname: ['', ],
      surname:[''],
      email:[''],
      phone:['', Validators.required],
      address:['', Validators.required],
      sector:[''],
      plant:[''],
      photo:['']
    })
  }
  changePhoto(event){
    
    const reader = new FileReader();
    const file = event.target.files[0];
    
    reader.readAsDataURL(file);
    reader.onload = () => {

      this.img = reader.result
    };
  }

  deletedPhoto(){
    this.photoSelected = !this.photoSelected;
    this.img = this.user.base64Profile;
  }

  onSubmit() {
    debugger
    var i = this.photo.value;
    if(this.img != null){
      this.user.base64Profile = this.img;
    }
    this.user.firstname = this.user.name;
    this.user.phoneNumber = this.phone.value;
    this.user.address = this.address.value;
    this.blockUI.start("Cargando Datos");
    
    this._usersService.update(this.user).subscribe((res) => {
      this._usersService.setUser(res);
      this._toastrManager.successToastr('Perfil actualizado', 'Éxito');
       this._router.navigate(['/profile/view']);
       this.blockUI.stop()
    })
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
