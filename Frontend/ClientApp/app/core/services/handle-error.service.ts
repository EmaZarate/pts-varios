import {Injectable, ErrorHandler, Injector, ViewContainerRef} from "@angular/core";
import {Router} from "@angular/router";

import { ToastrManager } from 'ng6-toastr-notifications';

import { UNAUTHORIZED, BAD_REQUEST, FORBIDDEN, INTERNAL_SERVER_ERROR } from "http-status-codes";


@Injectable()
export class HandleErrorService implements ErrorHandler {

  static readonly REFRESH_PAGE_ON_TOAST_CLICK_MESSAGE: string = "An error occurred: Please click this message to refresh";
  static readonly DEFAULT_ERROR_TITLE: string = "Something went wrong..";

  constructor(
    private injector: Injector,
    ) { }

  public handleError(error: any) {
    const router = this.injector.get(Router);
    const toastManager = this.injector.get(ToastrManager);
    let httpErrorCode = error.httpErrorCode;
    if(httpErrorCode == null) httpErrorCode = error.status;

    switch (httpErrorCode) {
      case UNAUTHORIZED:
          router.navigateByUrl("/login");
          break;
      case FORBIDDEN:
          router.navigateByUrl("/login");
          break;
      case BAD_REQUEST:
        toastManager.errorToastr(error.error, 'Algo salio mal', {
          position: "bottom-right"
        })
          break;
      case INTERNAL_SERVER_ERROR:
        toastManager.errorToastr("Something went wrong, Internal server error!", "Oops :'(", {
          position: "bottom-right"
        });
        break;
      default:
        
        toastManager.errorToastr(error.error, "Error "+httpErrorCode);
    }
  }

}
