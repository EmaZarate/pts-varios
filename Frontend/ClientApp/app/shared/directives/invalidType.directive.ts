import { Directive, OnInit, Input, TemplateRef, ViewContainerRef } from "@angular/core";
import { InvalidmessageDirective } from "./invalidmessage.directive";

@Directive({
    selector: '[invalidType]'
  })
  export class InvalidTypeDirective implements OnInit{
    // tslint:disable-next-line:no-input-rename
    @Input('invalidType') type: string;
    private hasView = false;
    constructor(
      private invalidmessage: InvalidmessageDirective,
      private templateRef:TemplateRef<any>,
      private viewContainer: ViewContainerRef
    ) {}
    ngOnInit() {
     this.invalidmessage.controlValue$.subscribe(() => {
        this.setVisible();
      });
    }
  
    private setVisible() {
      if (this.invalidmessage.match(this.type)){
        if (!this.hasView) {
          this.viewContainer.createEmbeddedView(this.templateRef);
          this.hasView = true;
        }
      }else {
        if (this.hasView) {
           this.viewContainer.clear();
           this.hasView = false;
        }
      }
    }
  }
  
  