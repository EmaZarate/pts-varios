
import { Component, OnInit, ViewChildren, AfterViewInit, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { CorrectiveActionService } from '../../corrective-action.service';
import { CorrectiveActionStateCode } from '../../models/CorrectiveActionStateCode';
import { CompanyService } from '../../../../core/services/company.service';
import * as moment from 'moment';
import Swal from 'sweetalert2/dist/sweetalert2';
import { TaskConfigService } from '../../task.service';
import { Task } from '../../models/Task';
import { CorrectiveAction } from '../../models/CorrectiveAction';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { AuthService } from 'ClientApp/app/core/services/auth.service';
import * as PERMISSIONS from '../../../../core/permissions/index';
import { Subject } from 'rxjs';

declare var $: any;

@Component({
  selector: 'app-list-corrective-actions',
  templateUrl: './list-corrective-actions.component.html',
  styleUrls: ['./list-corrective-actions.component.css'],
  animations: [
    trigger('rowExpansionTrigger', [
        state('void', style({
            transform: 'translateX(-10%)',
            opacity: 0
        })),
        state('active', style({
            transform: 'translateX(0)',
            opacity: 1
        })),
        transition('* <=> *', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)'))
    ])
]
})

export class ListCorrectiveActionsComponent implements OnInit, OnDestroy {


  initializedDatatable = false;
  CorrectiveActionStateCode = CorrectiveActionStateCode;
  correctiveActions: Array<CorrectiveAction> = new Array<CorrectiveAction>();
  company: any;
  srcLogo: any;
  fileName;
  tasks: Task[];
  userIsReferrent2;
  userIsReferrent;
  userLogged;

  canReassign;
  canReassignDirectly;
  canSchedule
  canPlanning
  canDelete
  canExport
  canEvaluate
  canRequestPlanningDueDateExtention
  canRequestEvaluateDueDateExtention
  canExtendPlanningDueDate
  canExtendEvaluateDueDate

  cols = [
    { field: 'correctiveActionID', header: 'ID' },
    { field: 'description', header: 'Descripción' },
    { field: 'correctiveActionStateName', header: 'Estado' },
    { field: 'sectorPlantLocationName', header: 'Sector-Planta ubicación' },
    { field: 'sectorPlantTreatmentName', header: 'Sector-Planta tratamiento' },
    { field: 'responsibleUserFullName', header: 'Responsable' },
    { field: 'reviewerUserFullName', header: 'Evaluador' }
  ];

  @BlockUI() blockUI: NgBlockUI;
  
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  constructor(
    private _correctiveActionService: CorrectiveActionService,
    private _companyService:CompanyService,
    private _taskService: TaskConfigService,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.setPermissions();
    this.userLogged = this._authService.getUserLogged();
    console.log('User logged');
    console.log(this.userLogged);
    this._companyService.getAll()
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res: any) => {
      if(res.length > 0) {
        this.company = res[0];
        this.srcLogo = this.company.logo;
      }
    });
    this.getTasks();
  }

  getCorrectiveActions() {
    this._correctiveActionService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.correctiveActions = res;
        this.correctiveActions.forEach(correctiveAction => {
          let tasks = this.tasks.filter(x => x.entityID == correctiveAction.correctiveActionID);
          correctiveAction.tasks = tasks;
          correctiveAction.editAsResponsibleRefferrent = this.isResponsibleReferrent(correctiveAction);
          correctiveAction.editAsReviewerRefferrent = this.isReviewerReferrent(correctiveAction);
          correctiveAction.isResponsibleUser = correctiveAction.responsibleUserID == this.userLogged.id;
          correctiveAction.isReviewerUser = correctiveAction.reviewerUserID == this.userLogged.id;
        });
        this.blockUI.stop();
        setTimeout( this.setClickPaginator, 1500);
      })
  }

  setClickPaginator = () => {
    $(".ui-paginator-element").click(() => {
      this.setClickPaginator()
      $(".main-panel.ps.ps--active-y").scrollTop(0);
    })
  }

  isResponsibleReferrent(el){
    this.userIsReferrent2 = this.userLogged.job == el.sectorPlantTreamtentReferring2JobId;
    if(this.userIsReferrent2){
      const isACAssignedToReferrent = el.responsibleUserJob == el.sectorPlantTreamtentReferringJobId
      return isACAssignedToReferrent;
    }
    this.userIsReferrent = this.userLogged.job == el.sectorPlantTreamtentReferringJobId;
    if(this.userIsReferrent){
      const isACAssignedToReferrent = el.responsibleUserJob == el.sectorPlantTreamtentReferringJobId
      return isACAssignedToReferrent;
    }
    return this.userLogged.job == el.responsibleUserJob;
  }

  isReviewerReferrent(el){
    this.userIsReferrent2 = this.userLogged.job == el.sectorPlantTreamtentReferring2JobId;
    if(this.userIsReferrent2){
      const isFindingAssignedToReferrent = el.reviewerUserJob == el.sectorPlantTreamtentReferringJobId
      return isFindingAssignedToReferrent;
    }
    this.userIsReferrent = this.userLogged.job == el.sectorPlantTreamtentReferringJobId;
    if(this.userIsReferrent){
      const isFindingAssignedToReferrent = el.reviewerUserJob == el.sectorPlantTreamtentReferringJobId
      return isFindingAssignedToReferrent;
    }
    return this.userLogged.job == el.reviewerUserJob;
  }


  setPermissions() {
    this.canSchedule = this._canSchedule();
    this.canPlanning = this._canPlanning();
    this.canDelete = this._canDelete();
    this.canExport = this._canExport();
    this.canReassign = this._canReassign();
    this.canReassignDirectly = this._canReassignDirectly();
    this.canEvaluate = this._canEvaluate();
    this.canRequestPlanningDueDateExtention = this._canRequestPlanningDueDateExtention();
    this.canRequestEvaluateDueDateExtention = this._canRequestEvaluateDueDateExtention();
    this.canExtendPlanningDueDate = this._canExtendPlanningDueDate();
    this.canExtendEvaluateDueDate = this._canExtendEvaluateDueDate();
  }

  private _canReassign() {
    return this._authService.hasClaim(PERMISSIONS.FINDING.REASSIGN)
  }

  private _canPlanning() {
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION.PLANNING);
  }

  private _canSchedule() {
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION.SCHEDULE)
  }

  private _canDelete() {
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION.DELETE);
  }

  private _canExport() {
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION.EXPORT)
  }

  private _canEvaluate() {
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION.EVALUATE)
  }

  private _canRequestPlanningDueDateExtention() {
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION.REQUEST_PLANNING_DUEDATE_EXTENTION)
  }

  private _canRequestEvaluateDueDateExtention() {
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION.REQUEST_EVALUATE_DUEDATE_EXTENTION);
  }

  private _canExtendPlanningDueDate() {
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION.EXTEND_PLANNING_DUEDATE);
  }

  private _canExtendEvaluateDueDate() {
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION.EXTEND_EVALUATE_DUEDATE);
  }
  private _canReassignDirectly() {
    return this._authService.hasClaim(PERMISSIONS.FINDING.REASSIGN_DIRECTLY);
  }


  getTasks() {
      this._taskService.GetAllTaksForAC()
    .takeUntil(this.ngUnsubscribe)
    .subscribe(res => {
      this.getCorrectiveActions();
      this.tasks = res;
    });
  }

  deleteCorrectiveAction(id){
    Swal.fire({
      text: '¿Desea borrar esta acción correctiva?',
      type: 'question',
      showCancelButton: true,
      confirmButtonText: 'Si',
      cancelButtonText: 'No ',
      focusCancel: true
    }).then((result) => {
      if (result.value) {
        this.blockUI.start();
        this._correctiveActionService.delete(id)
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          let correctiveAction = this.correctiveActions.find(x=>x.correctiveActionID = id)
          let index = this.correctiveActions.indexOf(correctiveAction);
          this.correctiveActions.splice(index, 1)
          this.blockUI.stop();
        });
      }
    });
  }

  exportPdf(correctiveActionId: number){
    var correctiveActions = this.correctiveActions as any
    let correctiveAction: any = correctiveActions.find(x => x.correctiveActionID == correctiveActionId) as any;
    let workGroups: string = "";
    let fileNames: string = ""; 
    correctiveAction.userCorrectiveActions.forEach(element => {
      if(element.users != null) {
        workGroups += element.users.fullName + "; "
      }
    });  

    correctiveAction.evidences.forEach(element => {
      fileNames += element.fileName + "; "
    });
    
    let findingPrintHTML = `<style type="text/css">
      <!--
        #page-wrap {
          height:98%;
          min-height:1050px;
          width:745px;
          border:1px solid #894aa4;
          padding: 10px;
          margin: 0 auto;
        }

        #tableLogo th,
        #tableLogo td {
          border: 1px solid #894aa4;
        }

        .centerText { text-align: center; }
        .t { border-top: 1px solid #894aa4; }

        #metaRight {
          margin-top: 3px;
          margin-bottom: 1px;
          width: 360px;
          float: right;
          border-collapse: collapse;
          border-radius: 5px;
          overflow: hidden;
          border-top: 1px solid transparent;
          border-left: 1px solid transparent;
          border-right: 1px solid transparent;
          border-bottom: 1px solid #894aa4;
        }
        
        #metaLeft {
          margin-top: 3px;
          margin-bottom: 1px;
          width: 360px;
          float: left;
          border-collapse: collapse;
          border-radius: 5px;
          overflow: hidden;
          border-top: 1px solid transparent;
          border-left: 1px solid transparent;
          border-right: 1px solid transparent;
          border-bottom: 1px solid #894aa4;
        }
      
        #metaCenter { 
          margin-top: 3px;
          margin-bottom: 1px;
          width: 723px;
          border-collapse: collapse;
          border-radius: 5px;
          overflow: hidden;
          border-top: 1px solid transparent;
          border-left: 1px solid transparent;
          border-right: 1px solid transparent;
          border-bottom: 1px solid #894aa4;
        }

        .items {
          display: inline-grid;
          width: 100%;
        }

        .labelBlack {
          margin:5px;
          color: black;
          font-weight: 500;
        }
        .marginLeft {
          margin-left:20px;
        }
      -->
    </style>
    <div id="page-wrap">
      <table style="width:100%;margin-bottom: 10px" id="tableLogo">
          <tr>
              <td rowspan="3" class="centerText"><img width="150px" height="100px"
                      src="../../../../../assets/img/hoshinlogo.png"></td>
              <td rowspan="3" class="centerText labelBlack"> Acción Correctiva ${ correctiveAction.correctiveActionID }<div>
                      <div class="t labelBlack">Estado: ${ correctiveAction.correctiveActionState.name }</div>
                  </div>
              </td>
              <td rowspan="3" class="centerText"><img width="180px" height="50px"
                      src="${this.srcLogo}"></td>
          </tr>
      </table>

      <table id="metaCenter" class="page-break">
        <tr class="bottom-line">
          <td class="table-header" style="width: 100px;">Descripción:</td>
          <td class="centerText table-data"> ${ correctiveAction.description } </td>
        </tr>  
      </table>
      <table id="metaLeft" class="page-break">
        <tr>
            <td class="table-header" style=" width: 140px;">Sector Ubicación:</td>
            <td class="centerText table-data">${ correctiveAction.sectorPlantLocation.sector != null ? correctiveAction.sectorPlantLocation.sector.name : '----'}</td>
        </tr>
      </table>
      <table id="metaRight" class="page-break">
        <tr>
            <td class="table-header" style="width: 155px;">Sector Tratamiento:</td>
            <td class="centerText table-data">${ correctiveAction.sectorPlantTreatment.sector != null ? correctiveAction.sectorPlantTreatment.sector.name : '----'}</td>
        </tr>
      </table>
      <table id="metaLeft" class="page-break">
        <tr>
          <td class="table-header" style="width: 110px;">Responsable:</td>
          <td class="centerText table-data">${ correctiveAction.responisbleUser.name } ${ correctiveAction.responisbleUser.surname }</td>
        </tr>
      </table>
      <table id="metaRight" class="page-break">
        <tr>
          <td class="table-header" style="width: 85px;">Evaluador:</td>
          <td class="centerText table-data">${ correctiveAction.reviewerUser != null ? correctiveAction.reviewerUser.name + ' ' + correctiveAction.reviewerUser.surname : '----' }</td>
        </tr>
      </table>
      <table id="metaCenter" class="page-break">
        <tr>
          <td class="table-header" style="width: 135px;">Grupo de trabajo:</td>
          <td class="centerText table-data"> ${ workGroups || '----' } </td>
        </tr>  
      </table>
      <table id="metaLeft" style="width: 300px;" class="page-break">
          <tr>
            <td class="table-header" style="width: 165px;">Hallazgo relacionado:</td>
            <td class="centerText table-data">${ correctiveAction.findingID != null ? correctiveAction.findingID : '----' }</td>
          </tr>
      </table>
      <table id="metaRight" style="width:421px;" class="page-break">
        <tr>
          <td class="table-header" style="width: 135px;">Tipo de Hallazgo:</td>
          <td class="centerText table-data">${ correctiveAction.finding != null ? correctiveAction.finding.findingType != null ? correctiveAction.finding.findingType.name : '----' : '' }</td>
        </tr>
      </table>
      <table id="metaCenter" class="page-break">
        <tr>
          <td class="table-header" style="width: 185px;">Descripción del Hallazgo:</td>
          <td class="centerText table-data">${ correctiveAction.finding != null ? correctiveAction.finding.description != null ? correctiveAction.finding.description : '----' : '----' }</td>
        </tr>
      </table>
      <table id="metaCenter" class="page-break">
        <tr>
          <td class="table-header" style="width: 90px;">Causa raíz:</td>
          <td class="centerText table-data">${ correctiveAction.rootReason != null ? correctiveAction.rootReason : '----' }</td>
        </tr>
      </table>
      <div style="margin-bottom: 10px;">
        <p class="labelBlack"><b>Análisis de causas</b></p>
        ${ this.prepareIshikawa(correctiveAction.correctiveActionFishbone) }        
      </div>
      <div style="margin-bottom: 10px;">
        <p class="labelBlack"><b>Plan de acción</b></p>
        ${ this.getTasksByCorrectiveActionId(correctiveAction.correctiveActionID) }
      </div>
      <table id="metaCenter" class="page-break">
        <tr>
          <td class="table-header" style="width: 165px;">Análisis de impacto:</td>
          <td class="centerText table-data">${ correctiveAction.impact != null ? correctiveAction.impact : '----' }</td>
        </tr>
      </table>
      <table id="metaLeft" class="page-break">
        <tr>
          <td class="table-header" style="width: 230px;">Fecha real de implementación:</td>
          <td class="centerText table-data">${ correctiveAction.effectiveDateImplementation != '0001-01-01T00:00:00' 
                                  ? moment(correctiveAction.effectiveDateImplementation, "YYYY/MM/DD").format("DD/MM/YYYY") 
                                  : '----' }</td>
        </tr>
      </table>
      <table id="metaRight" class="page-break">
        <tr>
          <td class="table-header" style="width: 190px;">Fecha real de evaluación:</td>
          <td class="centerText table-data">${ correctiveAction.dateTimeEfficiencyEvaluation != '0001-01-01T00:00:00'
                                  ? moment(correctiveAction.dateTimeEfficiencyEvaluation, "YYYY/MM/DD").format("DD/MM/YYYY") 
                                  : '----' }</td>
        </tr> 
      </table>
      <table id="metaCenter" class="page-break">
        <tr>
          <td class="table-header" style="width: 190px;">Resultado de Evaluación:</td>
          <td class="centerText table-data">${ (correctiveAction.correctiveActionState.code == CorrectiveActionStateCode.TRT 
                                    || correctiveAction.correctiveActionState.code == CorrectiveActionStateCode.PLN
                                    || correctiveAction.correctiveActionState.code == CorrectiveActionStateCode.VST )
                                    ? '----' 
                                    : correctiveAction.isEffective ? 'Eficaz': 'No Eficaz' }</td>
        </tr>
      </table>
      <table id="metaCenter" class="page-break">
        <tr>
          <td class="table-header" style="width: 210px;">Comentarios del Evaluador:</td>
          <td class="centerText table-data">${ correctiveAction.evaluationCommentary != null ?  correctiveAction.evaluationCommentary : '----' }</td>
        </tr>
      </table>
      <table id="metaCenter" class="page-break">
        <tr>
          <td class="table-header" style="width: 165px;">Evidencias de la AC:</td>
          <td class="centerText table-data">${ fileNames || '----' }</td>
        </tr> 
      </table>
    </div>`;

    this.fileName = `Acción Correctiva - ${moment().format("DD/MM/YYYY HH:mm")}`;

    return findingPrintHTML;
  }
  
  prepareIshikawa(fishBones) {
    let content = "";
    let title = "";
    for(let j = 0; j < fishBones.length; j++){
      switch(fishBones[j].fishboneID){
        case 1: title = 'Métodos/Procesos:';
        break;
        case 2: title = 'Materiales/Equipos:';
        break;
        case 3: title = 'Entorno:';
        break;
        case 4: title = 'Políticas:';
        break;
        case 5: title = 'Personas:';
        break;
        case 8: title = 'Capacidad de Servicio:';
        break;
      }
      if(this.hasValues(fishBones[j].causes)){
        content += 
        `<table id="metaCenter" class="page-break">
          <tr>
            <td class="table-header" style="width: 170px;">
              ${title}
            </td>
            <td class="centerText table-data">
            <ul style="text-align: left; align-items: middle;">`;
        
          for(let index = 0; index < fishBones[j].causes.length; index++){
            if(fishBones[j].causes[index].name != null){
              content += `<li>
              ${ fishBones[j].causes[index].name }
              </li>`
            }
            
          }
          content +=`</ul>
                  </td>
                  </tr>
                </table>`;
      }
    
      }
      

      
    return content;
  }
  hasValues(causes){
    let b = 0;
    for(let index = 0; index < causes.length; index ++ ){
      if(causes[index].name != null){
        b = 1;
      }
    }
    if(b == 1){
      return true;
    }else{
      return false;
    }
  }
  getTasksByCorrectiveActionId(id: number){
    let content = "";
    let tasks = this.tasks.filter(x => x.entityID == id);

    content = `<table id="metaCenter" class="page-break">
                  <thead>
                    <tr>
                      <th class="table-header centerText">
                        Descripción
                      </th>
                      <th class="table-header centerText">
                        Responsable
                      </th>
                      <th class="table-header centerText">
                        Fecha vencimiento
                      </th>
                    </tr>  
                  </thead>
                  <tbody>`;
    for (let index = 0; index < tasks.length; index++) {
      content += `<tr>
                    <td class="centerText table-line">
                        ${ tasks[index].description }
                    </td>
                    <td class="centerText table-line">
                        ${ tasks[index].firstname } ${ tasks[index].surname }
                    </td>
                    <td class="centerText table-line">
                        ${ moment(tasks[index].implementationPlannedDate).format("DD/MM/YYYY") }
                    </td>
                  </tr>
                  `;
    };
    content += `  </tbody>
                </table>`;
    return content;
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
