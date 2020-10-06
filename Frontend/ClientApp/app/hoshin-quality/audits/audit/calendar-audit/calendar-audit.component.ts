import { AuthService } from './../../../../core/services/auth.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { AUDIT } from '../../../../core/permissions/index';
import Swal from 'sweetalert2/dist/sweetalert2.js';
import PerfectScrollbar from 'perfect-scrollbar';
import { AuditService } from "../../audit.service";
import { Audit } from '../../models/Audit';
import { AuditCalendar } from "../../models/AuditsCalendar";
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';

declare const $: any;
var audits: Audit[]

@Component({
  selector: 'app-calendar-audit',
  templateUrl: './calendar-audit.component.html',
  styleUrls: ['./calendar-audit.component.css']
})
export class CalendarAuditComponent implements OnInit, OnDestroy {
  
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  constructor(
    private _auditService: AuditService,
    private _router: Router,
    private _authService: AuthService) {
   }
  auditsCalendar: {}
  auditCalendars: AuditCalendar[] = new Array();
  currentView;
  canSchedule: boolean;
  
  ngOnInit() {
    this.blockUI.start()
    this._auditService.getAll()
    .takeUntil(this.ngUnsubscribe)
    .subscribe(audit => {
      audits = audit;
      this.blockUI.stop();
      audits.forEach(audit => {
        let auditsCalendar = new AuditCalendar();
         auditsCalendar.id = audit.auditID;
         auditsCalendar.title = audit.auditType.name;
         auditsCalendar.start = new Date(audit.auditInitTime).getTime() == new Date("0001-01-01T00:00:00").getTime() ? audit.auditInitDate.substring(0,10) : audit.auditInitTime;;
         auditsCalendar.url = audit.auditID;
         //auditsCalendar.end = audit.auditFinishTime;
         //auditsCalendar.className ;
         this.auditCalendars.push(auditsCalendar); 
      });
      this.setPermissions();
      this.loadCalendar(this)
      this.addClickButton();
      this.setColorAndIdAudit(this);
      this.changeYearLanguage()
      //this.setEventClick();
    }) 
  }

  _canSchedule() {
    return this._authService.hasClaim(AUDIT.SCHEDULE)
  }

  setPermissions() {
    this.canSchedule = this._canSchedule();
  }

  changeYearLanguage(){
    $('.fc-year-button').text("Año");
    $('.fc-year-button').css("margin-right", "9px");
  }

  redirectScheduleAudit(start){
    let date = new Date(start)
    date.setHours(0)
    this._auditService.setCreateAuditDate(date)  
    this._router.navigate(["/quality/audits/schedule"])
  }

  addClickButton(){
    $('.fc-button').on('click',{_context:this},this.setColorAndIdAudit)
    $('.fc-year-button').on('click',{_context:this},this.saveCurrentView);
    $('.fc-month-button').on('click',{_context:this},this.saveCurrentView);
    $('.fc-agendaWeek-button').on('click',{_context:this},this.saveCurrentView);
    $('.fc-agendaDay-button').on('click',{_context:this},this.saveCurrentView);
  }

  saveCurrentView(context){
    context.data._context.currentView = $(this).text();
  }

  setColorAndIdAudit(context) {
    audits.forEach(audit => {
      let id = audit.auditID
      $(`a[href='${id}'`).css("background-color", `${audit.auditState.color}`);
      $(`a[href='${id}'`).attr("id", id);
      $(`a[href='${id}'`).removeAttr("href");
    })
    if (context.data){
      $(`.fc-day-grid-event`).click(context.data._context.showAudit)
    }
    else{
      $(`.fc-day-grid-event`).click(this.showAudit)
    } 
  }

  showAudit(){
    let id = $(this).attr('id');
    let audit = audits.find(audit =>{
      return audit.auditID == id
    })

    Swal.fire({
      title: `${new Date(audit.auditInitDate).toLocaleDateString()}`,
      html:
      `
      <div class="row justify-content-center">
        <div class="col-md-6">
          Fecha de creación
          <br/> 
          <label class=" col-form-label">${new Date(audit.creationDate).toLocaleDateString()}</label>
        </div>
        <div class="col-md-6">
          Tipo de Auditoría
          <br/>  
          <label class=" col-form-label">${audit.auditType.name}</label>
        </div>
      </div>
      <br/>
      <div class="row justify-content-center">
        <div class="col-md-6">
          Planta
          <br/> 
          <label class=" col-form-label">${audit.sectorPlant.plant.name}</label>
        </div>
        <div class="col-md-6">
          Sector
          <br/> 
          <label class=" col-form-label">${audit.sectorPlant.sector.name}</label>
        </div>
      </div>
      <br/>
      <div class="row justify-content-center">
        <div class="col-md-6">
          Estado
          <br/>  
          <label class=" col-form-label">${audit.auditState.name}</label>
        </div>
        <div class="col-md-6">
          Responsable 
          <br/> 
          <label class=" col-form-label">${audit.auditorID != null ?  audit.auditor.name + ' ' +  audit.auditor.surname  : audit.externalAuditor}</label>
        </div>
      </div>`,
      confirmButtonText: 'Confirmar'
    })
  }

  loadCalendar(context){
    const $calendar = $('#fullCalendar');

    const today = new Date();
    const y = today.getFullYear();
    const m = today.getMonth();
    const d = today.getDate();
    const contextConponent = context

    $calendar.fullCalendar({
      locale: 'es',
      viewRender: function (view: any, element: any) {
        // We make sure that we activate the perfect scrollbar when the view isn't on Month
        if (view.name != 'month') {
          var elem = $(element).find('.fc-scroller')[0];
          let ps = new PerfectScrollbar(elem);
        }
      },
      header: {
        left: 'title',
        center: 'year,month, agendaWeek, agendaDay',
        right: 'prev, next, today'
      },
      defaultView: 'month',
      defaultDate: today,
      selectable: this.canSchedule,
      selectHelper: true,
      views: {
        month: { // name of view
          titleFormat: 'MMMM YYYY'
          // other view-specific options here
        },
        week: {
          titleFormat: ' MMMM D YYYY'
        },
        day: {
          titleFormat: 'D MMM, YYYY'
        }
      },

      select: function (start: Date, end: any) {
        start = new Date(start);
        if(contextConponent.currentView =="Año") return false
        if(contextConponent.currentView =="Mes" || !(contextConponent.currentView)) start.setDate(start.getDate() + 1);
        start.setHours(0,0,0,0);
        let now = new Date();
        now.setHours(0,0,0,0);
        if(now.getTime() < start.getTime()){
          Swal.fire({
            title: `Programar una auditoría para la fecha: ${new Date(start).toLocaleDateString()}`,
            html: '<div class="form-group">' + '</div>',
            showCancelButton: true,
            confirmButtonText: 'Confirmar',
            cancelButtonText: "Cancelar",
          }).then(function (result: any) {
            if (result.value) {
              contextConponent.redirectScheduleAudit(start)
            }
          });
          
        }

        // on select we show the Sweet Alert modal with an input
        
      },

      editable: false,
      eventLimit: true, // allow "more" link when too many events
      events: this.auditCalendars
    });
  }


  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
