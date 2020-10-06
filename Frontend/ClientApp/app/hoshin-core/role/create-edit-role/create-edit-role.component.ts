import { map } from 'rxjs/operators';
import { Component, OnInit, AfterViewInit, ViewChildren, OnDestroy } from '@angular/core';
import { FormControl, FormGroupDirective, NgForm, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { ActivatedRoute, Router } from '@angular/router';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { RolesService } from '../../../core/services/roles.service';
import { ClaimsService } from '../../../core/services/claims.service';

import { existsRoleValidator } from '../../../shared/validators/exists-role.validator';

import { FindingClaims } from '../../models/claims/FindingClaims';
import { CoreClaims } from '../../models/claims/CoreClaims';
import { Subject } from 'rxjs';

declare const $: any;
var existsBasic;

@Component({
    selector: 'app-create-edit-role',
    templateUrl: './create-edit-role.component.html',
    styleUrls: ['./create-edit-role.component.css']
})
export class CreateEditRoleComponent implements OnInit, AfterViewInit, OnDestroy {

    @ViewChildren('wizard') wizard;

    @BlockUI() blockUI: NgBlockUI;
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    
    disponibleName: boolean;
    existsName: boolean;
    searchingName = true;
    findingClaims: FindingClaims;
    coreClaims: CoreClaims;

    isCreate: boolean = true;

    roleSelected;

    allClaims = [];

    claimsSelected = [];

    get name() { return this.type.get('name'); }
    get active() { return this.type.get('active'); }
    get basic() {return this.type.get('basic'); }

    matcher = new MyErrorStateMatcher();

    type: FormGroup;
    constructor(
        private formBuilder: FormBuilder,
        private _route: ActivatedRoute,
        private _router: Router,
        private _toastrManager: ToastrManager,
        private _rolesService: RolesService,
        private _claimsService: ClaimsService
    ) { }

    isFieldValid(form: FormGroup, field: string) {
        return !form.get(field).valid && form.get(field).touched;
    }

    displayFieldCss(form: FormGroup, field: string) {
        return {
            'has-error': this.isFieldValid(form, field),
            'has-feedback': this.isFieldValid(form, field)
        };
    }

    getRole(id) {
        this._rolesService.getOne(id)
         .takeUntil(this.ngUnsubscribe)
            .subscribe((role) => {
                this.roleSelected = role;
                this.patchRole();
                this.blockUI.stop();
            });
    }

    getClaims() {
        this._claimsService.getAll()
            .takeUntil(this.ngUnsubscribe)
            .subscribe((res: any) => {
                this.allClaims = res;
                if (this._route.snapshot.data.typeForm == 'edit') {
                    this.isCreate = false;
                    this.getRole(this._route.snapshot.params.id);
                } else {
                    this.blockUI.stop();
                }
            });

    }
    ngOnInit() {
        this.blockUI.start();
        
        this.getClaims();
        this.createModel();
        this.name.valueChanges
            .debounceTime(400)
            .distinctUntilChanged()
            .takeUntil(this.ngUnsubscribe)
            .subscribe((name) => {
                if (name.length > 3) {
                    if (!(this._route.snapshot.data.typeForm == 'edit' && name == this.roleSelected.name)) {
                        this._rolesService.checkIfExists(name)
                           .takeUntil(this.ngUnsubscribe)
                            .subscribe((res: boolean) => {
                                //If exists res = true
                                this.disponibleName = !res;
                                this.existsName = res;
                                this.searchingName = false;
                                this.resetNameValidators();
                                $('#validateExists').attr('checked', this.disponibleName);
                            })
                    } else {
                        this.name.clearValidators();
                        this.name.updateValueAndValidity();
                        this.disponibleName = true;
                        this.existsName = false;
                        this.searchingName = false;
                    }
                }
                this.blockUI.stop();
            })
    }

    changeBasic() {
        if (this.basic.value) {
            if (!(this._route.snapshot.data.typeForm == 'edit' && this.roleSelected.basic)) {
                this._rolesService.checkIfExistsBasic()
                .takeUntil(this.ngUnsubscribe)
                .subscribe(res => {
                    if (res) {
                        this._toastrManager.errorToastr('Ya existe un rol básico activo', 'Error')
                    }
                    existsBasic = res
                })
            }
            else {
                existsBasic = false
            }
        }
        else {
            existsBasic = false
        }
    }

    createModel() {
        this.type = this.formBuilder.group({
            // To add a validator, we must first convert the string value into an array. The first item in the array is the default value if any, then the next item in the array is the validator. Here we are adding a required validator meaning that the firstName attribute must have a value in it.
            name: [null, existsRoleValidator(this.existsName)],
            active: false,
            basic: false
        });
    }
    resetNameValidators() {
        this.name.clearValidators();
        this.name.setValidators(existsRoleValidator(this.existsName))
        this.name.setValidators(Validators.required);
    }

    patchRole() {
        //console.log(this.roleSelected);
        this.name.patchValue(this.roleSelected.name);
        this.active.patchValue(this.roleSelected.active);
        this.basic.patchValue(this.roleSelected.basic);
        $('.claimCheck').each((index, el) => {
            if ((this.roleSelected.roleClaims as Array<any>).find((claim) => claim.claimValue == $(el).val()) != null) {
                $(el).attr('checked', true);
                this.claimsSelected.push($(el).val());
            }
        })
    }

    initializeWizard() {
        // Code for the Validator
        const $validator = $('.card-wizard form').validate({
            rules: {
                name: {
                    required: true,
                    minlength: 3
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
                    move_distance -= 0;
                } else if ($current == total_steps || (mobile_device == true && (index % 2 == 1))) {
                    move_distance += 0;
                }

                if (mobile_device) {
                    let x: any = index / 2;
                    vertical_level = parseInt(x);
                    vertical_level =  vertical_level * 38;
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

    bootstrapWizard($validator) {
        $('.card-wizard').bootstrapWizard({
            'tabClass': 'nav nav-pills',
            'nextSelector': '.btn-next',
            'previousSelector': '.btn-previous',

            onNext: function (tab, navigation, index,) {
                var $valid = $('.card-wizard form').valid();
                $('#wizardProfile')[0].scrollIntoView(true);
                //if $disponibleName == true the form is valid
                var $disponibleName = $('#validateExists').is(':checked');
                var $existsBasic = existsBasic
                if (!$valid || !$disponibleName || $existsBasic) {
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
                const $existsBasic = existsBasic
                const $disponibleName = $('#validateExists').is(':checked');

                if (!$valid || !$disponibleName || $existsBasic) {
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
                let vertical_level = 4;

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

    checkboxClicked(ev, claim) {

        if (ev.target.checked) {
            //Addclaim
            this.claimsSelected.push(claim);
        }
        else {
            //Remove claim
            let indexClaimToDelete = this.claimsSelected.findIndex(x => x == claim);
            this.claimsSelected.splice(indexClaimToDelete, 1);
        }

        //console.log(this.claimsSelected);
    }

    onSubmit() {
        this.blockUI.start();
        let role = { name: this.name.value, active: this.active.value, basic: this.basic.value, claims: this.claimsSelected }
        if (this.isCreate) {
            this._rolesService.addRole(role)
                .takeUntil(this.ngUnsubscribe)
                .subscribe((res) => {
                    this._toastrManager.successToastr('El rol se ha creado correctamente', 'Éxito');
                    this._router.navigate(['/core/role']);
                    this.blockUI.stop();
                })
        }
        else {
            this._rolesService.updateRole(role, this._route.snapshot.params.id)
                .takeUntil(this.ngUnsubscribe)
                .subscribe((res) => {
                    this._toastrManager.successToastr('El rol se ha actualizado correctamente', 'Éxito');
                    this._router.navigate(['/core/role']);
                    this.blockUI.stop();
                })
        }

    }

    ngOnDestroy() {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
      }

}

export class MyErrorStateMatcher implements ErrorStateMatcher {
    isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
        const isSubmitted = form && form.submitted;
        return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
    }
}

