import { Component, OnInit, AfterViewInit, ViewChildren, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

import { NgBlockUI, BlockUI } from 'ng-block-ui';

import { UsersService } from '../../../core/services/users.service';

import * as PERMISSIONS from '../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

declare const $: any;

declare interface DataTable {
  headerRow: string[];
}

@Component({
  selector: 'app-read-user',
  templateUrl: './read-user.component.html',
  styleUrls: ['./read-user.component.css']
})
export class ReadUserComponent implements OnInit, AfterViewInit, OnDestroy {

  public dataTable: DataTable;

  @ViewChildren('users-datatable') userDatatable;

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  constructor(
    private _usersService: UsersService,
    private _authService: AuthService
  ) { }

  users = [];    

  canEdit;
  canAdd;

  ngOnInit() {
    this.blockUI.start();
    this.setPermissions();
    this._usersService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any[]) => {
        
        console.log(res);
        this.users = res;
        this.dataTable = {
          headerRow: ['#','Usuario','Nombre','Apellido','Planta', 'Sector', 'Puesto', 'Acciones']
        }
        this.blockUI.stop();
      })
  }

  ngAfterViewInit(){
    this.userDatatable.changes
    .takeUntil(this.ngUnsubscribe)
    .subscribe(() => {
      this.initializeDatatable()
      $('#users-datatable_info').attr("style","white-space:unset!important");
    });

  }

  initializeDatatable(){
    var table = $('#users-datatable').DataTable({
      "pagingType": "full_numbers",
      "pageLength": 5,
      responsive:true,
      order: [[4, "asc"], [3, "asc"]],
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
      }

    });

    //Logic for open - close details
    $('#users-datatable').on('click', 'td.view-detail', function(){
      var tr = $(this).closest('tr');
      var materialIcon = $(this).find('button span i.material-icons');
      var row = table.row(tr);

      if(row.child.isShown()){
        //This row uis already open - close it
        row.child.hide();
        materialIcon.html("add_circle");
      }
      else{
        row.child(format(row.data())).show();
        materialIcon.html("remove_circle");
      }
    });

    function format(d){
      let roles = d[0].split(',').join(', ');
      roles = roles != "" ? roles : "No hay roles asignados";

      //This HTML render in the detail view
      return '<h6><b>Roles</b></h6><span>'+roles+'</span>'
    }

    this.createNewButton("#users-datatable");
  }

  createNewButton(id: string) {
    if(!this.canAdd) return;
    setTimeout(() => {
        $(id + "_length").parent().remove();
        $(".dataTables_filter").css('float', 'left');
        let divButton = document.createElement("div");
        $(divButton).addClass("col-sm-6").append($("#btnNewUser"));
        $(".dataTables_filter").parent().parent().append(divButton)
      }, 150)
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  setPermissions(){
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
  }


  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.USER.EDIT_USER);
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.USER.ADD_USER);
  }
}