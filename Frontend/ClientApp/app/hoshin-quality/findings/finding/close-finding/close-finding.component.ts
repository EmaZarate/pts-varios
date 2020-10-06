import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from "@angular/core";
import { FormGroup, FormBuilder, Validators, FormControlName } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";

import { NgBlockUI, BlockUI } from "ng-block-ui";
import { ToastrManager } from "ng6-toastr-notifications";

import { CloseFindingService } from "../../close-finding.service";

import { Finding } from "../../models/Finding";

@Component({
    selector: 'app-close-finding',
    templateUrl: './close-finding.component.html',
    styleUrls: ['./close-finding.component.css']
})
export class CloseFindingComponent implements OnInit, OnDestroy {

    @BlockUI() blockUI: NgBlockUI;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    //-----------VER: texto comun
    language_idLabel: string;
    language_descriptionLabel: string;
    language_closeButton: string;
    language_backButton: string;
    //-----------VER: texto comun
    language_title: string;
    language_subtitle: string;
    language_finalComment: string;
    language_findingState: string;
    
    findingStates = [
        {value: '11', name: 'Finalizado OK'},
        {value: '12', name: 'Finalizado No OK'},
        {value: '8', name: 'Cerrado'}
    ];

    idLabel: number;
    descriptionLabel: string;

    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    closeFindingForm: FormGroup;
    get ctrFinalComment() { return this.closeFindingForm.get('ctrFinalComment'); }
    get ctrFindingState() { return this.closeFindingForm.get('ctrFindingState'); }
    
    _finding: any;

    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private fb: FormBuilder,
        private _toastrManager: ToastrManager,
        private _closeFindingService: CloseFindingService
    ) { }

    
    ngOnInit() {
        this.blockUI.start();
        //-----------VER: texto comun
        this.language_idLabel = 'Id';
        this.language_descriptionLabel = 'Descripción';
        this.language_closeButton = 'Cerrar';
        this.language_backButton = 'Volver';
        //-----------VER: texto comun
        this.language_title = 'Hallazgo';
        this.language_subtitle = 'Cerrar Hallazgo';
        this.language_finalComment = 'Comentarios finales';
        this.language_findingState = 'Estado';

        this.closeFindingForm = this.modelCreate();
        this._route.params
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res)=> {
            this._closeFindingService.get(res.id)
                  .takeUntil(this.ngUnsubscribe)
                  .subscribe((cf: any)=> {
                    this._finding = cf;
                    this.idLabel = this._finding.id;
                    this.descriptionLabel = this._finding.description;
                    this.blockUI.stop();
                  })
        });
    }

    ngOnDestroy(){
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    modelCreate() {
        debugger
        return this.fb.group({
            ctrFinalComment: ['', Validators.required],
            ctrFindingState: ['', Validators.required]
        });
    }

    onSubmit() {
        if (this.closeFindingForm.valid) {
            let find = new Finding();
            find = this._finding
            find.finalComment = this.ctrFinalComment.value;
            find.findingStateID = this.ctrFindingState.value;
            find.findingId = this._finding.id;
            find.state = this.findingStates.find(x => x.value == find.findingStateID).name;
            find.eventData = "Close";
            this.blockUI.start();
            this._closeFindingService.close(find)
            .takeUntil(this.ngUnsubscribe)
            .subscribe(() => {
                this._toastrManager.successToastr('El hallazgo se ha cerrado correctamente', 'Éxito');
                this._router.navigate(['/quality/finding']);
                this.blockUI.stop();
            });
        }
    }
}