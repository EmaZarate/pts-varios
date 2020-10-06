import { Component, OnInit, ViewChildren, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { RolesService } from '../../../core/services/roles.service';


import * as PERMISSIONS from '../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

declare const $: any;

declare interface DataTable {
  headerRow: string[];
}

@Component({
  selector: 'app-read-roles',
  templateUrl: './read-roles.component.html',
  styleUrls: ['./read-roles.component.css']
})
export class ReadRolesComponent implements OnInit, OnDestroy {

  public dataTable: DataTable;

  initializedDatatable = false;

  canSwitch;
  canEdit;
  canAdd;

  @ViewChildren('roles-datatable') rolesDatatable;

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  public roles = [];
  constructor(
    private _toastrManager: ToastrManager,
    private _rolesService: RolesService,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.setPermissions();
    this.loadRoles();
  }

  ngAfterViewInit(){
    this.rolesDatatable.changes
    .takeUntil(this.ngUnsubscribe)
    .subscribe(() => 
    {
      // if(!this.initializedDatatable){

        this.initializeDatatable();
        this.initializedDatatable = true;
        $('#roles-datatable_info').attr("style","white-space:unset!important");
      // }
    });
  }

  loadRoles(){
    this.blockUI.start();
    this._rolesService.getAll()
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res: any) => {
      this.dataTable = {
        headerRow: ['ID','Nombre','Activo','Acciones']
      }
      this.roles = res;
      this.blockUI.stop();
    });
  }

  initializeDatatable(){

    let table = $('#roles-datatable').DataTable({
      "bDestroy": true,
      "pagingType": "full_numbers",
      "pageLength": 5,
      responsive:true,
      language: {
        "sProcessing":     "Procesando...",
        "sLengthMenu":     "Mostrar _MENU_ registros",
        "sZeroRecords":    "No se encontraron resultados",
        "sEmptyTable":     "Ningún dato disponible en esta tabla",
        "sInfo":           "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
        "sInfoEmpty":      "Mostrando registros del 0 al 0 de un total de 0 registros",
        "sInfoFiltered":   "(filtrado de un total de _MAX_ registros)",
        "sInfoPostFix":    "",
        "sSearch":         "Buscar:",
        "sUrl":            "",
        "sInfoThousands":  ",",
        "sLoadingRecords": "Cargando...",
        "oPaginate": {
          "sFirst":    "<span><i class='material-icons'>skip_previous</i></span>",
          "sLast":     "<span><i class='material-icons'>skip_next</i></span>",
          "sNext":     "<span><i class='material-icons'>navigate_next</i></span>",
          "sPrevious": "<span><i class='material-icons'>navigate_before</i></span>"
        },
        "oAria": {
          "sSortAscending":  ": Activar para ordenar la columna de manera ascendente",
          "sSortDescending": ": Activar para ordenar la columna de manera descendente"
        }
      },

    });
    var column = table.column(0);

    $(column.footer());

    this.createNewButton("#roles-datatable");
  }

  createNewButton(id: string) {
    setTimeout(() => {
        $(id + "_length").parent().remove();
        $(".dataTables_filter").css('float', 'left');
        let divButton = document.createElement("div");
        $(divButton).addClass("col-sm-6").append($("#btnNewRole"));
        $(".dataTables_filter").parent().parent().append(divButton)
      }, 150)
  }

  updateRoleActive(role) {
    this.blockUI.start();
    if(!role.active){
      if (role.basic) {
        this._rolesService.checkIfExistsBasic()
        .takeUntil(this.ngUnsubscribe)
        .subscribe(res => {
          if (!res) {
            this.submitUpdateRoleActiva(role)
          }
          else{
            this._toastrManager.errorToastr('Ya exite un rol básico activo', 'Error');
            this.blockUI.stop();
          }
        })
      }
      else{
        this.submitUpdateRoleActiva(role)
      }
    }
    else{
      this.submitUpdateRoleActiva(role)
    }
  }

  submitUpdateRoleActiva(role){
    let claims = (role.roleClaims as Array<any>).map((el) => el.claimValue);
    let updateRole = { name: role.name, active: !role.active, basic: role.basic, claims: claims };
    this._rolesService.updateRole(updateRole, role.id)
    .takeUntil(this.ngUnsubscribe)  
    .subscribe((res) => {
        if ($.fn.DataTable.isDataTable("#roles-datatable")) {
          $('#roles-datatable').DataTable().clear().destroy();
        }
        this.loadRoles();
        this._toastrManager.successToastr('El rol se ha actualizado correctamente', 'Éxito!');
      });
  }

  setPermissions(){
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
    this.canSwitch = this._canSwitch();
  }
  
  private _canSwitch(){
    return this._authService.hasClaim(PERMISSIONS.ROLE.DEACTIVATE_ROLE) && this._authService.hasClaim(PERMISSIONS.ROLE.ACTIVATE_ROLE)
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.ROLE.EDIT_ROLE);
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.ROLE.ADD_ROLE);
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
