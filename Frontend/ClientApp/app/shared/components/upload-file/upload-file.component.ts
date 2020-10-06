import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';

import { Attachment } from '../../models/attachment';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent implements OnInit {

  @ViewChild('file') file;

  @Input() canDownload: Boolean = true;
  @Input() canUpload: Boolean;
  @Input() canDelete: Boolean;
  @Input() title: String;
  @Input() _attachments: Attachment[] = [];
  @Input() _oldAttachments: Attachment[] = [];

  @Output() _attachmentsOutput = new EventEmitter<any>();

  private _newAttachments: Attachment[] = [];
  public files: Set<File> = new Set();

  constructor() { }

  ngOnInit() {

  }

  addFiles() {
    this.file.nativeElement.click();
  }

  onFilesAdded(): void {
    Array.prototype.forEach.call(this.file.nativeElement.files, f => {
      const newAttach = this._newAttachments.find(x => x.fileName === f.name);
      const oldAttach = this._oldAttachments.find(x => x.fileName === f.name);
      const attach = this._attachments.find(x => x.fileName === f.name);
      const attachNew = this._attachments.find(x => x.fileName === f.name && x.id === undefined);
      if (newAttach) {
        this.deleteFromArray(newAttach, this._newAttachments);
        if (attachNew) {
          this.removeFromAllAttachments(attachNew);
        }
      } else if (oldAttach) {
        this.deleteFiles(attach);
        this.deleteFromArray(oldAttach, this._oldAttachments);
      }

      const files: { [key: string]: File} = this.file.nativeElement.files;
      for (const key in files) {
        if (!isNaN(parseInt(key))) {
          this.files.add(files[key]);
        }
      }

      this._attachmentsOutput.emit(this.files);

      // let reader = new FileReader();
      // reader.readAsDataURL(f);
      // let a = new Attachment();
      // reader.onload = () => {
      //   a.base64 = reader.result;
      //   a.fileName = f.name;
      //   a.isInsert = true;
      //   a.isDelete = false;
      //   this._newAttachments.push(a);
      //   this._attachments.push(a);
      //   this._attachmentsOutput.emit(this._attachments);
      // }
    });
  }

  test(tg) {
    console.log(tg);
  }

  removeFiles(f: any, aArray: Attachment[]): void {
    this.deleteFromArray(f, aArray);
    this.removeFromAllAttachments(f);
  }

  deleteFiles(f: any): void {
    const a = this._attachments.find(x => x.id == f.id);
    a.isDelete = true;
    a.isInsert = false;
    this.deleteFromArray(a, this._oldAttachments);
  }

  deleteFromArray(f: any, aArray: Attachment[]) {
    aArray.forEach( (item, index) => {
      if ((item.fileName === f.fileName || item.fileName === f.name)) { aArray.splice(index, 1); }
    });
    this._attachmentsOutput.emit(this._attachments);
  }

  removeFromAllAttachments(f: any): void {
    this._attachments.forEach( (item, index) => {
      if (item === f) { this._attachments.splice(index, 1); }
    });
    this._attachmentsOutput.emit(this._attachments);
  }

}
