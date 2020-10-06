import { filter } from 'rxjs/internal/operators/filter';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';

import { NgBlockUI, BlockUI } from 'ng-block-ui';

import { RolesService } from '../../../core/services/roles.service';
import { ClaimsService } from '../../../core/services/claims.service';

import { Role } from '../../models/Role';
import { takeUntil } from 'rxjs/operators';

declare const $: any;

@Component({
  selector: 'app-detail-role',
  templateUrl: './detail-role.component.html',
  styleUrls: ['./detail-role.component.css']
})
export class DetailRoleComponent implements OnInit, OnDestroy{

  roleDetail : Role;
  allClaims = [];
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  
  @BlockUI() blockUI: NgBlockUI;


  constructor(
    private _rolesService: RolesService,
    private _claimsService: ClaimsService,
    private _route: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.roleDetail = new Role();
    this.getClaimsAndRoleData();
  }

  getClaimsAndRoleData() {
    this._claimsService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        this.allClaims = res;
        this.getRoleData();

      })
  }

  getRoleData(){
    this._rolesService.getOne(this._route.snapshot.params.id)
    .takeUntil(this.ngUnsubscribe)
    .map((res: any) => {
      this.roleDetail = res;
      this.roleDetail.claims = (res.roleClaims as Array<any>).map((res) => 
      {
        if (res.claimValue != 'findings.reassign.direct') {
          return res.claimValue
        }
      });

      this.patchClaimsChecked();
      this.blockUI.stop();
    })
    .subscribe((res) => console.log(res))
  }

  patchClaimsChecked(){
    $('.claimCheck').each((index, el) => {
      if(this.roleDetail.claims.find((claim) => claim == $(el).val()) != null){
          $(el).attr('checked', true);
      }
  })
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
