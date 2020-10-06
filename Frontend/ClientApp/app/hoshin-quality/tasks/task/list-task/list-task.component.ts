import { Component, OnInit, ViewChildren, AfterViewInit, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Task } from 'ClientApp/app/hoshin-quality/corrective-actions/models/Task';
import { TaskConfigService } from '../../../corrective-actions/task.service';
import { TaskStateCode } from '../../../corrective-actions/models/TaskStateCode';
import * as PERMISSIONS from '../../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';
import { Subject } from 'rxjs';

declare var $: any;

@Component({
  selector: 'app-list-task',
  templateUrl: './list-task.component.html',
  styleUrls: ['./list-task.component.css']
})
export class ListTaskComponent implements OnInit, AfterViewInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  @ViewChildren('tasks-datatable') tasksDatatable;
  dataSource: Task[] = new Array();
  displayedColumns: string[] = ['Id', 'Description', 'User', 'Date', 'buttons'];
  initializedDatatable = false;
  taskStateCode = TaskStateCode;
  canExtendDueDate;
  canRequestDueDate; 
  canEdit;
  canDeleteAC;
  userLogged;
  cols = [
    { field: 'taskID', header: 'ID' },
    { field: 'description', header: 'Descripción' },
    { field: 'taskState.name', header: 'Estado' },
    { field: 'responsibleUser.name', header: 'Responsable' },
    { field: 'implementationPlannedDate', header: 'Fecha vencimiento' },
    { field: '', header: 'Origen' },
    { field: 'entityID', header: 'ID Origen'}  ]
  constructor(
    private _taskService: TaskConfigService,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.userLogged = this._authService.getUserLogged();
    this.getTasks();
    this.setPermissions();
  }

  ngAfterViewInit() {
    this.tasksDatatable.changes
    .takeUntil(this.ngUnsubscribe)
    .subscribe((change) => {
      if (!this.initializedDatatable) {
        this.initializeDatatable();
        this.initializedDatatable = true;
        $('#tasks-datatable_info').attr('style', 'white-space:unset!important');
      }
    });

  }

  getTasks() {
    this.blockUI.start();
    this._taskService.GetAllTaks() 
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res) => {
      console.log(res);
      this.dataSource = res.map(task => {
        return {
          ...task,
          acApporverOrResponsibleSGCCanEdit : this.AcApporverOrResponsibleSGCCanEdit(task)
        }
      });
      if (this.dataSource.length === 0) {
        this.initializeDatatable();
        this.initializedDatatable = true;
      }
      this.blockUI.stop();
      setTimeout( this.setClickPaginator, 1500);
    });
  }
  setClickPaginator = () => {
    $(".ui-paginator-element").click(() => {
      this.setClickPaginator()
      $(".main-panel.ps.ps--active-y").scrollTop(0);
    })
  }

  AcApporverOrResponsibleSGCCanEdit(task) {
    if(!this.canDeleteAC) return true
    if(task.responsibleUserID == this.userLogged.id) return true
    return false
 }



  setPermissions() {
    this.canExtendDueDate = this._canExtendDueDate();
    this.canRequestDueDate = this._canRequestDueDate();
    this.canEdit = this._canEdit();
    this.canDeleteAC = this._canDeleteAC();
  }

  private _canExtendDueDate() {
    return this._authService.hasClaim(PERMISSIONS.TASK.EXTEND_DUEDATE);
  }

  private _canRequestDueDate() {
    return this._authService.hasClaim(PERMISSIONS.TASK.REQUEST_DUE_DATE_EXTEND)
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.TASK.EDIT)
  }

  private _canDeleteAC() {
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION.DELETE);
  }

  initializeDatatable() {

    let table = $('#tasks-datatable').DataTable({
      bDestroy: true,
      pagingType: 'full_numbers',
      pageLength: 15,
      order: [[0, 'desc']],
      responsive: true,
      language: {
        sProcessing: 'Procesando...',
        sLengthMenu: 'Mostrar _MENU_ registros',
        sZeroRecords: 'No se encontraron resultados',
        sEmptyTable: 'Ningún dato disponible en esta tabla',
        sInfo: 'Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros',
        sInfoEmpty: 'Mostrando registros del 0 al 0 de un total de 0 registros',
        sInfoFiltered: '(filtrado de un total de _MAX_ registros)',
        sInfoPostFix: '',
        sSearch: 'Buscar:',
        sUrl: '',
        sInfoThousands: ',',
        sLoadingRecords: 'Cargando...',
        oPaginate: {
          sFirst: '<span><i class=\'material-icons\'>skip_previous</i></span>',
          sLast: '<span><i class=\'material-icons\'>skip_next</i></span>',
          sNext: '<span><i class=\'material-icons\'>navigate_next</i></span>',
          sPrevious: '<span><i class=\'material-icons\'>navigate_before</i></span>'
        },
        oAria: {
          sSortAscending: ': Activar para ordenar la columna de manera ascendente',
          sSortDescending: ': Activar para ordenar la columna de manera descendente'
        }
      },
      columnDefs: [
        { orderable: true, targets: 3 },
        { type: 'alt-string', targets: 2 }
      ]
    });

    var column = table.column(0);
    $(column.footer());
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
