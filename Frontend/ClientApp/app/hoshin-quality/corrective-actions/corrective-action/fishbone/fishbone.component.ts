import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FishboneService } from '../../Fishbone.service';
import { CorrectiveActionFishbone, CorrectiveActionFishboneCauses } from '../corrective-action-fishbone.model';
import { FormControl, Validators } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-fishbone',
  templateUrl: './fishbone.component.html',
  styleUrls: ['./fishbone.component.css']
})
export class FishboneComponent implements OnInit, OnDestroy {
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  actualFishboneData: any = {}

  @Input() correctiveActionFishbone: CorrectiveActionFishbone[];
  @Input() correctiveActionID;
  @Input() categoriesFishbone: Array<any>;
  @Input() rootReason: String;
  @Input() reedOnly: boolean = false;
  @Output() onFishboneDataUpdated = new EventEmitter<any>();

  rootReasonControl = new FormControl('', Validators.required);
  fishboneData: Array<any>;
  isFirstRequest: boolean = true;

  constructor(
    private route: ActivatedRoute,
    private fishboneService: FishboneService
  ) { }

  ngOnInit() {
    this.reedOnly ? this.rootReasonControl.disable() : false ;
    this.rootReasonControl.patchValue(this.rootReason);
    if (this.correctiveActionFishbone.length > 0) {
      this.actualFishboneData = { Category: this.mapToCategory() };
    }

    this.rootReasonControl.valueChanges
      .pipe(
        debounceTime(400),
        distinctUntilChanged(),
      )
      .takeUntil(this.ngUnsubscribe)
      .subscribe(this.emitEventDataChanges)
  }

  emitEventDataChanges = () => {
    const valid = this.validFishboneData() && this.rootReasonControl.valid;
    const data = { data: [...this.fishboneData], rootReason: this.rootReasonControl.value, valid: valid,isFirstRequest: this.isFirstRequest  };
    this.onFishboneDataUpdated.emit(data);
    if(this.isFirstRequest) { //Because the event is emitted when initialize data.
      this.isFirstRequest = false;
    };
  }

  mapToCategory() {
    return this.correctiveActionFishbone.map((element: CorrectiveActionFishbone) => {
      const cat: Category = new Category();
      cat.CategoryId = `id${element.fishboneID}`;
      cat.CorrectiveActionID = this.correctiveActionID;
      cat.CategoryName = this.categoriesFishbone
        .find(
          (x: any) => x.id == `id${element.fishboneID}`)
        .name;

      cat.BoneSpineChild = this.mapToBoneSpineChild(element.causes);

      return cat;
    })
  }

  mapToBoneSpineChild(causes: CorrectiveActionFishboneCauses[]) {
    return causes.map((element: CorrectiveActionFishboneCauses) => {
      const boneSpineChild = new BoneSpineChild();

      boneSpineChild.Cause = (element.whys as Array<any>).map((el) => {
        const causeChildren = el['description'];
        return { ...el, causeChildren }
      });
      boneSpineChild.SpineChildName = element.name
      boneSpineChild.Coords = [{
        x1: element.x1,
        x2: element.x2,
        y1: element.y1,
        y2: element.y2
      }];

      return boneSpineChild;
    })
  }

  OnGetDataDiagram(data: any): void {
    const req = data.Category.map((category: Category) => {
      const CategoryId = category.CategoryId.substring(2, category.CategoryId.length);
      return { ...category, CategoryId };
    })
    this.fishboneData = req;
    this.emitEventDataChanges();
  }

  validFishboneData() {
    var isValid: boolean = false;
    this.fishboneData.forEach((fishbone) => {
      if (fishbone.BoneSpineChild.length > 0) isValid = true;
    });

    return isValid;
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}

class BoneSpineChild {
  public SpineChildName: string = "";
  public Cause: any = []
  public Coords: any = [];
  public AddCause = function (cause) { this.Cause.push(cause); };
  public AddCoords = function (coords) { this.Coords.push(coords); };
}

class FishBoneData {
  public Category: any = []
}

class Category {
  CorrectiveActionID?: string;
  public CategoryId: string;
  public CategoryName: string = "";
  public BoneSpineChild: any = [];
  public AddBoneSpineChild = function (spineChild) { this.BoneSpineChild.push(spineChild); };
}
