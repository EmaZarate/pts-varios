import { Component, OnInit, Renderer, ElementRef, ViewChild } from '@angular/core';
import { LoginService } from '../../../core/services/login.service';
import { ROUTES } from '../sidebar/sidebar.component';
import { Subscription } from 'rxjs/Subscription';
import { Router, NavigationEnd, ActivatedRoute, NavigationStart } from '@angular/router';
import { Location } from '@angular/common';
import { switchMap } from 'rxjs/operators';
// import { AuthService } from 'ClientApp/app/core/services/auth.service';
// import { FindingsService } from 'ClientApp/app/core/services/findings.service';

const misc: any = {
  navbar_menu_visible: 0,
  active_collapse: true,
  disabled_collapse_init: 0,
};

declare var $: any;

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  private listTitles: any[];
  location: Location;
  mobile_menu_visible: any = 0;
  private nativeElement: Node;
  private toggleButton: any;

  private sidebarVisible: boolean;
  tooltipToggleText: String;

  private _router: Subscription;

  //userLoged
  //chats: Array<any> = new Array<any>();
  //message
  //first= true;
  @ViewChild('app-header') button: any;

  constructor(location: Location,
    private renderer: Renderer,
    private element: ElementRef,
    private loginService: LoginService,
    //private authService: AuthService,
    //private _findingService: FindingsService,
    private router: Router,
    private route: ActivatedRoute) {


    this.location = location;
    this.nativeElement = element.nativeElement;
    this.sidebarVisible = false;
    this.tooltipToggleText = "Minimizar menú";
  }

  minimizeSidebar() {
    const body = document.getElementsByTagName('body')[0];

    if (misc.sidebar_mini_active === true) {
      body.classList.remove('sidebar-mini');
      misc.sidebar_mini_active = false;
      this.tooltipToggleText = "Minimizar menú"
    } else {

      this.tooltipToggleText = "Maximizar menú";
      setTimeout(function () {
        body.classList.add('sidebar-mini');

        misc.sidebar_mini_active = true;
      }, 300);
    }



    // we simulate the window Resize so the charts will get updated in realtime.
    const simulateWindowResize = setInterval(function () {
      window.dispatchEvent(new Event('resize'));
    }, 180);

    // we stop the simulation of Window Resize after the animations are completed
    setTimeout(function () {
      clearInterval(simulateWindowResize);
    }, 1000);
  }

  ngOnInit() {
    //this.userLoged = this.authService.getUserLogged();
    //console.log(this.userLoged)
    //this._findingService.startConnection();
    //this._findingService.addTransferChatDataListener();
    // this._findingService.chat$.subscribe(chat =>{
    //   debugger
    //   if(this.first != true){
    //     this.chats.push(chat)
    //   }
    //   this.first = false
    // })

    this.listTitles = ROUTES.filter(listTitle => listTitle);

    const navbar: HTMLElement = this.element.nativeElement;
    const body = document.getElementsByTagName('body')[0];
    this.toggleButton = navbar.getElementsByClassName('navbar-toggler')[0];
    if (body.classList.contains('sidebar-mini')) {
      misc.sidebar_mini_active = true;
    }
    if (body.classList.contains('hide-sidebar')) {
      misc.hide_sidebar_active = true;
    }
    this._router = this.router.events.filter(event => event instanceof NavigationEnd).subscribe((event: NavigationEnd) => {
      this.sidebarClose();

      const $layer = document.getElementsByClassName('close-layer')[0];
      if ($layer) {
        $layer.remove();
      }
    });

  }

  // send(){
  //   this._findingService.send(this.userLoged.sub +': '+ this.message);
  // }

  onResize(event) {
    if ($(window).width() > 991) {
      return false;
    }
    return true;
  }

  sidebarOpen() {
    var $toggle = document.getElementsByClassName('navbar-toggler')[0];
    const toggleButton = this.toggleButton;
    const body = document.getElementsByTagName('body')[0];
    setTimeout(function () {
      toggleButton.classList.add('toggled');
    }, 500);
    body.classList.add('nav-open');
    setTimeout(function () {
      $toggle.classList.add('toggled');
    }, 430);

    var $layer = document.createElement('div');
    $layer.setAttribute('class', 'close-layer');


    if (body.querySelectorAll('.main-panel')) {
      document.getElementsByClassName('main-panel')[0].appendChild($layer);
    } else if (body.classList.contains('off-canvas-sidebar')) {
      document.getElementsByClassName('wrapper-full-page')[0].appendChild($layer);
    }

    setTimeout(function () {
      $layer.classList.add('visible');
    }, 100);

    $layer.onclick = function () { //asign a function
      body.classList.remove('nav-open');
      this.mobile_menu_visible = 0;
      this.sidebarVisible = false;

      $layer.classList.remove('visible');
      setTimeout(function () {
        $layer.remove();
        $toggle.classList.remove('toggled');
      }, 400);
    }.bind(this);

    body.classList.add('nav-open');
    this.mobile_menu_visible = 1;
    this.sidebarVisible = true;
  }

  sidebarClose() {
    var $toggle = document.getElementsByClassName('navbar-toggler')[0];
    const body = document.getElementsByTagName('body')[0];
    this.toggleButton.classList.remove('toggled');
    var $layer = document.createElement('div');
    $layer.setAttribute('class', 'close-layer');

    this.sidebarVisible = false;
    body.classList.remove('nav-open');
    // $('html').removeClass('nav-open');
    body.classList.remove('nav-open');
    if ($layer) {
      $layer.remove();
    }

    setTimeout(function () {
      $toggle.classList.remove('toggled');
    }, 400);

    this.mobile_menu_visible = 0;
  }

  sidebarToggle() {
    if (this.sidebarVisible === false) {
      this.sidebarOpen();
    } else {
      this.sidebarClose();
    }
  }

  getTitle() {


    let titlee: any = this.location.prepareExternalUrl(this.location.path());
    for (let i = 0; i < this.listTitles.length; i++) {
      if (this.listTitles[i].type === "link" && this.listTitles[i].path === titlee) {
        return this.listTitles[i].title;
      } else if (this.listTitles[i].type === "sub" || (this.listTitles[i].type === "link" && this.listTitles[i].children)) {
        for (let j = 0; j < this.listTitles[i].children.length; j++) {

          let subtitle = this.listTitles[i].path + '/' + this.listTitles[i].children[j].path;
          if (subtitle === titlee) {
            return this.listTitles[i].children[j].title;
          }

          if (this.listTitles[i].children[j].actions) {
            for (let h = 0; h < this.listTitles[i].children[j].actions.length; h++) {
              let actionTitle = this.listTitles[i].path + '/' + this.listTitles[i].children[j].path + '/' + this.listTitles[i].children[j].actions[h].actionPath;
              if (actionTitle === titlee) {
                return this.listTitles[i].children[j].actions[h].actionTitle;
              }
            }
          }
        }
      }
    }
    return 'Home';
  }

  getPath() {
    return this.location.prepareExternalUrl(this.location.path());
  }

  logout() {
    this.loginService.logout();
    this.router.navigate(['/login']);
  }

}
