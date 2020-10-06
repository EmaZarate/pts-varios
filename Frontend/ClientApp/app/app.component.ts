import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router} from '@angular/router';

import { LockScreenService } from "./core/services/lock-screen.service";
import { AuthService } from './core/services/auth.service';
import { SwUpdate } from '@angular/service-worker';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})



export class AppComponent implements OnInit, AfterViewInit {
    

    constructor(private _lockScreenService: LockScreenService
      ,private _router:Router, private authService: AuthService
      ,private swUpdate:SwUpdate) {
        this._lockScreenService.loadRouting();     

     }

    ngOnInit() {

      if(this.swUpdate.isEnabled) {
        this.swUpdate.available.subscribe(() => {
            if(confirm("Existe una nueva versión.¿Desea actualizar a la nueva versión?")){
                window.location.reload();
            }
        });
      }
      

      let locked = localStorage.getItem("locked")
      if(!locked){
       let locked = {
         isLocked: false
       }
       localStorage.setItem("locked",JSON.stringify(locked));
      }
    }

    ngAfterViewInit() {
      let locked = JSON.parse(localStorage.getItem("locked"));
      if (locked && locked.isLocked) {
        this._router.navigate(['/lock']);
      }
    }
}
