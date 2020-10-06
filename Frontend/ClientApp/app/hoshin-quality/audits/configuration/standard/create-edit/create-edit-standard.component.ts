import { Component, OnInit, ViewChildren, ElementRef, ViewChild, OnDestroy, HostListener } from '@angular/core';
import { Standard } from '../../../models/Standard';
import { FormGroup, FormControlName, FormBuilder, Validators, FormGroupDirective } from '@angular/forms';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { Subject, BehaviorSubject, Observable } from 'rxjs';
import { DataSource } from '@angular/cdk/table';
import { StandardService } from '../../standard.service';
import { CollectionViewer } from '@angular/cdk/collections';
import { ToastrManager } from 'ng6-toastr-notifications';
import { ActivatedRoute, Router } from '@angular/router';
import { Aspect } from '../../../models/Aspect';
import Swal from 'sweetalert2/dist/sweetalert2'

declare var $: any;

@Component({
  selector: 'app-create-edit-standard',
  templateUrl: './create-edit-standard.component.html',
  styleUrls: ['./create-edit-standard.component.css']
})
export class CreateEditStandardComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  @HostListener('window:scroll', ['$event']) 

  private scroll: number;
  standardForm: FormGroup;
  aspectsForm: FormGroup;

  _standard: Standard;

  private ngUnsuscribe: Subject<void> = new Subject<void>();

  get name() { return this.standardForm.get('name'); }
  get code() { return this.standardForm.get('code'); }

  get codeAspect() { return this.aspectsForm.get('code'); }
  get title() { return this.aspectsForm.get('title'); }
  get aspectId() { return this.aspectsForm.get('aspectID'); }

  isCreate: boolean;
  dataSource: AspectDataSource;
  displayedColumns: string[] = ['AspectID', 'Code', 'Title', 'Activo', 'buttons'];

  constructor(
    private fb: FormBuilder,
    private _standardService: StandardService,
    private _toastrManager: ToastrManager,
    private _route: ActivatedRoute,
    private _router: Router
  ) { }

  ngOnInit() {
    this._standardService.clearAspectsArray();
    this.blockUI.start();
    this.standardForm = this.modelStandardCreate();
    this.aspectsForm = this.modelAspectCreate();
    this.dataSource = new AspectDataSource(this._standardService);

    if (this._route.snapshot.params.id != null) {
      this.isCreate = false;
      this._standardService.get(this._route.snapshot.params.id)
        .takeUntil(this.ngUnsuscribe)
        .subscribe((res: any) => {
          this._standard = res;
          let aspects: Aspect[]= this._standard.aspects;
          // var sortedStandard = tempArray.aspects.sort(this.compare(obj1,obj2));
          aspects.sort((a, b) => a.code < b.code ? -1 : a.code > b.code ? 1 : 0)
          this.name.patchValue(this._standard.name);
          this.code.patchValue(this._standard.code);

          this.dataSource.loadSolicitudes(false, aspects);
          this.aspectsForm.reset();
          this.blockUI.stop();
        });

    }
    else {

      this._standard = new Standard();
      this.isCreate = true;
      this.blockUI.stop();
    }

  }
  onSubmit() {
    this.blockUI.start();
    if (this.standardForm.valid) {

      this._standard.name = this.name.value;
      this._standard.code = this.code.value;
      this.blockUI.stop();
    } else {
      this.standardForm.get('code').markAsTouched();
      this.standardForm.get('name').markAsTouched();
      this.aspectsForm.get('code').markAsTouched();
      this.aspectsForm.get('title').markAsTouched();
      this.blockUI.stop();
    }

    if (!this.dataSource.length) {
      return;
    }



    if (this.isCreate) {
      this._standard.active = true;
      this._standardService.add(this._standard)
        .takeUntil(this.ngUnsuscribe)
        .subscribe((res) => {
          this._toastrManager.successToastr('La norma se ha creado correctamente', 'Éxito');
          this._router.navigate(['/quality/audits/config/standard']);
          this.blockUI.stop();
        });
    }
    else {
      this._standardService.update(this._standard)
        .takeUntil(this.ngUnsuscribe)
        .subscribe((res) => {
          this._toastrManager.successToastr('La norma se ha actualizado correctamente', 'Éxito');
          this._router.navigate(['/quality/audits/config/standard']);
          this.blockUI.stop();
        })
    }
  }

  modelStandardCreate() {
    return this.fb.group({
      name: ['', Validators.required],
      code: ['', Validators.required]
    });
  }

  modelAspectCreate() {
    return this.fb.group({
      code: ['', Validators.required],
      title: ['', Validators.required],
      aspectID: ['']
    });
  }

  ngOnDestroy() {
    this.ngUnsuscribe.next();
    this.ngUnsuscribe.complete();
  }

  addEditAspectsOnSubmit(formDirective: FormGroupDirective) {
    if (this.standardForm.invalid || this.aspectsForm.invalid) {
      // this.standardForm.get('code').markAsTouched();
      // this.standardForm.get('name').markAsTouched();
      this.aspectsForm.get('aspectID').markAsTouched();
      this.aspectsForm.get('title').markAsTouched();
      return;
    }

    let $btnAddEdit = $('#btnAddEditAspect');

    if ($btnAddEdit.hasClass('add')) {

      if (this.addAspect()) {
        setTimeout(() => {
          formDirective.resetForm();
          this.scrollTop();
        }, 50);

      }
    }
    else if ($btnAddEdit.hasClass('edit')) {
      if (this.acceptEditAspect()) {
        setTimeout(() => {
          formDirective.resetForm();
          this.scrollTop();
        }, 50);
      }
    }
  }

  addAspect() {

    if (this.validateIfExistInMemory()) {
      return false;
    }
    if(this._standard.aspects == undefined){
      this._standard.aspects = [];
    }
    let lastAspect = this._standardService.getLastAspectInMemory();

    let aspect = new Aspect();
    //aspect.aspectID = lastAspect != null ? lastAspect.aspectID - 1 : 0
    if(lastAspect != null){
      aspect.aspectID = lastAspect.aspectID - 1;
    }else{
      aspect.aspectID = 0;
    }
    aspect.code = this.codeAspect.value;
    aspect.title = this.title.value;
    aspect.active = true;
    if(this._standardService._aspects.length == 0){
      this._standardService.addAspect(aspect);
    }
    this._standard.aspects.push(aspect);
    console.log(this._standard)
    let aspects: Aspect[] = this._standard.aspects;
    aspects.sort((a, b) => a.code < b.code ? -1 : a.code > b.code ? 1 : 0)

    this.dataSource.loadSolicitudes(true, []);
    return true;
  }

  acceptEditAspect() {

    if (this.validateIfExistInMemory()) {
      return false;
    }

    let aspectId = this.aspectId.value;
    let code = this.codeAspect.value
    let title = this.title.value;

    this._standardService.updateAspectInMemory(aspectId, title, code, true);
    this.dataSource.loadSolicitudes(true, []);
    this.aspectsForm.reset();
    this.aspectSetMode('add');
    return true;
  }

  editAspect(aspectID) {

    let aspect = this._standardService.getAspectByIdInMemory(aspectID);
    this.codeAspect.patchValue(aspect.code);
    this.title.patchValue(aspect.title);
    this.aspectId.patchValue(aspect.aspectID);

    this.aspectSetMode('edit');
    this.scrollTop('edit');
  }
  scrollTop(type = null) {
    if(type === 'edit'){
      this.scroll = $(".main-panel.ps.ps--active-y").scrollTop();
      $(".main-panel.ps.ps--active-y").scrollTop(0);
    }else if(this.scroll){
      $(".main-panel.ps.ps--active-y").scrollTop(this.scroll);
      this.scroll = null;
    }
}

  updateStandardActive(aspect) {
    aspect.active = !aspect.active
    this._standardService.updateAspectInMemory(aspect.aspectID, aspect.title, aspect.code, aspect.active);
    this.dataSource.loadSolicitudes(true, []);

    if ($('#btnAddEditAspect').hasClass('edit')) {
      this.cancelEditAspect();
    }
  }

  cancelEditAspect() {
    this.aspectSetMode('add');
    this.aspectsForm.reset();
  }

  aspectSetMode(mode: string) {

    let $btnAddEdit = $('#btnAddEditAspect');
    let $icon = $btnAddEdit.find('i.material-icons');
    let $btnCancelEdit = $('#btnCancelEditAspect');

    if (mode == 'add') {
      $btnAddEdit.addClass('add').removeClass('edit');
      $icon.html('add');
      $btnCancelEdit.hide();

    } else if (mode == 'edit') {
      $btnAddEdit.addClass('edit').removeClass('add');
      $icon.html('done');
      $btnCancelEdit.show();
    }

  }

  validateIfExistInMemory() {

    let valid = this._standardService.validateAspect(this.aspectId.value, this.title.value, this.codeAspect.value);

    switch (valid) {
      case 0:
        return false;
      case 1:
        this._toastrManager.errorToastr('El código ingresada ya existe', 'Error');
        return true;
      default:
        return false;
    }

  }

  goListStandard() {

    let aspect = this._standardService.getLastAspectInMemory();
    if (aspect) {
      Swal.fire({
        text: 'Se perderan los datos no guardados. ¿Desea continuar?',
        type: 'question',
        showCancelButton: true,
        confirmButtonText: 'Si',
        cancelButtonText: 'No ',
        focusCancel: true
      }).then((result) => {
        if (result.value) {
          this._router.navigate(['/quality/audits/config/standard']);
        }
      });
    }
    else {
      this._router.navigate(['/quality/audits/config/standard']);
    }
  }
}

export class AspectDataSource extends DataSource<any> {


  private lessonsSubject = new BehaviorSubject<any[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();
  public length;
  constructor(
    private _standardService: StandardService
  ) {
    super();
    this.length = 0;
  }

  connect(collectionViewer: CollectionViewer): Observable<any[]> {
    return this.lessonsSubject.asObservable();
  }

  disconnect() {
    this.lessonsSubject.complete();
    this.loadingSubject.complete();
  }

  loadSolicitudes(isNew: boolean, aspects: any) {
    aspects = aspects.sort(function(a, b) {
      return a.code.localeCompare(b.code, undefined, {
        numeric: true,
        sensitivity: 'base'
      });
    });
    console.log(aspects);
    this.loadingSubject.next(true);

    if (isNew) {
      this._standardService.getAllAspectInMemory()
        .subscribe((aspects) => {
          this.length = aspects.length;
          this.lessonsSubject.next(aspects);
        });
    }
    else {
      this.length = aspects.length;
      this.lessonsSubject.next(aspects);
      this._standardService.setAspectInMemory(aspects);
    }
  }
}
