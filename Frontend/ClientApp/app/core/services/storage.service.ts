import { Injectable } from "@angular/core";

import "rxjs/add/operator/share";


@Injectable()
export abstract class StorageService {
  public abstract get(): Storage;

}
@Injectable()
export class LocalStorageService extends StorageService {
  public get(): Storage {
    return localStorage;
  }
}