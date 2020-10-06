import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { Location,PopStateEvent} from '@angular/common';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
declare var require: any
var moment = require("moment");


import PerfectScrollbar from 'perfect-scrollbar';
import { HeaderComponent } from './components/header/header.component';
import { LockScreenService } from '../core/services/lock-screen.service';
import { UsersService } from "../core/services/users.service";
import { AuthService } from '../core/services/auth.service';
import { Subject } from 'rxjs';


declare var $: any;

export interface DropdownLink {
  title: string;
  iconClass?: string;
  routerLink?: string;
}

export enum NavItemType {
  Sidebar = 1, // Only ever shown on sidebar
  NavbarLeft = 2, // Left-aligned icon-only link on navbar in desktop mode, shown above sidebar items on collapsed sidebar in mobile mode
  NavbarRight = 3 // Right-aligned link on navbar in desktop mode, shown above sidebar items on collapsed sidebar in mobile mode
}

export interface NavItem {
  type: NavItemType;
  title: string;
  routerLink?: string;
  iconClass?: string;
  numNotifications?: number;
  dropdownItems?: (DropdownLink | 'separator')[];
}

@Component({
  selector: 'app-inside-routing',
  templateUrl: './inside-routing.component.html',
  styleUrls: ['./inside-routing.component.css']
})

export class InsideRoutingComponent implements OnInit, OnDestroy {
  blockTemplate: any;
  private _router: Subscription;
  private lastPoppedUrl: string;  
  private yScrollStack: number[] = [];
  timeMouse:any;
  url: string;
  location: Location;  
  subscription: Subscription;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
   @ViewChild('sidebar') sidebar: any;
   @ViewChild(HeaderComponent) header: HeaderComponent;

   
   constructor(private router: Router,
               location: Location,
               private _lockScreenService:LockScreenService,
               private _userService: UsersService,
               private _authService: AuthService) {

    this.timeMouse =  moment();

    this.subscription = Observable.fromEvent(document, 'keydown').subscribe(e => {
        let key = <KeyboardEvent>e;
        if (key.keyCode == 66 && key.ctrlKey) {
            this.router.navigateByUrl('/lock');
        }
    });

    this.subscription = Observable.fromEvent(document, 'mouseover').subscribe(e => {
        this.timeMouse =  moment();
    });
    

    setInterval(() => {

        if (moment(this.timeMouse).isSameOrBefore(moment().add((_lockScreenService.getLockScreenTime() * -1), 'minutes'))) {
            this.router.navigateByUrl('/lock');
        }
    }, 15000);

    this.location = location;
  }

  ngOnInit() {
    let userLogged = this._authService.getUserLogged();
    this._userService.get(userLogged.id)
    .takeUntil(this.ngUnsubscribe)
    .subscribe((user)=>{
          this._userService.setUser(user)
      })
    const elemMainPanel = <HTMLElement>document.querySelector('.main-panel');
    const elemSidebar = <HTMLElement>document.querySelector('.sidebar .sidebar-wrapper');
     
    this.location
    .subscribe((ev:PopStateEvent) => {
          
          this.lastPoppedUrl = ev.url;
      });

    this.router.events
    .takeUntil(this.ngUnsubscribe)
    .subscribe((event:any) => {
          if (event instanceof NavigationStart) {
             if (event.url != this.lastPoppedUrl)
                 this.yScrollStack.push(window.scrollY);
         } else if (event instanceof NavigationEnd) {
             if (event.url == this.lastPoppedUrl) {
                 this.lastPoppedUrl = undefined;
                 window.scrollTo(0, this.yScrollStack.pop());
             }
             else
                 window.scrollTo(0, 0);
         }
      });

    this._router = this.router.events.filter(event => event instanceof NavigationEnd).subscribe((event: NavigationEnd) => {
           elemMainPanel.scrollTop = 0;
           elemSidebar.scrollTop = 0;
      });
      
    const html = document.getElementsByTagName('html')[0];
    if (window.matchMedia(`(min-width: 960px)`).matches && !this.isMac()) {
          let ps = new PerfectScrollbar(elemMainPanel);
          ps = new PerfectScrollbar(elemSidebar);
          html.classList.add('perfect-scrollbar-on');
      }
      else {
          html.classList.add('perfect-scrollbar-off');
      }

    this._router = this.router.events.filter(event => event instanceof NavigationEnd).subscribe((event: NavigationEnd) => {
        this.header.sidebarClose();
      });

    
  }

  ngAfterViewInit() {
      this.runOnRouteChange();
  }

  public isMap() {
    
      if (this.location.prepareExternalUrl(this.location.path()) === '/maps/fullscreen') {
          return true;
      } else {
          return false;
      }
  }

  runOnRouteChange(): void {
    if (window.matchMedia(`(min-width: 960px)`).matches && !this.isMac()) {
      const elemSidebar = <HTMLElement>document.querySelector('.sidebar .sidebar-wrapper');
      const elemMainPanel = <HTMLElement>document.querySelector('.main-panel');
      let ps = new PerfectScrollbar(elemMainPanel);
      ps = new PerfectScrollbar(elemSidebar);
      ps.update();
    }    
  }

  isMac(): boolean {
      let bool = false;
      if (navigator.platform.toUpperCase().indexOf('MAC') >= 0 || navigator.platform.toUpperCase().indexOf('IPAD') >= 0) {
          bool = true;
      }
      return bool;
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
