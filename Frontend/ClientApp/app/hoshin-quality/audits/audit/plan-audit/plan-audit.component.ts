import { Component, OnInit, ViewChildren, ElementRef, AfterViewInit, ViewChild } from '@angular/core';
import { StandardService } from "../../configuration/standard.service";
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuditService } from "../../audit.service";
import { Audit } from '../../models/Audit';
import { AuditStandardAspect } from '../../models/AuditStandardAspect';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrManager } from 'ng6-toastr-notifications';
import { AuditStandardAspectService } from "../../audit-standard-aspect.service";
import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { AppDateAdapter, APP_DATE_FORMATS } from "../../../../shared/util/dates/date.adapter";

declare const $: any;

@Component({
    selector: 'app-plan-audit',
    templateUrl: './plan-audit.component.html',
    styleUrls: ['./plan-audit.component.css'],
    providers: [
        {
            provide: DateAdapter, useClass: AppDateAdapter
        },
        {
            provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
        }
    ],
})
export class PlanAuditComponent implements OnInit, AfterViewInit {

    @BlockUI() blockUI: NgBlockUI;
    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
    @ViewChildren('wizard') wizard;
    @ViewChild('title') title: ElementRef;

    get auditGroup() { return this.type.get('auditGroup'); }
    get documentsAnalysisDate() { return this.type.get('documentsAnalysisDate'); }
    get documentAnalysisDuration() { return this.type.get('documentAnalysisDuration'); }
    get auditInitDate() { return this.type.get('auditInitDate'); }
    get auditInitTime() { return this.type.get('auditInitTime'); }
    get auditFinishDate() { return this.type.get('auditFinishDate'); }
    get auditFinishTime() { return this.type.get('auditFinishTime'); }
    get mettingCloseDate() { return this.type.get('mettingCloseDate'); }
    get mettingCloseTime() { return this.type.get('mettingCloseTime'); }
    get planCloseDate() { return this.type.get('planCloseDate'); }

    type: FormGroup;
    isCreate = true;
    audit: Audit;
    standars = []
    auditStandardAspectSelected = [];

    cantChecked: number = 1;

    constructor(
        private _standardService: StandardService,
        private fb: FormBuilder,
        private _auditService: AuditService,
        private _route: ActivatedRoute,
        private _router: Router,
        private _toastrManager: ToastrManager,
        private _auditStandardAspect: AuditStandardAspectService
    ) { }

    ngOnInit() {
        this.createModel()
        this.blockUI.start()
        this._route.params.subscribe((res) => {
            this._auditService.get(res.id).subscribe(res => {
                this.audit = res
                this.audit.auditStandards.forEach(standard => {
                    standard.aspectSelected = []
                });
                this.auditInitDate.patchValue(this.audit.auditInitDate);
                this.blockUI.stop()
                this._auditStandardAspect.getAllForAudit(this.audit.auditID).subscribe(res => {
                    this.patchAspect(res)
                })
            })
        })

        this.documentsAnalysisDate.valueChanges.subscribe((res) => console.log(this.documentsAnalysisDate));
    }

    showPanel(standradID) {
        this.markControlsAsTouched();

        const isAspectTab = $('.tab-pane.active').attr('id') != 'basic';
        if (this.type.valid && this._validateDatesWithoutToastr()) {
            if ($(`#${standradID}.tab-pane`).find('.aspectCheck:checked').length > 20) {
                $('.tab-pane').removeClass('active show')
                $(`#${standradID}.tab-pane`).addClass('active show')
            }
        }
    }

    patchAudit() {
        this.auditGroup.patchValue(this.audit.auditTeam);
        this.documentsAnalysisDate.patchValue(this.audit.documentsAnalysisDate)
        this.documentAnalysisDuration.patchValue(this.audit.documentAnalysisDuration)
        this.auditInitDate.patchValue(this.audit.auditInitDate)
        let auditInitTime = new Date(this.audit.auditInitTime)
        this.auditInitTime.patchValue(this.formatTime(auditInitTime))
        this.mettingCloseDate.patchValue(this.audit.closeMeetingDate)
        this.mettingCloseTime.patchValue(this.audit.closeMeetingDuration)
        this.planCloseDate.patchValue(this.audit.closeDate);
        this.auditFinishDate.patchValue(this.audit.auditFinishDate);
        let auditFinishTime = new Date(this.audit.auditFinishTime);
        this.auditFinishTime.patchValue(this.formatTime(auditFinishTime))
    }

    formatTime(date) {
        let hours = date.getHours()
        let minutes = date.getMinutes()
        if (hours < 10) {
            hours = '0' + hours
        }
        if (minutes < 10) {
            minutes = '0' + minutes
        }
        return (hours + ':' + minutes)
    }

    patchAspect(auditStandardAspects) {
        
        auditStandardAspects.forEach(auditStandardAspect => {
            let idaspect = "#" + auditStandardAspect.aspectID
            $(idaspect).attr('checked', true);
            this.auditStandardAspectSelected.push(auditStandardAspect)
        });

        this.audit.auditStandards.forEach(standard => {
            standard.standard.aspects = standard.standard.aspects.sort(function(a, b) {
                return a.title.localeCompare(b.title, undefined, {
                  numeric: true,
                  sensitivity: 'base'
                });
              });
            standard.standard.aspects.forEach(aspect => {
                auditStandardAspects.forEach(auditStandardAspect => {
                    if (aspect.aspectID == auditStandardAspect.aspectID) {
                        standard.aspectSelected.push(aspect);
                    }
                });
            });
        });
    }

    createModel() {
        this.type = this.fb.group({
            // To add a validator, we must first convert the string value into an array. The first item in the array is the default value if any, then the next item in the array is the validator. Here we are adding a required validator meaning that the firstName attribute must have a value in it.
            auditGroup: [null],
            documentsAnalysisDate: [null, Validators.required],
            documentAnalysisDuration: [1, [Validators.required, Validators.min(1), Validators.max(24)]],
            auditInitDate: [null, Validators.required],
            auditInitTime: [null, Validators.required],
            mettingCloseDate: [null, Validators.required],
            mettingCloseTime: [1, [Validators.required, Validators.min(1), Validators.max(24)]],
            planCloseDate: [null, Validators.required],
            auditFinishDate: [null, Validators.required],
            auditFinishTime: [null, Validators.required]
        });
    }

    initializeWizard() {
        // Code for the Validator
        const $validator = $('.card-wizard form').validate({
            rules: {
                auditGroup: {
                },
                documentsAnalysisDate: {
                    required: true
                },
                documentAnalysisDuration: {
                    required: true
                },
                auditInitDate: {
                    required: true
                },
                auditInitTime: {
                    required: true
                },
                mettingCloseDate: {
                    required: true
                },
                mettingCloseTime: {
                    required: true
                },
                planCloseDate: {
                    required: true
                },

                auditFinishDate: {
                    required: true
                },
                auditFinishTime: {
                    required: true
                }
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

        const validateFunctionWithoutToastr = () => this.type.valid && this._validateDatesWithoutToastr();
        const validateAspects = () => this.validateAspects();
        // Wizard Initialization
        this.bootstrapWizard($validator, validateFunctionWithoutToastr, validateAspects);

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

    ngAfterViewInit() {

        //Need wait to ngFor finish to initialize the wizard
        this.wizard.changes.subscribe(() => this.initializeWizard());

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

    bootstrapWizard($validator, validateFunctionWithoutToastr, validateAspects) {
        $('.card-wizard').bootstrapWizard({
            'tabClass': 'nav nav-pills',
            'nextSelector': '.btn-next',
            'previousSelector': '.btn-previous',

            onNext: function (tab, navigation, index, ) {

                console.log('Tab', tab);
                console.log('Navigation', navigation);
                console.log('index', index);
                // var $valid = $('.card-wizard form').valid();
                let $valid;

                if (index > 1) {
                    $valid = $('.tab-pane.active').find('.aspectCheck:checked').length > 0
                } else {
                    $valid = validateFunctionWithoutToastr()
                }

                //if $disponibleName == true the form is valid

                if (!$valid) {
                    $validator.focusInvalid();
                    return false;
                }
                const element = $('.tab-pane.active');
                element.next().addClass('active show');
                element.removeClass('active show');
                return true;
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

                return false;
                // let $valid;
                // console.log('VALID', $valid);
                // $(".main-panel.ps.ps--active-y").scrollTop(0)

                // if (index > 1) {
                //     $valid = validateAspects();
                // } else {
                //     $valid = validateFunctionWithoutToastr()
                // }

                // return $valid;

                // if (!$valid) {
                //     return false;
                // } else {
                //     return true;
                // }
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

    scrollTopAndShowPanel(navigation) {
        $(".main-panel.ps.ps--active-y").scrollTop(0);
        let element = $('.tab-pane.active');
        this.markControlsAsTouched();

        const isAspectTab = $('.tab-pane.active').attr('id') != 'basic';

        if (navigation == 'next') {
            if (this.type.valid && this.validateForm()) {
                if (!((isAspectTab && this.validateAspects()) || !isAspectTab)) {
                    this._toastrManager.errorToastr('Debe seleccionar al menos un aspecto', 'No se puede continuar');
                }
            } else {
                return;
            }
        }
        else {
            element.prev().addClass('active show');
            element.removeClass('active show');
        }

    }



    checkboxClicked(ev, aspect, standard) {
        let auditStandardAspect = new AuditStandardAspect();
        auditStandardAspect.standardID = standard.standardID;
        auditStandardAspect.auditID = this.audit.auditID;
        auditStandardAspect.aspectID = aspect.aspectID;
        auditStandardAspect.aspect = aspect;
        aspect.select = ev.target.checked;
        if (ev.target.checked) {
            //AddAspect
            this.auditStandardAspectSelected.push(auditStandardAspect);
            standard.aspectSelected.push(aspect);
        }
        else {
            //Remove Aspect
            let indexClaimToDelete = this.auditStandardAspectSelected.findIndex(x => x.aspectID == auditStandardAspect.aspectID);
            this.auditStandardAspectSelected.splice(indexClaimToDelete, 1);
            let indexAspect = standard.aspectSelected.findIndex(x => x.aspectID == auditStandardAspect.aspectID);
            standard.aspectSelected.splice(indexAspect, 1);
        }

        this.cantChecked = standard.aspectSelected.length;
    }

    validateAspects() {
        return $('.tab-pane.active').find('.aspectCheck:checked').length > 0;
    }

    validateForm() {
        if (new Date(new Date().setHours(0, 0, 0, 0)).getTime() > new Date(new Date(this.documentsAnalysisDate.value).setHours(0, 0, 0, 0)).getTime()) {
            this._toastrManager.errorToastr("La fecha de Análisis de documentación debe ser mayor o igual a la fecha de hoy", "Error"); return false;
        }
        let auditInitTimeHour = this.auditInitTime.value.substring(0, 2);
        let auditInitTimeMinutes = this.auditInitTime.value.substring(3, 5);

        let auditFinishTimeHour = this.auditFinishTime.value.substring(0, 2);
        let auditFinishTimeMinutes = this.auditFinishTime.value.substring(3, 5);

        if (new Date(new Date(this.documentsAnalysisDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.auditInitDate.value).setHours(0, 0, 0, 0)).getTime()) {
            if (new Date(new Date(this.auditInitDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.mettingCloseDate.value).setHours(0, 0, 0, 0)).getTime()) {
                if (new Date(new Date(this.mettingCloseDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.auditFinishDate.value).setHours(0, 0, 0, 0)).getTime()) {
                    if (new Date(new Date(this.auditInitDate.value).setHours(auditInitTimeHour, auditInitTimeMinutes)).getTime() < new Date(new Date(this.auditFinishDate.value).setHours(auditFinishTimeHour, auditFinishTimeMinutes)).getTime()) {
                        if (new Date(new Date(this.auditFinishDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.planCloseDate.value).setHours(0, 0, 0, 0)).getTime()) {
                            return true
                        }
                        this._toastrManager.errorToastr("La fecha de fin de auditoría debe ser menor a la fecha de cierre de plan", "Error"); return false;
                    }
                    this._toastrManager.errorToastr("La hora de inicio de auditoría se superpone con la hora de fin de auditoría", "Error"); return false;
                }
                this._toastrManager.errorToastr("La fecha de reunión de cierre debe ser menor o igual a la fecha de fin de auditoría", "Error"); return false;
            }
            this._toastrManager.errorToastr("La fecha de inico de auditoría debe ser menor o igual a la fecha de reunión de cierre", "Error"); return false;
        }
        this._toastrManager.errorToastr("La fecha de Análisis de documentación debe ser menor o igual a la fecha de inico de auditoría", "Error"); return false;
    }

    _validateDatesWithoutToastr() {
        if (new Date(new Date().setHours(0, 0, 0, 0)).getTime() > new Date(new Date(this.documentsAnalysisDate.value).setHours(0, 0, 0, 0)).getTime()) {
            return false;
        }
        let auditInitTimeHour = this.auditInitTime.value.substring(0, 2);
        let auditInitTimeMinutes = this.auditInitTime.value.substring(3, 5);

        let auditFinishTimeHour = this.auditFinishTime.value.substring(0, 2);
        let auditFinishTimeMinutes = this.auditFinishTime.value.substring(3, 5);

        if (new Date(new Date(this.documentsAnalysisDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.auditInitDate.value).setHours(0, 0, 0, 0)).getTime()) {
            if (new Date(new Date(this.auditInitDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.mettingCloseDate.value).setHours(0, 0, 0, 0)).getTime()) {
                if (new Date(new Date(this.mettingCloseDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.auditFinishDate.value).setHours(0, 0, 0, 0)).getTime()) {
                    if (new Date(new Date(this.auditInitDate.value).setHours(auditInitTimeHour, auditInitTimeMinutes)).getTime() < new Date(new Date(this.auditFinishDate.value).setHours(auditFinishTimeHour, auditFinishTimeMinutes)).getTime()) {
                        if (new Date(new Date(this.auditFinishDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.planCloseDate.value).setHours(0, 0, 0, 0)).getTime()) {
                            return true
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
        return false;
    }

    onSubmit() {
        if (this.type.valid) {
            if (this.validateForm()) {
                this.blockUI.start();

                let auditInitTimeHour = this.auditInitTime.value.substring(0, 2);
                let auditInitTimeMinutes = this.auditInitTime.value.substring(3, 5);

                let auditFinishTimeHour = this.auditFinishTime.value.substring(0, 2);
                let auditFinishTimeMinutes = this.auditFinishTime.value.substring(3, 5);

                this.audit.auditStandardAspect = this.auditStandardAspectSelected;
                this.audit.auditTeam = this.auditGroup.value;
                this.audit.documentsAnalysisDate = new Date(this.documentsAnalysisDate.value);
                this.audit.documentAnalysisDuration = this.documentAnalysisDuration.value;
                this.audit.auditInitDate = new Date(new Date(this.auditInitDate.value).setHours(auditInitTimeHour, auditInitTimeMinutes));
                this.audit.auditInitTime = new Date(new Date(this.auditInitDate.value).setHours(auditInitTimeHour, auditInitTimeMinutes));
                this.audit.auditFinishDate = new Date(new Date(this.auditFinishDate.value).setHours(auditFinishTimeHour, auditFinishTimeMinutes));
                this.audit.auditFinishTime = new Date(new Date(this.auditFinishDate.value).setHours(auditFinishTimeHour, auditFinishTimeMinutes));
                this.audit.closeMeetingDate = new Date(this.mettingCloseDate.value);
                this.audit.closeMeetingDuration = this.mettingCloseTime.value;
                this.audit.closeDate = new Date(this.planCloseDate.value);
                this._auditService.planning(this.audit).subscribe(res => {
                    this._toastrManager.successToastr("Exito, la auditoría se planifico correctamente")
                    this._router.navigate(['/quality/audits/list'])
                    this.blockUI.stop();
                });
            }
        }
        else {
            this._toastrManager.errorToastr("Error, ingrese todos los campos de información básica")
        }

    }

    markControlsAsTouched() {
        this.auditGroup.markAsTouched();
        this.documentsAnalysisDate.markAsTouched();
        this.documentAnalysisDuration.markAsTouched();
        this.auditInitDate.markAsTouched();
        this.auditInitTime.markAsTouched();
        this.auditFinishDate.markAsTouched();
        this.auditFinishTime.markAsTouched();
        this.mettingCloseDate.markAsTouched();
        this.mettingCloseTime.markAsTouched();
        this.planCloseDate.markAsTouched();

        this.auditGroup.markAsDirty();
        this.documentsAnalysisDate.markAsDirty();
        this.documentAnalysisDuration.markAsDirty();
        this.auditInitDate.markAsDirty();
        this.auditInitTime.markAsDirty();
        this.auditFinishDate.markAsDirty();
        this.auditFinishTime.markAsDirty();
        this.mettingCloseDate.markAsDirty();
        this.mettingCloseTime.markAsDirty();
        this.planCloseDate.markAsDirty();

        this.auditGroup.updateValueAndValidity();
        this.documentsAnalysisDate.updateValueAndValidity();
        this.documentAnalysisDuration.updateValueAndValidity();
        this.auditInitDate.updateValueAndValidity();
        this.auditInitTime.updateValueAndValidity();
        this.auditFinishDate.updateValueAndValidity();
        this.auditFinishTime.updateValueAndValidity();
        this.mettingCloseDate.updateValueAndValidity();
        this.mettingCloseTime.updateValueAndValidity();
        this.planCloseDate.updateValueAndValidity();
    }

}
