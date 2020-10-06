import { Component, OnInit, ViewChildren, ElementRef, AfterViewInit, ViewChild, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Attachment } from 'ClientApp/app/shared/models/attachment';
import { ActivatedRoute, Router } from '@angular/router';
import { CorrectiveActionService } from "../../corrective-action.service";
import { CorrectiveAction } from "../../models/CorrectiveAction";
import { CorrectiveActionEvidence } from "../../models/CorrectiveActionEvidence";
import { ActionPlanOutput } from "../../models/actionPlanOutput";
import { FishboneService } from '../../Fishbone.service';
import { debug } from 'util';
import { CorrectiveActionFishbone } from '../corrective-action-fishbone.model';
import { UsersService } from 'ClientApp/app/core/services/users.service';
import { CorrectiveActionWorkgroupService } from '../../corrective-action-workgroup.service';
import { CorrectiveActionEvidenceService } from '../../corrective-action-evidence.service';
import { MyFile } from 'ClientApp/app/shared/components/upload-large-files/upload-large-files.component';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Subject } from 'rxjs';
import Swal from 'sweetalert2/dist/sweetalert2';


declare const $: any;

@Component({
    selector: 'app-generate-ac',
    templateUrl: './generate-ac.component.html',
    styleUrls: ['./generate-ac.component.css']
})
export class GenerateAcComponent implements OnInit, AfterViewInit, OnDestroy {

    @ViewChildren('wizard') wizard;
    @BlockUI() blockUI: NgBlockUI;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    _correctiveActionsEvidences: CorrectiveActionEvidence[] = [];

    type: FormGroup;

    correctiveAction: CorrectiveAction;
    fishboneCategories;
    users;

    fishboneSteepValid: { valid: boolean } = { valid: true };
    fishboneStepData;
    correctiveActionID: number;
    fishboneData;
    actionPlanData: ActionPlanOutput;
    _newAttachments: Array<MyFile> = [];

    constructor(
        private formBuilder: FormBuilder,
        private _route: ActivatedRoute,
        private _correctiveActionService: CorrectiveActionService,
        private _fishboneService: FishboneService,
        private _usersService: UsersService,
        private _correctiveActionWorkgroupService: CorrectiveActionWorkgroupService,
        private _correctiveActionEvidenceService: CorrectiveActionEvidenceService,
        private _toastrManager: ToastrManager,
        private _router: Router
    ) { }

    ngOnInit() {
        let res = this._route.snapshot.data.correctiveAction;
        this.correctiveAction = res;
        console.log(this.correctiveAction);

        this.fishboneCategories = this._route.snapshot.data.categories.slice(0, 6).map((el, index) => {
            return {
                id: `id${el.fishboneID}`,
                name: el.name,
                position: index > 2 ? 'bottom' : 'top'
            }
        });
        this._correctiveActionsEvidences = res.evidences.map(element => {
            element.id = element.evidenceID;
            element.name = element.fileName;
            return element;
        });

        this._usersService.getAllBySectorPlant(this.correctiveAction.sectorTreatmentID, this.correctiveAction.plantTreatmentID)
            .takeUntil(this.ngUnsubscribe)
            .subscribe((res) => {
                this.users = (res as Array<any>).map((el) => {
                    const id = el.id;
                    const value = `${el.name} ${el.surname}`;
                    return {
                        id: id,
                        value: value
                    };
                });
            })

        this.correctiveActionID = res.correctiveActionID;
        //this.createModel();
    }

    // saveData(index) {
    //     index;
    //     switch (index) {
    //         case 1:
    //             //Mateo
    //             break;
    //         case 2:
    //             this._fishboneService.editCorrectiveActionFishbone(this.fishboneData)
    //              .takeUntil(this.ngUnsubscribe)   
    //              .subscribe(() => console.log('Saved'))
    //             break;
    //         case 3:
    //             break;
    //         case 4:
    //             break;
    //     }
    // }

    // createModel() {
    //     this.type = this.formBuilder.group({
    //         // To add a validator, we must first convert the string value into an array. The first item in the array is the default value if any, then the next item in the array is the validator. Here we are adding a required validator meaning that the firstName attribute must have a value in it.
    //         //name: [null]
    //     });
    // }

    submitFishbone(data) {
        //data.valid => true if the step is valid
        this.fishboneSteepValid.valid = data.valid;
        this.fishboneData = data;
        if (!data.isFirstRequest) {
            this._fishboneService.editCorrectiveActionFishbone(data, this.correctiveActionID)
                .takeUntil(this.ngUnsubscribe)
                .subscribe(() => console.log('Saved'))
        }
    }

    invalidFishbone() {
        this._toastrManager.warningToastr("Complete la causa raíz");
    }

    submitWorkGroup(data) {
        let usersIDs = data.map(x => { return x.id });
        let users = {
            correctiveActionID: this.correctiveAction.correctiveActionID,
            usersID: usersIDs
        }
        this._correctiveActionWorkgroupService.add(users)
            .takeUntil(this.ngUnsubscribe)
            .subscribe(res => {
            })
    }

    initializeWizard() {
        //Code for the Validator
        const $validator = $('.card-wizard form').validate({
            rules: {
                name: {
                    required: false,
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

        // Wizard Initialization

        ;
        this.bootstrapWizard($validator, this.fishboneSteepValid, this)

        // $('[data-toggle="wizard-radio"]').click(function () {
        //     const wizard = $(this).closest('.card-wizard');
        //     wizard.find('[data-toggle="wizard-radio"]').removeClass('active');
        //     $(this).addClass('active');
        //     $(wizard).find('[type="radio"]').removeAttr('checked');
        //     $(this).find('[type="radio"]').attr('checked', 'true');
        // });

        $('.set-full-height').css('height', 'auto');


    }

    ngAfterViewInit() {
        //Need wait to ngFor finish to initialize the wizard
        //this.wizard.changes
        //.takeUntil(this.ngUnsubscribe)
        //.subscribe(() => this.initializeWizard());
        this.initializeWizard();

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

    bootstrapWizard($validator, fishboneSteepValid, ctx) {
        $('.card-wizard').bootstrapWizard({
            'tabClass': 'nav nav-pills',
            'nextSelector': '.btn-next',
            'previousSelector': '.btn-previous',

            onNext: function (tab, navigation, index) {
                let $next = index + 1;
                //var $valid = $('.card-wizard form').valid();

                //if $disponibleName == true the form is valid

                if (!($next == 3 && !fishboneSteepValid.valid)) {
                    // $validator.focusInvalid();
                    return true;
                }
                else {
                    ctx.invalidFishbone();
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

            onTabClick: function (tab: any, navigation: any, index: any, next: any) {
                let $current = index + 1;
                //const $valid = $('.card-wizard form').valid();
                //if $disponibleName == true the form is valid
                const $disponibleName = $('#validateExists').is(':checked');

                if (!((next == 2 || next == 3) && !fishboneSteepValid.valid)) {
                    return true;
                } else {
                    ctx.invalidFishbone();
                    return false;
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
                //ctx.saveData(index)
            }
        });
    }

    getAttachments(event): void {
        this._correctiveActionsEvidences = event;
    }

    saveEvidence() {
        this.blockUI.start();
        this.correctiveAction.Evidences = this._correctiveActionsEvidences.filter(x => x.isInsert);
        this.correctiveAction.FileNamesToDelete = this._correctiveActionsEvidences.filter(x => x.isDelete).map(y => y.name);
        this._correctiveActionEvidenceService.update(this.correctiveAction)
            .takeUntil(this.ngUnsubscribe)
            .subscribe(res => {
                this._correctiveActionsEvidences = res.evidences.map(element => {
                    element.id = element.evidenceID;
                    element.name = element.fileName;
                    return element;
                });
                this._newAttachments = new Array<MyFile>();
                this.blockUI.stop();
            });
    }

    submitDatesAndImpact(data) {
        // data.maxDateEfficiencyEvaluation = data.maxDateEfficiencyEvaluation.toISOString();
        // console.log(data);
        // this.actionPlanData = data;
        // this._correctiveActionService.edit
    }

    submitImpact(data) {
        this.correctiveAction.impact = data.impact
        this.actionPlanData = data;
        data.maxDateEfficiencyEvaluation = data.maxDateEfficiencyEvaluation;
        this._correctiveActionService.editImpact(data, this.correctiveActionID)
            .takeUntil(this.ngUnsubscribe)
            .subscribe(res => {
            });
    }

    scrollTop() {
        $(".main-panel.ps.ps--active-y").scrollTop(0);
    }

    generateAC() {
        if (this.validateSaveEvidences(this._correctiveActionsEvidences)) {
            if (this.fishboneSteepValid && this.actionPlanData.isValid) {
                if (this.checkDateTask(this.actionPlanData.tasks)) {
                    Swal.fire({
                        title: "Verifique todas las actividades de la Planificación de esta Acción Correctiva, ya que después de hacer click en el botón Generar, no podrá volver a editarla.¿Desea generar la AC?",
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonText: 'ACEPTAR',
                        cancelButtonText: 'CANCELAR',
                        buttonsStyling: true
                      }).then((result) => { 
                          if(result.value){
                            this.actionPlanData.workflowId = this.correctiveAction.workflowId;
                            this._correctiveActionService.generateAC(this.actionPlanData)
                                .takeUntil(this.ngUnsubscribe)
                                .subscribe((res) => {
                                    this._toastrManager.successToastr('La acción correctiva se ha planificado correctamente', 'Éxito');
                                    this._router.navigate(['/quality/corrective-actions/list']);
                                })
                          }});

                } else {
                    this._toastrManager.errorToastr('Revise las fechas de sus tareas', 'Error');
                }
            } else if (this.actionPlanData.impact == '' || !this.actionPlanData.impact) {
                this._toastrManager.errorToastr('Debe ingresar el análisis de impacto', 'Error');
            }
            else {
                this._toastrManager.errorToastr('Debe ingresar al menos una tarea', 'Error');
            }
        } else {
            this._toastrManager.errorToastr('Antes de Generar, debe Guardar los archivos', 'Error');
        }
    }
    acceptGenerateAC(){
        let b = 0;
        Swal.fire({
            title: "¿Esta seguro que desea generar la AC? Tenga en cuenta que luego no podra volver a modificarla.",
            type: 'warning',
            showCancelButton: true,
            confirmButtonClass: 'btn btn-success',
            cancelButtonClass: 'btn btn-danger',
            confirmButtonText: 'Aceptar',
            cancelButtonText: 'Cancelar',
            buttonsStyling: false
          }).then((result) => { 
              if(result.value){
                return true
              }
              else{
                  return false;
              }
          });

    }
    validateSaveEvidences(evidencias) {
        for (const evidence of evidencias) {
            if (evidence.isInsert) {
                return false;
            }
        }
        return true;
    }

    checkDateTask(tasks) {
        let dateNow = new Date().toISOString();

        for (const task of tasks) {
            if (task.implementationPlannedDate < dateNow)
                return false;
        }

        return true;
    }


    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
}
