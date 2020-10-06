import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { CoreModule } from '../core.module';

import { environment } from '../../../environments/environment';
import { Observable, Subject } from 'rxjs';


@Injectable({
  providedIn: CoreModule
})
export class HomeService {

  private findingsCountUrl = environment.BASE_URL + "/api/finding/getcount";  
  private auditsCountUrl = environment.BASE_URL+"/api/audit/getcount";
  private correctiveActionsCountUrl = environment.BASE_URL+"/api/correctiveaction/getcount";
  private taskCountUrl = environment.BASE_URL + "/api/task/getcount";

  constructor(
    private _httpClient: HttpClient
  ) { }

  public getFindingsCount() {    
    return this._httpClient.get<number>(this.findingsCountUrl).pipe(map(res => res));
  }
 
  public getAuditsCount(){
    return this._httpClient.get<number>(this.auditsCountUrl).pipe(map(resp => resp));
  }

  public getCorrectiveActionsCount(){
    return this._httpClient.get<number>(this.correctiveActionsCountUrl).pipe(map(res => res));
  }

  public getTasksCount(){
    return this._httpClient.get<number>(this.taskCountUrl).pipe(map(res => res));
  }


  upload(files: Set<File>): { [key: string]: {progress: Observable<number> } } {
   
    const status: { [key: string]: {progress: Observable<number>}} = {};
    const url = environment.BASE_URL + "/api/finding/uploadfiles";
    files.forEach(file => {
      const formData = new FormData();
      formData.append('file', file, file.name);

      const req = new HttpRequest('POST',url,formData, {
        reportProgress: true
      });

      const progress = new Subject<number>();

      this._httpClient.request(req).subscribe(event => {
        if(event.type === HttpEventType.UploadProgress){
          const percentDone = Math.round(100 * event.loaded / event.total);

          progress.next(percentDone);
        }
        else if (event instanceof HttpResponse){
          progress.complete();
        }
      });

      status[file.name] = {
        progress: progress.asObservable()
      }
    });

    return status;
  }

}
