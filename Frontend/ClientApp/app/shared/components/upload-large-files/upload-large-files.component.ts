import { Component, OnInit, Input, ViewChild, Output, EventEmitter, OnDestroy } from '@angular/core';
import { forkJoin, Subject } from 'rxjs';
import {enableProdMode} from '@angular/core';
import { FormControl } from '@angular/forms';
@Component({
  selector: 'app-upload-large-files',
  templateUrl: './upload-large-files.component.html',
  styleUrls: ['./upload-large-files.component.css']
})
export class UploadLargeFilesComponent implements OnInit, OnDestroy {
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  fileInput: FormControl = new FormControl('');

  constructor() {
  }

  ngOnInit() {
    //Clone initial attachments array
    // this.cloneInitialDocs();
    this.fileInput.valueChanges
    .takeUntil(this.ngUnsubscribe)
    .subscribe(() => console.log('TRIGGERED'));
  }

  ngOnChanges(change) {
    if (change._attachments && change._attachments.currentValue) {
      this.cloneInitialDocs();
    }
  }

  cloneInitialDocs() {
    this._oldAttachments = JSON.parse(JSON.stringify(this._attachments));
  }

  @ViewChild('file') file;

  /**
   * if true the component will be responsible to send data at the specified endpoint
   */
  @Input() canSendData = false;

  /**
   * Allows to download attachments
   */
  @Input() canDownload: Boolean = true;

  /**
   * Upload new attachments behavior
   */
  @Input() canUpload: Boolean = true;

  /**
   * Delete attachments uploaded behavior
   */
  @Input() canDelete: Boolean = true;

  /**
   * Label for formControl
   */
  @Input() title: String;

  /**
   * Text for save button if canSendData = true
   */
  @Input() saveButtonText: String = 'Guardar'

  /**
   * Initial attachments
  */
  @Input() _attachments: Array<MyFile> = [];



  //Attachments added
  @Input() _newAttachments: Array<MyFile> = [];

  //Attachments wich already have
  _oldAttachments: Array<MyFile> = [];

  /**
   * Return the list of attachments in each change
   */
  @Output() updatedAttachmentsEmitter = new EventEmitter<any>();


  // public _newAttachments: Set<File> = new Set();

  addFiles() {
    this.file.nativeElement.value = '';
    this.file.nativeElement.click();
  }

  files = new Array<MyFile>();


  removeFiles(f: any, aArray): void {
    this.deleteFromArray(f, aArray);
    this.removeFromAllAttachments(f);
  }

  deleteFiles(f: any): void {
    const a = this._attachments.find(x => x.id == f.id);
    a.isDelete = true;
    a.isInsert = false;
    this.deleteFromArray(a, this._oldAttachments);
  }

  deleteFromArray(f: any, aArray) {
    aArray.forEach( (item, index) => {
      if ((item.fileName === f.name || item.name === f.name)) { aArray.splice(index, 1); }
    });
    this.updatedAttachmentsEmitter.emit(this._attachments);
  }

  removeFromAllAttachments(f: any): void {
    this._attachments.forEach( (item, index) => {
      if (item === f) { this._attachments.splice(index, 1); }
    });
    this.updatedAttachmentsEmitter.emit(this._attachments);
  }


  onFilesAdded(): void {
    debugger
    Array.prototype.forEach.call(this.file.nativeElement.files, (f: MyFile) => {
      const newAttach = this._newAttachments.find(x => x.name === f.name);
      const oldAttach = this._oldAttachments.find(x => x.name === f.name);
      const attach = this._attachments.find(x => x.name === f.name);
      const attachNew = this._attachments.find(x => x.name === f.name && x.id === undefined);
      if (newAttach) {
        this.deleteFromArray(newAttach, this._newAttachments);
        if (attachNew) {
          this.removeFromAllAttachments(attachNew);
        }
      } else if (oldAttach) {
        this.deleteFiles(attach);
        this.deleteFromArray(oldAttach, this._oldAttachments);
      }

      f.isInsert = true;
      f.isDelete = false;

      this.files.push(f);

      this._newAttachments.push(f);
      this._attachments.push(f);

      this.updatedAttachmentsEmitter.emit(this._attachments);
    });
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}


export interface MyFile extends File {
  isInsert;
  isDelete;
  id;
  url;
}
