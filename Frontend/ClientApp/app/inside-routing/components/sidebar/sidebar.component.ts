import { Component, OnInit, AfterViewInit } from '@angular/core';
import PerfectScrollbar from 'perfect-scrollbar';
import { LoginService } from '../../../core/services/login.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { UsersService } from '../../../core/services/users.service';

import * as PERMISSIONS from '../../../core/permissions/index';
import { TASK } from '../../../core/permissions/quality/task';

declare const $: any;

export interface RouteInfo {
  path?: string;
  title?: string;
  type?: string;
  icontype?: string;
  collapse?: string;
  expectedClaim?: string;
  children?: ChildrenItems[];
}

export interface ChildrenItems {
  path: string;
  title: string;
  ab?: string;
  type?: string;
  expectedClaim?: string;
  actions?: ActionChildrenItems[]
}

export interface ActionChildrenItems {
  actionPath: string;
  actionTitle: string;
}


export const ROUTES: RouteInfo[] = [
  {
    path: '/home',
    title: 'Home',
    type: 'link',
    icontype: 'home'
  },
  {
    path: '/core',
    title: 'Administración',
    type: 'sub',
    icontype: 'apps',
    collapse: 'administracion',
    children: [
      { path: 'role', title: 'Roles', ab: 'RO', expectedClaim: PERMISSIONS.ROLE.ADD_ROLE },
      { path: 'claims', title: 'Permisos', ab: 'PE', expectedClaim: PERMISSIONS.ROLE.ADD_ROLE },
      { path: 'company', title: 'Configuración de Empresa', ab: 'CE', expectedClaim: PERMISSIONS.COMPANY.ADD_COMPANY },
      { path: 'plants', title: 'Plantas', ab: 'PL', expectedClaim: PERMISSIONS.PLANT.ADD_PLANT },
      { path: 'sectors', title: 'Sectores', ab: 'SE', expectedClaim: PERMISSIONS.SECTOR.ADD_SECTOR },
      { path: 'jobs', title: 'Puestos', ab: 'PU', expectedClaim: PERMISSIONS.JOB.ADD_JOB },
      { path: 'associate/tree', title: 'Configuración de plantas', ab: 'CP', expectedClaim: PERMISSIONS.ADMINISTRATION.CONFIGURE_PLANTS },
      { path: 'users', title: 'Usuarios', ab: 'US', expectedClaim: PERMISSIONS.USER.ADD_USER }
     
    ]
  },
  {
    path: '/quality/finding',
    title: 'Hallazgos',
    type: 'sub',
    icontype: 'search',
    collapse: 'hallazgos',
    children: [
      { path: 'list', title: 'Mis hallazgos', ab: 'MH', expectedClaim: PERMISSIONS.FINDING.READ_SECTOR },
      { path: 'config/parametrization-criteria', title: 'Criterios de parametrización', ab: 'CP', expectedClaim: PERMISSIONS.FINDING_PARAMETRIZATION_CRITERIA.ADD },
      { path: 'config/types', title: 'Tipos de hallazgo', ab: 'TP', expectedClaim: PERMISSIONS.FINDING_TYPES.ADD },
      { path: 'config/states', title: 'Estados de hallazgo', ab: 'EH', expectedClaim: PERMISSIONS.FINDING_STATES.ADD, actions: [{ actionPath: 'new', actionTitle: 'Nuevo' }, { actionPath: 'edit', actionTitle: 'Edicion' }] }      
    ]
  },
  {
    path: '/quality/audits',
    title: 'Auditorías',
    type: 'sub',
    icontype: 'group',
    collapse: 'auditorias',
    children: [
      { path: 'list', title: 'Mis auditorías', ab: 'MA', expectedClaim: PERMISSIONS.AUDIT.READAUDIT},
      { path: 'calendar', title: 'Calendario de Auditorías', ab: 'CA', expectedClaim: PERMISSIONS.AUDIT.READ_AUDIT_CALENDAR },
      { path: 'schedule', title: 'Programar auditoría', ab: 'PA', expectedClaim: PERMISSIONS.AUDIT.SCHEDULE },
      { path: 'config/standard', title: 'Normas', ab: 'N', expectedClaim: PERMISSIONS.STANDARD.ADD, actions: [{ actionPath: 'new', actionTitle: 'Nuevo' }, { actionPath: 'edit', actionTitle: 'Edicion' }] },
      { path: 'config/types', title: 'Tipos de auditoría', ab: 'TP', expectedClaim: PERMISSIONS.AUDITTYPES.ADD },
      { path: 'config/state', title: 'Estados de auditoría', ab: 'EA', expectedClaim: PERMISSIONS.AUDIT_STATE.ADD, actions: [{ actionPath: 'new', actionTitle: 'Nuevo' }, { actionPath: 'edit', actionTitle: 'Edicion' }] },
      { path: 'config/aspect-states', title: 'Estados de Aspectos', ab: 'EA', expectedClaim: PERMISSIONS.AUDIT_STATE.ADD }  
    ]
  },
  {
    path: '/quality/corrective-actions',
    title: 'Acciones Correctivas',
    type: 'sub',
    icontype: 'check_circle',
    collapse: 'acciones-correctivas',
    children: [
      { path: 'list', title: 'Mis acciones correctivas', ab: 'MA', expectedClaim: PERMISSIONS.CORRECTIVE_ACTION.READ },
      { path: 'schedule', title: 'Registrar acción correctiva', ab: 'RA', expectedClaim: PERMISSIONS.CORRECTIVE_ACTION.SCHEDULE },
     
      { path: 'config/states', title: 'Estados Acción Correctiva', ab: 'EA',expectedClaim: PERMISSIONS.CORRECTIVE_ACTION_STATE.READ, actions: [{ actionPath: 'new', actionTitle: 'Nuevo' }, { actionPath: 'edit', actionTitle: 'Edicion' }] },
      { path: 'config/fish-bone', title: 'Categorías Ishikawa', ab: 'CI', expectedClaim: PERMISSIONS.FISHBONE_CATEGORY.READ},
      { path: 'config/parametrization', title: 'Parametrizaciones', ab: 'PA', expectedClaim: PERMISSIONS.CORRECTIVE_ACTION_PARAMETRIZATION.READ_CORRECTIVE_ACTION_PARAMETRIZATION},
    ]
  },
  {
    path: '/quality/tasks',
    title: 'Tareas',
    type: 'sub',
    icontype: 'assignment',
    collapse: 'mis-tareas',
    children: [
      { path: 'list', title: 'Mis tareas', ab: 'MT', expectedClaim: PERMISSIONS.TASK.READ },
      { path: 'config/task-states', title: 'Estados de Tarea', ab: 'ET', expectedClaim: PERMISSIONS.TASK_STATE.EDIT }
    ]
  }
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})

export class SidebarComponent implements OnInit {
  public menuItems: any[];
  public userLogged: any;
  public currentStatusNavbar: string;
  user;

  menuClaims = {};

  constructor(private loginService: LoginService,
              private authService: AuthService,
              private router: Router,
              private _userService: UsersService) {
      this.setClaims();
      console.log(this.menuClaims);
  }

  isMobileMenu() {

    if ($(window).width() > 991) {
      return false;
    }
    return true;
  };

  ngOnInit() {
    this._userService.user$.subscribe(user => { this.user = user });
    this.userLogged = this.authService.getUserLogged()
    console.log(this.userLogged);
    this.menuItems = ROUTES.filter(menuItem => menuItem);
  }

  ngAfterViewInit() {
    this.updateNavbarCollapse();
  }

  updatePS(): void {
    if (window.matchMedia(`(min-width: 960px)`).matches && !this.isMac()) {
      const elemSidebar = <HTMLElement> document.querySelector('.sidebar .sidebar-wrapper');
      let ps = new PerfectScrollbar(elemSidebar, { wheelSpeed: 2, suppressScrollX: true });
    }
  }

  isMac(): boolean {

    let bool = false;
    if (navigator.platform.toUpperCase().indexOf('MAC') >= 0 || navigator.platform.toUpperCase().indexOf('IPAD') >= 0) {
      bool = true;
    }
    return bool;
  }


  logout() {
    this.loginService.logout();
    this.router.navigate(['/']);
  }

  updateNavbarCollapse() {
    var item = ROUTES.filter(x => this.router.url.includes(x.path));
      if ((item.length = 0) && item[0].collapse) document.getElementById(item[0].collapse).className = 'collapse show';
  }

  hasAccess(expectedClaim) {

    if (!expectedClaim) return true;
    return this.authService.hasClaim(expectedClaim);
  }

  hasOneAccess(children: ChildrenItems[]) {
    if (!children) return true;
    let hasAccess = false;
    children.forEach(item => {
      if (this.hasAccess(item.expectedClaim)) {
        hasAccess = true;
      }
    })

    return hasAccess;
  }

  setClaims(){
    ROUTES.forEach(rinfo => {
      if (rinfo.children){
        this.menuClaims[rinfo.title] = this.hasOneAccess(rinfo.children);
        rinfo.children.forEach(route => {
          this.menuClaims[route.title] = this.hasAccess(route.expectedClaim);
        })
      }
    })
  }

}
