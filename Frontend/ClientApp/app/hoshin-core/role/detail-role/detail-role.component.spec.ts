import { ComponentFixture, TestBed } from "@angular/core/testing";
import { DetailRoleComponent } from './detail-role.component';
import { ClaimsService } from '../../../core/services/claims.service';
import { RolesService } from '../../../core/services/roles.service';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { Pipe, PipeTransform, Input, Directive, NO_ERRORS_SCHEMA } from '@angular/core';


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

describe('DetailRoleComponent', () => {

    let fixture: ComponentFixture<DetailRoleComponent>;
    let ROLES;
    let CLAIMS;
    let mockClaimsService;
    let mockRolesService;
    let mockActivatedRoute;
    

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

        mockClaimsService = jasmine.createSpyObj(['getAll']);
        mockRolesService = jasmine.createSpyObj(['getOne']);
        mockActivatedRoute = {
            snapshot: { params: { id: '2' } } 
        }

        TestBed.configureTestingModule({
            declarations: [DetailRoleComponent, MockPipe, RouterLinkDirectiveStub],
            providers: [
                { provide: ClaimsService, useValue: mockClaimsService },
                { provide: RolesService, useValue: mockRolesService },
                { provide: ActivatedRoute, useValue: mockActivatedRoute }
            ],
            schemas: [NO_ERRORS_SCHEMA]
        })
        mockClaimsService.getAll.and.returnValue(of({}));
        fixture = TestBed.createComponent(DetailRoleComponent);
        // fixture.detectChanges();
    })

    it('should set role correctly when call getRoleData', () => {
        mockRolesService.getOne.and.returnValue(of(ROLES[1]));
        spyOn(fixture.componentInstance, 'patchClaimsChecked').and.returnValue(true);
        
        fixture.componentInstance.getRoleData();

        expect(fixture.componentInstance.roleDetail.id).toBe('2');
    });

    it('should set claims correctly when call getClaimsAndRoleData', () => {
        mockClaimsService.getAll.and.returnValue(of(CLAIMS));
        spyOn(fixture.componentInstance, 'getRoleData').and.returnValue(true);

        fixture.componentInstance.getClaimsAndRoleData();

        expect(fixture.componentInstance.allClaims.length).toBe(4);
    })
})