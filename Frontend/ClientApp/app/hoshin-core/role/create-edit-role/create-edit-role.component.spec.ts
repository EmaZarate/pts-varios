import { CreateEditRoleComponent } from "./create-edit-role.component";
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Input, Directive, NO_ERRORS_SCHEMA, PipeTransform, Pipe } from '@angular/core';
import { ClaimsService } from '../../../core/services/claims.service';
import { RolesService } from '../../../core/services/roles.service';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { ToastrManager } from 'ng6-toastr-notifications';
import { FormBuilder, ReactiveFormsModule, FormsModule } from '@angular/forms';

@Pipe({name: 'translateClaims'})
class MockPipe implements PipeTransform {
    transform(value: number): number {
        //Do stuff here, if you want
        return value;
    }
}

@Directive({
    selector: '[routerLink]',
    host: { '(click)': 'onClick()' }
})
export class RouterLinkDirectiveStub {
    @Input('routerLink') linkParams: any;
    navigatedTo: any = null;

    onClick() {
        this.navigatedTo = this.linkParams;
    }
}


describe('CreateEditRoleComponent', () => {

    let fixture: ComponentFixture<CreateEditRoleComponent>;
    let ROLES;
    let CLAIMS;
    let mockClaimsService;
    let mockRolesService;
    let mockActivatedRoute;
    let mockRouter;
    let mockFormBuilder;
    let mockToastrManager;

    beforeEach(() => {

        ROLES = [
            {id: '1', name: "Adminsitrador", active: true, roleClaims: [{claimValue:"1"},{claimValue: "2"}]},
            {id: '2', name: "Responsable SGC", active: true, roleClaims: [{claimValue:"1"},{claimValue: "2"}]},
            {id: '3', name: "Usuarios", active: false, roleClaims: [{claimValue:"1"},{claimValue: "2"}]},
            {id: '4', name: "Auditor", active: true, roleClaims: [{claimValue:"1"},{claimValue: "2"}]},
            {id: '5', name: "Invitado", active: false, roleClaims: [{claimValue:"1"},{claimValue: "2"}]}
        ];

        CLAIMS = [
            {id: 1, name: "Claim1"},
            {id: 2, name: "Claim2"},
            {id: 3, name: "Claim3"},
            {id: 4, name: "Claim4"},
        ]
        mockToastrManager = jasmine.createSpyObj(['successToastr']);
        mockClaimsService = jasmine.createSpyObj(['getAll']);
        mockRolesService = jasmine.createSpyObj(['getOne', 'addRole', 'updateRole']);
        mockActivatedRoute = {
            snapshot: { params: { id: '2' }, data: {typeForm: 'edit'} } 
        };
        mockRouter = jasmine.createSpyObj(['navigate']);
        mockFormBuilder = jasmine.createSpyObj(['group']);

        TestBed.configureTestingModule({
            imports: [FormsModule, ReactiveFormsModule],
            declarations: [CreateEditRoleComponent, RouterLinkDirectiveStub, MockPipe],
            providers: [
                { provide: ClaimsService, useValue: mockClaimsService },
                { provide: RolesService, useValue: mockRolesService },
                { provide: ActivatedRoute, useValue: mockActivatedRoute },
                { provide: ToastrManager, useValue: mockToastrManager },
                { provide: Router, useValue: mockRouter },
                
                // { provide: FormBuilder, useValue: mockFormBuilder }
            ],
            schemas: [NO_ERRORS_SCHEMA]
        })
        mockClaimsService.getAll.and.returnValue(of({}));
    
        // fixture.detectChanges();
    });

    it('should set claims when call getClaims', () => {
        fixture = TestBed.createComponent(CreateEditRoleComponent);
        mockClaimsService.getAll.and.returnValue(of(CLAIMS));
        spyOn(fixture.componentInstance, 'getRole').and.returnValue(of({}));

        fixture.componentInstance.getClaims();
    
        expect(fixture.componentInstance.allClaims.length).toBe(4);
    })

    it('should set claims and call getRole when call getClaims in edit mode', () => {
        fixture = TestBed.createComponent(CreateEditRoleComponent);
        mockClaimsService.getAll.and.returnValue(of(CLAIMS));
        spyOn(fixture.componentInstance, 'getRole').and.returnValue(of({}));

        fixture.componentInstance.getClaims();

        expect(fixture.componentInstance.getRole).toHaveBeenCalledWith('2');
    });

    it('should set claims and dont call getRole when call getClaims in create mode', () => {
        mockClaimsService.getAll.and.returnValue(of(CLAIMS));
        let mockActivatedRouteCreate = {snapshot: { params: { id: '2' }, data: {typeForm: 'create'} }};
        TestBed.overrideProvider(ActivatedRoute, {useValue:mockActivatedRouteCreate})
        fixture = TestBed.createComponent(CreateEditRoleComponent);
        spyOn(fixture.componentInstance, 'getRole').and.returnValue(of({}));

        fixture.componentInstance.getClaims();

        expect(fixture.componentInstance.getRole).toHaveBeenCalledTimes(0);
    });

    it('should call addRole with the correct Role in onSubmit when isCreate is true', () => {
        fixture = TestBed.createComponent(CreateEditRoleComponent);
        fixture.componentInstance.createModel();
        fixture.componentInstance.name.patchValue('Admin');
        fixture.componentInstance.active.patchValue(true);
        fixture.componentInstance.claimsSelected = ['Claim 1', 'Claim 2'];
        fixture.componentInstance.isCreate = true;
        mockRolesService.addRole.and.returnValue(of({}));

        fixture.componentInstance.onSubmit();

        expect(mockRolesService.addRole).toHaveBeenCalledWith({name: 'Admin', active: true, claims: ['Claim 1', 'Claim 2']});
        expect(mockRolesService.addRole).toHaveBeenCalledTimes(1);
    });

    it('should call updateRole with the correct role in onSubmit when isCreate is false', () => {
        fixture = TestBed.createComponent(CreateEditRoleComponent);
        fixture.componentInstance.createModel();
        fixture.componentInstance.name.patchValue('Admin');
        fixture.componentInstance.active.patchValue(true);
        fixture.componentInstance.claimsSelected = ['Claim 1', 'Claim 2'];
        fixture.componentInstance.isCreate = false;
        mockRolesService.updateRole.and.returnValue(of({}));

        fixture.componentInstance.onSubmit();

        expect(mockRolesService.updateRole).toHaveBeenCalledWith({name: 'Admin', active: true, claims: ['Claim 1', 'Claim 2']}, '2');
        expect(mockRolesService.updateRole).toHaveBeenCalledTimes(1); 
    })
})