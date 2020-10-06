import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

import { NgBlockUI, BlockUI } from 'ng-block-ui';

import { FindingsService } from '../../../../core/services/findings.service';

import { Finding } from "../../models/Finding";
import { FindingStateCode } from "../../models/FindingStateCode";
import * as moment from 'moment';
import { CompanyService } from '../../../../core/services/company.service';

import * as PERMISSIONS from '../../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

declare var $: any;

@Component({
  selector: 'app-read',
  templateUrl: './read.component.html',
  styleUrls: ['./read.component.css'],
})
export class ReadComponent implements OnInit, OnDestroy {

  popoverTitle = "Exportar informe";
  fileName;
  cols = [
    { field: 'id', header: 'ID' },
    { field: 'description', header: 'Descripción' },
    { field: 'findingTypeName', header: 'Tipo' },
    { field: 'findingStateName', header: 'Estado' },
    { field: 'sectorPlantTreatmentName', header: 'Sector-Planta' },
    { field: 'responsibleUserFullName', header: 'Responsable' },
    { field: 'expirationDate', header: 'Vencimiento' }
  ];


  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  findings: Finding[] = new Array<Finding>();
  findingsSignalRTemporary: Finding[] = new Array<Finding>();
  finsingsLoad: boolean = false;
  findingStateCode = FindingStateCode;
  company: any;
  srcLogo: any;

  canApprove;
  canReassign;
  canApproveReassignment;
  canUpdateApproved;
  canClose;
  canEditExpirationDate;
  canAdd;
  canReassignDirectly;

  userLogged;
  userIsReferrent2;
  userIsReferrent;

  constructor(
    private _findingService: FindingsService,
    private _authService: AuthService,
    private _companyService: CompanyService
  ) {

  }

  ngOnInit() {
    this.connectSignalR();
    this.TransferFindingData();
    this.blockUI.start();
    this.setPermissions();
    this.userLogged = this._authService.getUserLogged();
    this._companyService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        if (res.length > 0) {
          this.company = res[0];
          this.srcLogo = this.company.logo;
        }
      });

    this._findingService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.findings = res.map(el => {
          return {
            ...el,
            editAsRefferrent: this.isResponsibleReferrent(el),
            isResponsibleUser: this.userLogged.id == el.responsibleUserID
          }
        });
        this.findings.sort(sortByIdDesc)
        this.blockUI.stop();
        console.log(this.findings);
        this.finsingsLoad = true;
        this.findingsSignalRTemporary.forEach(finding => {
          this.patchFindingSignalR(finding)
        });
        setTimeout(this.setClickPaginator, 1500);
      })
  }

  setClickPaginator = () => {
    $(".ui-paginator-element").click(() => {
      this.setClickPaginator()
      $(".main-panel.ps.ps--active-y").scrollTop(0);
    })
  }

  TransferFindingData() {
    this._findingService.finding$
      .takeUntil(this.ngUnsubscribe)
      .subscribe(finding => {
        this._findingService.getAll()
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            this.findings = res.map(el => {
              return {
                ...el,
                editAsRefferrent: this.isResponsibleReferrent(el),
                isResponsibleUser: this.userLogged.id == el.responsibleUserID
              }
            });
            this.findings.sort(sortByIdDesc)
            setTimeout(this.setClickPaginator, 1500);
          });
        // if(finding.findingID){
        //   if(this.finsingsLoad){
        //     this.patchFindingSignalR(finding)
        //   }
        //   else{
        //     this.findingsSignalRTemporary.push(finding);
        //   }   
        // }
      })
  }

  patchFindingSignalR(finding) {
    let index = this.findings.findIndex(x => x.id == finding.findingID)
    if (index >= 0) {
      // this.findings[index].isInProcessWorkflow = finding.isInProcessWorkflow;
      // this.findings[index].description = finding.description;
      // this.findings[index].findingTypeName = finding.findingTypeName;
      // this.findings[index].findingStateName = finding.findingStateName
      // this.findings[index].findingStateColor = finding.findingStateColor
      // this.findings[index].findingStateCode = finding.findingStateCode
      // this.findings[index].sectorPlantTreatmentSectorName = finding.sectorPlantTreatmentSectorName
      // this.findings[index].sectorPlantTreatmentPlantName = finding.sectorPlantTreatmentPlantName
      // this.findings[index].responsibleUserFullName = finding.responsibleUserFullName
      // this.findings[index].expirationDate = finding.expirationDate
      // this.findings[index].findingComments = finding.findingComments
      // this.findings[index].sectorPlantTreatmentName = finding.sectorPlantTreatmentName;
    }
    else {
      let newFinding: Finding = new Finding();
      newFinding.isInProcessWorkflow = finding.isInProcessWorkflow;
      newFinding.description = finding.description;
      newFinding.findingTypeName = finding.findingTypeName;
      newFinding.findingStateName = finding.findingStateName
      newFinding.findingStateColor = finding.findingStateColor
      newFinding.findingStateCode = finding.findingStateCode
      newFinding.sectorPlantTreatmentSectorName = finding.sectorPlantTreatmentSectorName
      newFinding.sectorPlantTreatmentPlantName = finding.sectorPlantTreatmentPlantName
      newFinding.responsibleUserFullName = finding.responsibleUserFullName
      newFinding.expirationDate = finding.expirationDate;
      newFinding.id = finding.findingID
      newFinding.findingComments = finding.findingComments
      newFinding.sectorPlantTreatmentName = finding.sectorPlantTreatmentName
      this.findings.unshift(newFinding)
    }
    console.log(this.findings)
  }

  connectSignalR() {
    this._findingService.startConnection();
    this._findingService.addTransferChartDataListener();
  }

  isResponsibleReferrent(el) {
    this.userIsReferrent2 = this.userLogged.job == el.sectorPlantTreamtentReferring2JobId;
    if (this.userIsReferrent2) {
      const isFindingAssignedToReferrent = el.responsibleUserJob == el.sectorPlantTreamtentReferringJobId
      return isFindingAssignedToReferrent;
    }
    this.userIsReferrent = this.userLogged.job == el.sectorPlantTreamtentReferringJobId;
    if (this.userIsReferrent) {
      const isFindingAssignedToReferrent = el.responsibleUserJob == el.sectorPlantTreamtentReferringJobId
      return isFindingAssignedToReferrent;
    }
    return this.userLogged.job == el.responsibleUserJob;
  }



  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
    this._findingService.disconnect();
  }

  setPermissions() {
    debugger
    this.canAdd = this._canAdd();
    this.canApprove = this._canApprove();
    this.canReassign = this._canReassign();
    this.canApproveReassignment = this._canApproveReassignment();
    this.canUpdateApproved = this._canUpdateApproved();
    this.canClose = this._canClose();
    this.canEditExpirationDate = this._canEditExpirationDate();
    this.canReassignDirectly = this._canReassignDirectly();
  }

  private _canApprove() {
    return this._authService.hasClaim(PERMISSIONS.FINDING.APPROVE) && this._authService.hasClaim(PERMISSIONS.FINDING.REJECT);
  }

  private _canReassign() {
    return this._authService.hasClaim(PERMISSIONS.FINDING.REASSIGN)
  }

  private _canApproveReassignment() {
    return this._authService.hasClaim(PERMISSIONS.FINDING.APPROVE_REASSIGNMENT) && this._authService.hasClaim(PERMISSIONS.FINDING.REJECT_REASSIGNMENT);
  }

  private _canUpdateApproved() {
    return this._authService.hasClaim(PERMISSIONS.FINDING.UPDATE_APPROVED)
  }

  private _canClose() {
    return this._authService.hasClaim(PERMISSIONS.FINDING.CLOSE)
  }

  private _canEditExpirationDate() {
    return this._authService.hasClaim(PERMISSIONS.FINDING.EDIT_EXPIRATION_DATE)
  }

  private _canAdd() {
    return this._authService.hasClaim(PERMISSIONS.FINDING.ADD);
  }

  private _canReassignDirectly() {
    return this._authService.hasClaim(PERMISSIONS.FINDING.REASSIGN_DIRECTLY);
  }

  clickExportPdf(findingId: number) {
    var findings = this.findings as any
    let finding: any = findings.find(x => x.id == findingId) as any;

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

        .marginRight {
          margin-right: 20px;
        }

        .marginTop {
          margin-top: 20px;
        }
    
      -->
    </style>
    <div id="page-wrap">
      <table style="width:100%;margin-bottom: 10px" id="tableLogo">
          <tr>
              <td rowspan="3" class="centerText"><img width="150px" height="100px"
                      src="../../../../../assets/img/hoshinlogo.png"></td>
              <td rowspan="3" class="centerText labelBlack">${finding.findingTypeName != null ? finding.findingTypeName : ''}<div>
                      <div class="t labelBlack">Estado: ${finding.findingStateName != null ? finding.findingStateName : ''}</div>
                  </div>
              </td>
              <td rowspan="3" class="centerText"><img width="180px" height="50px"
                      src="${this.srcLogo}"></td>
          </tr>
      </table>

      <table id="metaLeft" class="marginBottom">
          <tr>
              <td class="table-header" style="width: 33%;">Sector Emisor:</td>
              <td class="centerText table-data">${finding.sectorPlantEmitterSectorName != null ? finding.sectorPlantEmitterSectorName : ''}</td>
          </tr>
      </table>

      <table id="metaRight" class="marginBottom">
          <tr>
              <td class="table-header" style="width: 35%;">Fecha Creación:</td>
              <td class="centerText table-data">${finding.createdDate != '0001-01-01T00:00:00' ? moment(finding.createdDate, "YYYY/MM/DD HH:mm").format("DD/MM/YYYY") : ''}</td>
          </tr>
      </table>

      <table id="metaLeft" class="marginBottom">
          <tr>
              <td class="table-header" style="width: 40%;">Sector Ubicación:</td>
              <td class="centerText table-data">${finding.sectorPlantLocationSectorName != null ? finding.sectorPlantLocationSectorName : ''}</td>
          </tr>
      </table>
    
      <table id="metaRight" class="marginBottom">
          <tr>
              <td class="table-header" style="width: 44%;">Fecha Vencimiento:</td>
              <td class="centerText table-data">${moment(finding.expirationDate, "YYYY/MM/DD HH:mm").format("DD/MM/YYYY")}</td>
          </tr>
      </table>

      <table id="metaLeft" class="marginBottom">
          <tr>
              <td class="table-header" style="width: 42%;">Sector Tratamiento:</td>
              <td class="centerText table-data">
                  <div class="due">${finding.sectorPlantTreatmentSectorName != null ? finding.sectorPlantTreatmentSectorName : ''}</div>
              </td>
          </tr>
      </table>

      <table id="metaRight" class="marginBottom">
          <tr>
              <td class="table-header" style="width: 30%;">Responsable:</td>
              <td class="centerText table-data">
                  <div class="due">${finding.responsibleUserFullName != null ? finding.responsibleUserFullName : ""}</div>
              </td>
          </tr>
      </table>

      <div class="items">
          <label class="labelBlack">ID:</label>
          <div class="marginLeft">${finding.id != null ? finding.id : ''}</div>

          <label class="labelBlack page-break marginTop">Descripción:</label>
          <div class="marginLeft page-break marginRight marginTop">${finding.description != null ? finding.description : ''}</div>

          <label class="labelBlack page-break marginTop">Acción de Contención:</label>
          <div class="marginLeft page-break marginRight marginTop">${finding.containmentAction != null ? finding.containmentAction : ''}</div>

          <label class="labelBlack page-break marginTop">Análisis de Causas:</label>
          <div class="marginLeft page-break marginRight marginTop">${finding.causeAnalysis != null ? finding.causeAnalysis : ''}</div>

          <label class="labelBlack page-break marginTop">Comentarios:</label>
          <div class="marginLeft page-break marginRight marginTop">${finding.findingComments.length > 0 ? this.getFindingComments(finding.findingComments) : ''}</div>

          <label class="labelBlack page-break marginTop">Comentario Final:</label>
          <div class="marginLeft page-break marginRight marginTop">${finding.finalComment != null ? finding.finalComment : ''}</div>
      </div>

    </div>`;

    //setTimeout(() => {
    this.fileName = `Hallazgo - ${moment().format("DD/MM/YYYY HH:mm")}`;
    //},10);

    return findingPrintHTML;
  }

  getFindingComments(comments) {
    let comment = "";
    for (let index = 0; index < comments.map(x => x.comment).length; index++) {
      comment += comments[index].comment + '<br>';
    }
    return comment;
  }
}

function sortByIdDesc(f1: any, f2: any) {
  return f2.id - f1.id
}
