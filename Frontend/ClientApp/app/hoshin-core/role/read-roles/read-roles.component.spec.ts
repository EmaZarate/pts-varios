import { ComponentFixture, TestBed } from "@angular/core/testing";
import { ReadRolesComponent } from './read-roles.component';
import { RolesService } from '../../../core/services/roles.service';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';

describe('ReadRolesComponent', () => {
    let fixture: ComponentFixture<ReadRolesComponent>;
    let mockRolesService;
    let mockToastrManager;
    let ROLES;

    beforeEach(() => {
        ROLES = [
            {id: 1, name: "Adminsitrador", active: true, roleClaims: [{claimValue:"1"},{claimValue: "2"}]},
            {id: 2, name: "Responsable SGC", active: true, roleClaims: [{claimValue:"1"},{claimValue: "2"}]},
            {id: 3, name: "Usuarios", active: false, roleClaims: [{claimValue:"1"},{claimValue: "2"}]},
            {id: 4, name: "Auditor", active: true, roleClaims: [{claimValue:"1"},{claimValue: "2"}]},
            {id: 5, name: "Invitado", active: false, roleClaims: [{claimValue:"1"},{claimValue: "2"}]}
        ];

        mockRolesService = jasmine.createSpyObj(['getAll', 'updateRole']);
        mockToastrManager = jasmine.createSpyObj(['successToastr']);

        TestBed.configureTestingModule({
            declarations: [ReadRolesComponent],
            providers: [
                {provide: RolesService, useValue: mockRolesService},
                {provide: ToastrManager, useValue: mockToastrManager}
            ],
            schemas: [NO_ERRORS_SCHEMA]
        });
        fixture = TestBed.createComponent(ReadRolesComponent);
    })


    it('should create one tr for each role', () => {
        mockRolesService.getAll.and.returnValue(of(ROLES));
        fixture.detectChanges();

        expect(fixture.debugElement.queryAll(By.css('tr')).length).toBe(6);
    })

    describe('updateRoleActive', () => {
        it('should call updateRole only once', () => {
            mockRolesService.updateRole.and.returnValue(of({}));
            spyOn(fixture.componentInstance, 'loadRoles').and.returnValue(true);
            
            fixture.componentInstance.updateRoleActive(ROLES[1]);

            expect(mockRolesService.updateRole).toHaveBeenCalledTimes(1);
        })
    })

    it('should set roles correctly when call loadRoles', () => {
        mockRolesService.getAll.and.returnValue(of(ROLES));

        fixture.componentInstance.loadRoles();

        expect(fixture.componentInstance.roles.length).toBe(5);
    })

})