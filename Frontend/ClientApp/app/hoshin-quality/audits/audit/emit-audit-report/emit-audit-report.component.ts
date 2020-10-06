import Swal  from 'sweetalert2/dist/sweetalert2';
import { Component, OnInit, ViewChildren, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { AuditService } from '../../audit.service';
import { ActivatedRoute} from '@angular/router';
import { Router } from '@angular/router';
import { ToastrManager } from 'ng6-toastr-notifications';
import { AuditStandardAspectService } from '../../audit-standard-aspect.service';
import { Audit } from '../../models/Audit';
import { FindingsService } from 'ClientApp/app/core/services/findings.service';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';
import { SwalPartialTargets } from '@toverux/ngx-sweetalert2';
import { Subject } from 'rxjs';



declare const $: any;

@Component({
  selector: 'app-emit-audit-report',
  templateUrl: './emit-audit-report.component.html',
  styleUrls: ['./emit-audit-report.component.css']
})
export class EmitAuditReportComponent implements OnInit, OnDestroy {
  language_closeButton: string;
  @ViewChildren('wizard') wizard;
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  get conclusion() { return this.emitReportForm.get('conclusion'); }
  get recomendation() { return this.emitReportForm.get('recomendation'); }

  constructor(
    private _auditService: AuditService,
    private _route: ActivatedRoute,
    private _findingService: FindingsService,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private _auditStandardAspect: AuditStandardAspectService,
    private _fb: FormBuilder,
    public readonly swalTargets: SwalPartialTargets
  ) { }

  audit: Audit;
  emittingReport: boolean = false;
  //auditStandardAspect = new AuditStandardAspect() 
  standars;

  emitReportForm: FormGroup;

  auditStandardAspectSelected = [];
  ngOnInit() {
    this.blockUI.start()
    this.language_closeButton = 'Cerrar';
    this._route.params
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res) => {
      this._auditService.get(res.id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(res => {
        this.audit = res
        this.getAuditStandardAspects();
      })
    })
  }

  getAuditStandardAspects() {
    this._auditStandardAspect.getAllForAudit(this.audit.auditID)
    .takeUntil(this.ngUnsubscribe)
    .subscribe(res => {
      this.patchAspect(res);
      console.log(res);
      this.blockUI.stop();
    })
  }

  deleteFinding(findingId) {
    this.blockUI.start();
    this._auditStandardAspect.delete(findingId)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(res => {
        this.auditStandardAspectSelected = [];
        this.getAuditStandardAspects();
      })
  }

  setWithoutFindings(standardId, aspectId) {
    this.blockUI.start();
    this._auditStandardAspect.setWithoutFindings(this.audit.auditID, standardId, aspectId)
    .takeUntil(this.ngUnsubscribe)  
    .subscribe(res => {
        this.auditStandardAspectSelected = [];
        this.getAuditStandardAspects();
      });
  }

  async setNoAudited(aspectStandard) {
    const { value: description } = await Swal.fire({
      title: 'Ingrese descripción',
      allowOutsideClick: false,
      showCancelButton: true,
      focusCancel: true,
      confirmButtonText: 'Aceptar',
      cancelButtonText: 'Cancelar',
      input: 'text',
      inputPlaceholder: 'Descripción',
      class: 'swal2-input',
      inputValidator: (value) => {
        if (!value) {
          return 'Este campo no puede estar vacío'
        }
      }
    })
  
    if(description) {
      this.blockUI.start();
      this._auditStandardAspect.setNoAudited(this.audit.auditID, aspectStandard.standardID, aspectStandard.aspectID, description)
      .takeUntil(this.ngUnsubscribe)  
      .subscribe(res => {
          this.auditStandardAspectSelected = [];
          this.getAuditStandardAspects();
        });
    }
  }



  patchAspect(auditStandardAspects) {
    auditStandardAspects.forEach(auditStandardAspect => {
      let idaspect = "#" + auditStandardAspect.aspectID
      $(idaspect).attr('checked', true);
      this.auditStandardAspectSelected.push(auditStandardAspect)
    });
    this.audit.auditStandard
  }

  onSubmit() {
    if (this.emitReportForm.valid) {
      this.blockUI.start();
      this.audit.conclusion = this.conclusion.value;
      this.audit.recomendations = this.recomendation.value;

      this._auditService.emitReport(this.audit)
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          this.myfunction(res);
        });
    }
  }

  myfunction(res) {
    this._toastrManager.successToastr('Se emitió el informe de auditoría para su aprobación correctamente', "Informe de Auditorías emitido");
    this._router.navigate(['/quality/audits/list']);
    this.blockUI.stop();
  }

  onFinishAspects() {
    if (this.completeAllAspects()) {
      this.emittingReport = true;
      this.emitReportForm = this.createModel();
    } else {
      this._toastrManager.warningToastr('Todos los aspectos deben estar revisados', 'No se puede continuar')
    }
  }


  cancelSubmit() {
    this.emittingReport = false;
  }

  createModel() {
    return this._fb.group({
      conclusion: ['', Validators.required],
      recomendation: ['', Validators.required]
    })
  }

  completeAllAspects() {
    let canEmitReport = true;

    this.auditStandardAspectSelected.forEach(auditStandardAspect => {
      if (!(auditStandardAspect.noAudited || auditStandardAspect.withoutFindings || auditStandardAspect.findings.length > 0)) {
        canEmitReport = false;
      }
    });

    return canEmitReport;
  }

  ngAfterViewInit() {
    this.wizard.changes
    .takeUntil(this.ngUnsubscribe)
    .subscribe(() => this.initializeWizard());

    $(window).resize(() => {
      $('.card-wizard').each(function () {

        const $wizard = $(this);
        const index = $wizard.bootstrapWizard('currentIndex');
        let $total = $wizard.find('.nav li').length;
        let $li_width = 100 / $total;

        let total_steps = $wizard.find('.nav li').length;
        let move_distance = $wizard.width() / total_steps;
        let index_temp = index;
        let vertical_level = 0;

        let mobile_device = $(document).width() < 600 && $total > 3;

        if (mobile_device) {
          move_distance = $wizard.width() / 2;
          index_temp = index % 2;
          $li_width = 50;
        }

        $wizard.find('.nav li').css('width', $li_width + '%');

        let step_width = move_distance;
        move_distance = move_distance * index_temp;

        let $current = index + 1;

        if ($current == 1 || (mobile_device == true && (index % 2 == 0))) {
          move_distance -= 8;
        } else if ($current == total_steps || (mobile_device == true && (index % 2 == 1))) {
          move_distance += 8;
        }

        if (mobile_device) {
          let x: any = index / 2;
          vertical_level = parseInt(x);
          vertical_level = vertical_level * 38;
        }

        $wizard.find('.moving-tab').css('width', step_width);
        $('.moving-tab').css({
          'transform': 'translate3d(' + move_distance + 'px, ' + vertical_level + 'px, 0)',
          'transition': 'all 0.5s cubic-bezier(0.29, 1.42, 0.79, 1)'
        });

        $('.moving-tab').css({
          'transition': 'transform 0s'
        });
      });
    });
  }

  initializeWizard() {

    // Code for the Validator
    const $validator = $('.card-wizard form').validate({
      rules: {

      },

      highlight: function (element) {
        $(element).closest('.form-group').removeClass('has-success').addClass('has-danger');
      },
      success: function (element) {
        $(element).closest('.form-group').removeClass('has-danger').addClass('has-success');
      },
      errorPlacement: function (error, element) {
        $(element).append(error);
      }
    });

    // Wizard Initialization
    this.bootstrapWizard($validator);

    $('[data-toggle="wizard-radio"]').click(function () {
      const wizard = $(this).closest('.card-wizard');
      wizard.find('[data-toggle="wizard-radio"]').removeClass('active');
      $(this).addClass('active');
      $(wizard).find('[type="radio"]').removeAttr('checked');
      $(this).find('[type="radio"]').attr('checked', 'true');
    });

    $('[data-toggle="wizard-checkbox"]').click(function () {
      if ($(this).hasClass('active')) {
        $(this).removeClass('active');
        $(this).find('[type="checkbox"]').removeAttr('checked');
      } else {
        $(this).addClass('active');
        $(this).find('[type="checkbox"]').attr('checked', 'true');
      }
    });

    $('.set-full-height').css('height', 'auto');
  }

  bootstrapWizard($validator) {
    $('.card-wizard').bootstrapWizard({
      'tabClass': 'nav nav-pills',
      'nextSelector': '.btn-next',
      'previousSelector': '.btn-previous',

      onNext: function (tab, navigation, index, ) {

        var $valid = $('.card-wizard form').valid();

        //if $disponibleName == true the form is valid

        if (!$valid) {
          $validator.focusInvalid();
          return false;
        }
      },

      onInit: function (tab: any, navigation: any, index: any) {

        // check number of tabs and fill the entire row
        let $total = navigation.find('li').length;
        let $wizard = navigation.closest('.card-wizard');

        let $first_li = navigation.find('li:first-child a').html();
        let $moving_div = $('<div class="moving-tab">' + $first_li + '</div>');
        $('.card-wizard .wizard-navigation').append($moving_div);

        $total = $wizard.find('.nav li').length;
        let $li_width = 100 / $total;

        let total_steps = $wizard.find('.nav li').length;
        let move_distance = $wizard.width() / total_steps;
        let index_temp = index;
        let vertical_level = 0;

        let mobile_device = $(document).width() < 600 && $total > 3;

        if (mobile_device) {
          move_distance = $wizard.width() / 2;
          index_temp = index % 2;
          $li_width = 50;
        }

        $wizard.find('.nav li').css('width', $li_width + '%');

        let step_width = move_distance;
        move_distance = move_distance * index_temp;

        let $current = index + 1;

        if ($current == 1 || (mobile_device == true && (index % 2 == 0))) {
          move_distance -= 8;
        } else if ($current == total_steps || (mobile_device == true && (index % 2 == 1))) {
          move_distance += 8;
        }

        if (mobile_device) {
          let x: any = index / 2;
          vertical_level = parseInt(x);
          vertical_level = vertical_level * 38;
        }

        $wizard.find('.moving-tab').css('width', step_width);
        $('.moving-tab').css({
          'transform': 'translate3d(' + move_distance + 'px, ' + vertical_level + 'px, 0)',
          'transition': 'all 0.5s cubic-bezier(0.29, 1.42, 0.79, 1)'

        });
        $('.moving-tab').css('transition', 'transform 0s');
      },

      onTabClick: function (tab: any, navigation: any, index: any) {

        const $valid = $('.card-wizard form').valid();
        //if $disponibleName == true the form is valid
        $(".main-panel.ps.ps--active-y").scrollTop(0)

        if (!$valid) {
          return false;
        } else {
          return true;
        }
      },

      onTabShow: function (tab: any, navigation: any, index: any) {
        let $total = navigation.find('li').length;
        let $current = index + 1;

        const $wizard = navigation.closest('.card-wizard');

        // If it's the last tab then hide the last button and show the finish instead
        if ($current >= $total) {
          $($wizard).find('.btn-next').hide();
          $($wizard).find('.btn-finish').show();
        } else {
          $($wizard).find('.btn-next').show();
          $($wizard).find('.btn-finish').hide();
        }

        const button_text = navigation.find('li:nth-child(' + $current + ') a').html();

        setTimeout(function () {
          $('.moving-tab').text(button_text);
        }, 150);

        const checkbox = $('.footer-checkbox');

        if (index !== 0) {
          $(checkbox).css({
            'opacity': '0',
            'visibility': 'hidden',
            'position': 'absolute'
          });
        } else {
          $(checkbox).css({
            'opacity': '1',
            'visibility': 'visible'
          });
        }
        $total = $wizard.find('.nav li').length;
        let $li_width = 100 / $total;

        let total_steps = $wizard.find('.nav li').length;
        let move_distance = $wizard.width() / total_steps;
        let index_temp = index;
        let vertical_level = 0;

        let mobile_device = $(document).width() < 600 && $total > 3;

        if (mobile_device) {
          move_distance = $wizard.width() / 2;
          index_temp = index % 2;
          $li_width = 50;
        }

        $wizard.find('.nav li').css('width', $li_width + '%');

        let step_width = move_distance;
        move_distance = move_distance * index_temp;

        $current = index + 1;

        if ($current == 1 || (mobile_device == true && (index % 2 == 0))) {
          move_distance -= 8;
        } else if ($current == total_steps || (mobile_device == true && (index % 2 == 1))) {
          move_distance += 8;
        }

        if (mobile_device) {
          let x: any = index / 2;
          vertical_level = parseInt(x);
          vertical_level = vertical_level * 38;
        }

        $wizard.find('.moving-tab').css('width', step_width);
        $('.moving-tab').css({
          'transform': 'translate3d(' + move_distance + 'px, ' + vertical_level + 'px, 0)',
          'transition': 'all 0.5s cubic-bezier(0.29, 1.42, 0.79, 1)'

        });
      }
    });
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
