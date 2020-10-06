import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { BlockUI, NgBlockUI } from 'ng-block-ui';

import { Company } from '../../models/Company';
import { CompanyService } from '../../../core/services/company.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-read-company',
  templateUrl: './read-company.component.html',
  styleUrls: ['./read-company.component.css']
})
export class ReadCompanyComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  editRoute: String;
  company: Company;
  srcLogo: String | ArrayBuffer;

  constructor(
    private _route: ActivatedRoute,
    private _companyService: CompanyService
  ) { }

  ngOnInit() {
    this.blockUI.start();

    this._companyService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        if(res.length > 0){
          this.company = res[0];
          this.srcLogo = this.company.logo;
          this.editRoute = '/core/company/'+ this.company.companyID +'/edit';
        }
        else{
          this.editRoute = '/core/company/new';
        }

        this.blockUI.stop();
      });
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
