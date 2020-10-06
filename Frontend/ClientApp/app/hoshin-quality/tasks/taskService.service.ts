import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'ClientApp/environments/environment';
import { EditTask } from './models/EditTask';

import { EditTaskUpdate } from './models/editTaskUpdate';
import { Task } from './models/Task';

const httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
        providedIn: 'root'
})

export class TaskService {
        constructor(private _httpClient: HttpClient) { }
        private TaskUrl = environment.BASE_URL + '/api/task/';

        get(id): Observable<EditTask> {
                return this._httpClient.get<EditTask>(this.TaskUrl + 'get/' + id);
        }

        update(task): Observable<EditTaskUpdate> {
                const formData = new FormData();
                task.TaskEvidences.forEach(file => { formData.append('TaskEvidences', file, file.name); });
                task.DeleteEvidencesUrls.forEach(fileName => { formData.append('DeleteEvidencesUrls', fileName); });
                task.TaskEvidences = [];
                task.DeleteEvidencesUrls = [];
                for (const prop in task) {
                        formData.append(prop, task[prop]);
                }
                return this._httpClient.post<EditTaskUpdate>(this.TaskUrl + 'updateTask', formData);
        }
        extendDueDate(task): Observable<Task> {
                return this._httpClient.post<Task>(environment.BASE_URL + '/api/extendDueDateTask', task, httpOptions)
        }
        overdueExtend(requestExtension) {

                return this._httpClient.post<any>(this.TaskUrl + 'overdueExtend', requestExtension );
        }

}
