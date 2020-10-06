import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { Company } from '../../models/Company';
import { CompanyService } from 'ClientApp/app/core/services/company.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-create-edit-company',
  templateUrl: './create-edit-company.component.html',
  styleUrls: ['./create-edit-company.component.css']
})
export class CreateEditCompanyComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  companyForm: FormGroup;

  get businessName() { return this.companyForm.get('businessName'); }
  get cuit() { return this.companyForm.get('cuit'); }
  get address() { return this.companyForm.get('address'); }
  get phoneNumber() { return this.companyForm.get('phoneNumber'); }
  get logo() { return this.companyForm.get('logo'); }

  isCreate: boolean;
  srcLogo: String | ArrayBuffer;
  imgLogo: String | ArrayBuffer;
  company = new Company;

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private fb: FormBuilder,
    private _companyService: CompanyService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.companyForm = this.modelCreate();

    this._route.data
      .takeUntil(this.ngUnsubscribe)
      .subscribe((data) => {
      if(data.typeForm == 'new'){
        this.isCreate = true;
        this.blockUI.stop();
      }
      else{
        this.isCreate = false;
        this._route.params
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
          this.getCompany(res.id);
        });
      }
    });
  }

  getCompany(id){
    this._companyService.getOne(id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: Company) => {
        this.company = res;
        this.businessName.patchValue(this.company.businessName);
        this.cuit.patchValue(this.company.cuit);
        this.address.patchValue(this.company.address);
        this.phoneNumber.patchValue(this.company.phoneNumber);
        this.srcLogo = this.company.logo;
        this.blockUI.stop();
      });
  }

  modelCreate() {
    return this.fb.group({
      businessName: ['', Validators.required],
      cuit: ['', Validators.required],
      address: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      logo: ['']
    });
  }

  limpiarImagen($event) : void {
    this.imgLogo = "";
  }

  changeListener($event) : void {
    this.readThis($event.target);
  }
  
  readThis(inputValue: any): void {
    var file: File = inputValue.files[0];
    var myReader: FileReader = new FileReader();
  
    myReader.onloadend = (e) => {
      this.imgLogo = myReader.result;
    }

    myReader.readAsDataURL(file);
  }

  onSubmit(){
    if(this.companyForm.valid){
      this.blockUI.start();

      this.company.businessName = this.businessName.value;
      this.company.cuit = this.cuit.value;
      this.company.address = this.address.value;
      this.company.phoneNumber = this.phoneNumber.value;

      if(this.imgLogo != "" && this.imgLogo != undefined){
        this.company.logo = this.imgLogo;
      }

      if(this.isCreate){
        this._companyService.add(this.company)
          .takeUntil(this.ngUnsubscribe)
          .subscribe(() => {
            this._toastrManager.successToastr('La configuración de la empresa se ha actualizado correctamente', 'Éxito');
            this._router.navigate(['/core/company']);
            this.blockUI.stop();
          });
      }
      else{
        this._companyService.update(this.company)
          .takeUntil(this.ngUnsubscribe)
          .subscribe(() => {
            this._toastrManager.successToastr('La configuración de la empresa se ha actualizado correctamente', 'Éxito');
            this._router.navigate(['/core/company']);
            this.blockUI.stop();
          });
      }
    }
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
