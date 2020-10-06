import Swal  from 'sweetalert2/dist/sweetalert2';
import { Component, OnInit, ViewChildren, OnDestroy } from '@angular/core';
import { AuditStateService } from '../../audit-state.service';
import { AuditService } from '../../audit.service';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';
import * as moment from 'moment';
import { CompanyService } from '../../../../core/services/company.service';
import { FindingsTypeService } from "../../../../core/services/findings-type.service";
import * as PERMISSIONS from "../../../../core/permissions/index";
import { AuthService } from 'ClientApp/app/core/services/auth.service';
import { AuditState } from '../../models/AuditState';

declare var $: any;

@Component({
  selector: 'app-list-audit',
  templateUrl: './list-audit.component.html',
  styleUrls: ['./list-audit.component.css']
})

export class ListAuditComponent implements OnInit, OnDestroy {

  private ngUnsubscribe: Subject<void> = new Subject<void>();


  initializedDatatable = false;

  popoverTitle = "Exportar informe";
  fileName;

  _allAuditState: any;
  _allFindingType: any;
  audits: any;

  _auditStatePendingApproveCode
  _auditStateScheduledCode
  _auditStateRejectedCode
  _auditPlannedCode
  _auditInitiatedCode
  _auditReportPendingApprovalCode
  _auditApprovedReportCode
  _auditReportRejectedCode
  srcLogo: any;
  @BlockUI() blockUI: NgBlockUI;

  canSchedule
  canPlanning
  canReSchedule
  canApporvePlanning
  canEmmitReport
  canApproveReport
  canDelete
  canExport
  canAddFindings

  cols = [
    { field: 'auditID', header: 'ID' },
    { field: 'auditTypeName', header: 'Tipo Auditoría' },
    { field: 'auditStandards', header: 'Normas' },
    { field: 'auditStateName', header: 'Estado' },
    { field: 'sectorPlantName', header: 'Sector-Planta' },
    { field: 'auditorName', header: 'Auditor' },
    { field: 'auditInitDate', header: 'Fecha Inicio' }
  ]

  constructor(private _auditStateService: AuditStateService,
    private _auditService: AuditService,
    private _companyService: CompanyService,
    private _findingTypeService: FindingsTypeService,
    private _authService: AuthService) { }

  ngOnInit() {

    this.blockUI.start();
    this.setPermissions();
    this.getAuditState();
    this.getFindingsType();

    this._companyService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        if (res.length > 0) {
          this.srcLogo = res[0].logo;
        }
      });
    this.getAudits();
  }

  getAudits() {
    this._auditService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.audits = res;
        console.log(this.audits)
        this.blockUI.stop();
        setTimeout(this.setClickPaginator, 1500);
      });
  }

  setClickPaginator = () => {
    $(".ui-paginator-element").click(() => {
      this.setClickPaginator()
      $(".main-panel.ps.ps--active-y").scrollTop(0);
    })
  }


  getFindingsType() {

    this._findingTypeService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this._allFindingType = res;
      });

  }

  getAuditState() {

    this._auditStateService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this._allAuditState = res;
        this._auditStateScheduledCode = this._allAuditState.find(x => x.code == "PRO");
        this._auditStatePendingApproveCode = this._allAuditState.find(x => x.code == "PPA");
        this._auditPlannedCode = this._allAuditState.find(x => x.code == "PLA");
        this._auditStateRejectedCode = this._allAuditState.find(x => x.code == "PRZ");
        this._auditInitiatedCode = this._allAuditState.find(x => x.code == "INI");
        this._auditReportPendingApprovalCode = this._allAuditState.find(x => x.code == "IPA");
        this._auditApprovedReportCode = this._allAuditState.find(x => x.code == "IAP");
        this._auditReportRejectedCode = this._allAuditState.find(x => x.code == "IRZ");
      });
  }



  clickExportPdf(auditId: number) {

    var audits = this.audits as any
    let audit: any = audits.find(x => x.auditID == auditId) as any;

    let auditPrintHTML = `<style type="text/css">
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
      margin-top: 1px;
      width: 50%;
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
      margin-top: 1px;
      width: 50%;
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
      width: 100%;
      margin-top: 1px;
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

    .marginBottom {
      margin-bottom: 20px;
    }

    .marginTop {
      margin-top: 10px;
    }
  
    .wrapText {
      word-break: break-all;
    }
    
    .tableFinding
    {
      border-collapse: collapse;
      border-radius: 5px;
      overflow: hidden;
      border: 1px solid transparent;
    }
    
    </style>

    <div id="page-wrap">

    <table style="width:100%;margin-bottom: 10px" id="tableLogo">
        <tr>
            <td rowspan="3" class="centerText"><img width="150px" height="100px" src="../../../../../assets/img/hoshinlogo.png"></td>

            <td rowspan="3" class="centerText labelBlack wrapText">Informe de Auditoría ${audit.auditID}
              <div class="labelBlack t">Tipo de auditoría:
              <div>${audit.auditType.name}</div>
              </div>
            </td>           
            <td rowspan="3" class="centerText"><img width="180px" height="50px" src="${this.srcLogo}"></td>
        </tr>
    </table>

    <table id="metaLeft" class="marginBottom">
        <tr>
            <td class="table-header" style="width: 40%;">Fecha realización: </td>
            <td class="table-data" style="text-align:center">${moment(audit.auditInitDate, "YYYY/MM/DD HH:mm").format("DD/MM/YYYY HH:mm")}</td>                
        </tr>
    </table>
    <table id="metaRight" class="marginBottom">
        <tr>
            <td class="table-header" style="width: 20%;">Normas: </td> 
            <td class="table-data" style="text-align:center">${this.getStandard(audit)}</td>                
        </tr>
    </table>
    <table id="metaLeft" class="marginBottom">
        <tr>
            <td class="table-header" style="width: 20%;">Sector: </td>
            <td class="table-data" style="text-align:center">${audit.sectorPlant.sector.name} </td>                
        </tr>
    </table>
    <table id="metaRight" class="marginBottom">
        <tr>
            <td class="table-header" style="width: 20%;">Auditor: </td>
            <td class"table-data" style="text-align:center">${audit.auditor != null ? audit.auditor.name + " " + audit.auditor.surname : audit.externalAuditor}</td>
        </tr>
    </table>
    <table id="metaCenter" class="marginBottom">
        <tr>
            <td class="table-header" style="width: 10%;">Estado: </td>
            <td class="table-data" style="text-align:center">${audit.auditState.name} </td>                
        </tr>
    </table>
    <table id="metaCenter" class="marginBottom">
        <tr>
            <td rowspan="3" class="table-header" style="width: 18%;">Equipo auditor: </td>
            <td class="table-data" style="text-align:center">${audit.auditTeam != null ? audit.auditTeam : ""}</td>
        </tr>
    </table>

    <p class="labelBlack" style="margin-left: 15px;">Agenda</p>

    <table id="metaLeft" class="marginBottom">
      <tr>
          <td class="table-header" style="width: 60%;">Análisis de documentación: </td>
          <td class="table-data" style="text-align:center">${audit.documentsAnalysisDate != null ? moment(audit.documentsAnalysisDate, "YYYY/MM/DD HH:mm").format("DD/MM/YYYY") : ""}
      </tr>
    </table>
    <table id="metaRight" class="marginBottom">
      <tr>
          <td class="table-header" style="width: 45%;">Duración estimada: </td>
          <td class="table-data" style="text-align:center">${audit.documentAnalysisDuration} hs</td>
      </tr>
    </table>
    <table id="metaCenter" class="marginBottom">
      <tr>
          <td class="table-header" style="width: 18%;">Fecha de inicio: </td>
          <td class="table-data" style="text-align:center">${audit.auditInitDate != null ? moment(audit.auditInitDate, "YYYY/MM/DD HH:mm").format("DD/MM/YYYY HH:mm") : ""} hs</td>
      </tr>
    </table>
    <table id="metaLeft" class="marginBottom">
      <tr>
          <td class="table-header" style="width: 40%;">Reunión de cierre: </td>
          <td class="table-data" style="text-align:center">${audit.closeMeetingDate != null ? moment(audit.closeMeetingDate, "YYYY/MM/DD HH:mm").format("DD/MM/YYYY") : ""}</td>
      </td>
    </table>
    <table id="metaRight" class="marginBottom">
      <tr>
          <td class="table-header" style="width: 45%;">Duración estimada: </td>
          <td class="table-data" style="text-align:center">${audit.closeMeetingDuration} hs</td>
      </tr>
    </table>
    <table id="metaCenter" class="marginBottom">
      <tr>
          <td class="table-header" style="width: 28%;">Fecha de fin de auditoría:</td>
          <td class="table-data" style="text-align:center">${audit.auditFinishDate != null ? moment(audit.auditFinishDate, "YYYY/MM/DD HH:mm").format("DD/MM/YYYY HH:mm") : ""} hs</td>
      </tr>
    </table>
    
    <p class="labelBlack" style="margin-left: 15px;">Conclusiones: </p>

    <table id="metaCenter" class="marginBottom marginTop">
        <tr>
            <td class="table-data centerText">${audit.conclusion != null ? audit.conclusion : "---"} </td>
        </tr>
    </table>    

    <p class="labelBlack" style="margin-left: 15px;">Observaciones y Recomendaciones: </p>
    
    <table id="metaCenter" class="marginBottom marginTop">
        <tr>
            <td class="table-data centerText">${audit.recomendations != null ? audit.recomendations : "---"} </td>
        </tr>
    </table>

          ${this.getFindings(audit)}

    </div>`;

    //setTimeout(() => {
    this.fileName = `Informe de auditoría - ${moment().format("DD/MM/YYYY HH:mm")}`;
    //},10);

    return auditPrintHTML;

  }

  private getFindingType(findingTypeId) {
    if (this._allFindingType) {
      let findingType = this._allFindingType.find(x => x.id == findingTypeId);
      return findingType.name;
    }
    return "";
  }

  private getStandard(audit) {
    let standardNames = [];
    $(audit.auditStandards).each((index, element) => {
      standardNames.push(element.standard.name);
    });
    return standardNames.join(', ');
  }

  private getFindings(audit) {

    let contentFindings = "";
    $(audit.auditStandards).each((i, element) => {
      let standardDescription = element.standard.name;
      if (element.auditStandardAspects.length) {

        $(element.auditStandardAspects).each((k, standardAspect) => {

          contentFindings += `<tr>
                <td class="table-line" style="text-align: center;"> ${standardDescription} </td>`;
          contentFindings += `<td class="table-line" style="text-align: center;"> ${standardAspect.aspect.aspectID} - ${standardAspect.aspect.title} </td>`;

          if (standardAspect.findings.length) {

            contentFindings += `<td class="table-line" style="text-align: center;">`

            $(standardAspect.findings).each((j, finding) => {
              contentFindings += `<div> Id: ${finding.id} - ${this.getFindingType(finding.findingTypeID)} - ${finding.description}  </div>`;
            });

            contentFindings += `</td></tr>`;
          }
          else {
            if (standardAspect.noAudited) {
              contentFindings += `<td class="table-line" style="text-align: center;">Este aspecto no se auditó. Motivo: ${standardAspect.description != null ? standardAspect.description : ""}</td></tr>`;
            }
            else if (standardAspect.withoutFindings) {
              contentFindings += `<td class="table-line" style="text-align: center;">No se registran hallazgos para este aspecto</td></tr>`;
            }
            else {
              contentFindings += `<td class="table-line" style="text-align: center;">No se registran hallazgos para este aspecto</td></tr>`;
            }
          }

        });
      }
    });

    let tableFindings = `<table class="tableFinding page-break" style="width: 100%;margin-bottom: 10px;">    
    <tr>
        <th class="table-header" style="text-align:center !important;"> Norma </th>
        <th class="table-header" style="text-align:center !important;"> Aspectos </th>
        <th class="table-header" style="text-align:center !important;"> Hallazgos encontrados </th>
    </tr>    
     <div id="contentFindig">
          ${contentFindings}
     </div>
    </table>`;

    return tableFindings;
  }

  deleteAudit(audit) {
    Swal.fire({
      text: '¿Desea eliminar la auditoría?',
      type: 'question',
      showCancelButton: true,
      confirmButtonText: 'Si',
      cancelButtonText: 'No ',
      focusCancel: true
    }).then((result) => {
      if (result.value) {
        this.blockUI.start();
        this._auditService.delete(audit.auditID, audit.workflowId)
          .subscribe(res => {
            this.getAudits();
          })
      }
    });
  }

  setPermissions() {
      this.canSchedule = this._canSchedule();
      this.canPlanning = this._canPlanning();
      this.canReSchedule = this._canReSchedule();
      this.canApporvePlanning = this._canApporvePlanning();
      this.canEmmitReport = this._canEmmitReport();
      this.canApproveReport = this._canApproveReport();
      this.canDelete = this._canDelete();
      this.canExport = this._canExport();
      this.canAddFindings = this._canAddFindings();
    }

  private _canSchedule() {
      return this._authService.hasClaim(PERMISSIONS.AUDIT.SCHEDULE);
    }

  private _canPlanning() {
      return this._authService.hasClaim(PERMISSIONS.AUDIT.PLANNING);
    }

  private _canReSchedule() {
      return this._authService.hasClaim(PERMISSIONS.AUDIT.RESCHEDULE);
    }

  private _canApporvePlanning() {
      return this._authService.hasClaim(PERMISSIONS.AUDIT.APPROVE_PLANNING);
    }

  private _canEmmitReport() {
      return this._authService.hasClaim(PERMISSIONS.AUDIT.EMMIT_REPORT);
    }

  private _canApproveReport() {
      return this._authService.hasClaim(PERMISSIONS.AUDIT.APPROVE_REPORT);
    }

  private _canDelete() {
      return this._authService.hasClaim(PERMISSIONS.AUDIT.DELETE);
    }

  private _canExport() {
      return this._authService.hasClaim(PERMISSIONS.AUDIT.EXPORT);
    }

  private _canAddFindings() {
      return this._authService.hasClaim(PERMISSIONS.AUDIT.ADD_FINDINGS);
    }


  ngOnDestroy() {
      this.ngUnsubscribe.next();
      this.ngUnsubscribe.complete();
    }


}



