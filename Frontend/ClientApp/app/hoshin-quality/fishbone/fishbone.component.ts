import { Component, Input, AfterViewInit, Output, EventEmitter, ViewEncapsulation, OnChanges } from '@angular/core';
import * as d3 from 'd3'
import Swal from 'sweetalert2/dist/sweetalert2';

declare var $: any;

@Component({
  selector: 'fishbone-diagram',
  templateUrl: './fishbone.component.html',
  styleUrls: ['./fishbone.css'],
  encapsulation: ViewEncapsulation.None
})

export class FishboneComponent implements AfterViewInit, OnChanges {

  ngOnChanges(changes: any): void {
    if (changes.DataSource && !changes.DataSource.firstChange) {
      this.DataSource = changes.DataSource.currentValue;
      this.setFishBoneData(null);
      this.render();
    }

    if (changes.Category && !changes.Category.firstChange) {
      this.Category = changes.Category.currentValue;
      this.setFishBoneData(null);
      this.initializeDiagram();
    }
    console.log(changes.DataSource)
  }


  private Svg: any;
  private SpineInCategory: any = [];
  private LastD3SpineSelected: any = null;
  private SpineEditingSelected: any = null;
  private projection: any = undefined;
  private closest: any = undefined;
  private _fishBoneData: any = {};
  private tp0: any;
  private tp1: any;
  private bp0: any;
  private bp1: any;
  public context: any = this;

  GeneratePath: any = d3.line()
    .x(function (d: any) { return d.x })
    .y(function (d: any) { return d.y; })
    .curve(d3.curveLinear);
  @Input() ColorHover: string = "rgb(239,108,0,0.50)";
  @Input() Width: number = 900;
  @Input() Height: number = 400;
  @Input() Id: string = "fishBoneDiagram";
  @Input() InitNode: number = 300;
  @Input() LineStroke: string = "5";
  @Input() CategoryStroke: string = "rgb(37,70,137)";
  @Input() CategoryFillColor: string = "rgb(120,193,226,0.75)";
  @Input() CategoryStrokeHover: string = "rgb(239,108,0)";
  @Input() CategoryFillColorHover: string = "rgb(239,108,0,0.50)";
  @Input() BackBoneNodeStrokeColor: string = "rgb(17, 126, 189)";
  @Input() BackBoneNodeHoverFillColor: string = "rgb(255,193,7)";
  @Input() BackBoneStrokeWidth: number = 9;
  @Input() LineStrokeChildren: string = "5";
  @Input() ChildSpineTextColor: string = "rgb(113,137,255)";
  @Input() SymbolSize: number = 1000;
  @Input() MaxSpinPerCategory: number = 3;
  @Input() DataSource: any = {};
  @Input() Category: any;
  @Output() GetDataDiagram: EventEmitter<any> = new EventEmitter();
  @Input() reedOnly: boolean = false;

  constructor() {
    
    this.tp0 = [this.Width * .1, this.Height * .45];
    this.tp1 = [this.Width * .1, this.Height * .1];
    this.bp0 = [this.Width * .1, this.Height * .87];
    this.bp1 = [this.Width * .1, this.Height * .53];

  }

  ngAfterViewInit(): void {

    this.createContentDiagram();
    this.initializeDiagram();
    this.getData();
  }

  initializeDiagram() {

    this.addSymbol(this.SymbolSize);
    let categoryTop = this.createSpinCategory("top");
    let categoryBottom = this.createSpinCategory("bottom");
    this.createBackBoneNode();
    this.setFishBoneData(categoryTop.concat(categoryBottom))
    this.initializeMarker();
    this.render();
    return this;
  }

  initializeMarker() {

    this.Svg.append("defs").append("marker")
      .attr("id", "circleEnd")
      .attr("markerUnits", "strokeWidth")
      .attr("markerWidth", "5")
      .attr("markerHeight", "8")
      .attr("viewBox", "0 0 16 16")
      .attr("refX", "6")
      .attr("refY", "6")
      .attr("orient", "auto")
      .append("circle")
      .attr("stroke", "rgb(17, 126, 189)")
      .attr("stroke-width", "2")
      .attr("fill", "white")
      .attr("r", "5")
      .attr("cx", "10")
      .attr("cy", "6");

    this.Svg.append("defs").append("marker")
      .attr("id", "circleEndHover")
      .attr("markerUnits", "strokeWidth")
      .attr("markerWidth", "5")
      .attr("markerHeight", "8")
      .attr("viewBox", "0 0 16 16")
      .attr("refX", "6")
      .attr("refY", "6")
      .attr("orient", "auto")
      .append("circle")
      .attr("stroke", "rgb(17, 126, 189)")
      .attr("stroke-width", "2")
      .attr("fill", "#ffc107")
      .attr("r", "5")
      .attr("cx", "10")
      .attr("cy", "6");

    this.Svg.append("defs").append("marker")
      .attr("id", "categoryRectBottom")
      .attr("markerUnits", "userSpaceOnUse")
      .attr("markerWidth", "39") //39
      .attr("markerHeight", "140")
      .attr("viewBox", "-3 0 50 150")
      .attr("refX", "2") //40
      .attr("refY", "85")
      .attr("orient", "90")
      .append("rect")
      .attr("stroke", this.CategoryStroke)
      .attr("stroke-width", "3")
      .attr("fill", this.CategoryFillColor)
      .attr("width", "45")
      .attr("height", "150")

    this.Svg.append("defs").append("marker")
      .attr("id", "categoryRectBottomHover")
      .attr("markerUnits", "userSpaceOnUse")
      .attr("markerWidth", "39") //39
      .attr("markerHeight", "140")
      .attr("viewBox", "-3 0 50 150")
      .attr("refX", "2") //40
      .attr("refY", "85")
      .attr("orient", "90")
      .append("rect")
      .attr("stroke", this.CategoryStrokeHover)
      .attr("stroke-width", "3")
      .attr("fill", this.CategoryFillColorHover)
      .attr("width", "45")
      .attr("height", "150")


    this.Svg.append("defs").append("marker")
      .attr("id", "categoryRectTop")
      .attr("markerUnits", "userSpaceOnUse")
      .attr("markerWidth", "40")
      .attr("markerHeight", "140")
      .attr("viewBox", "-2 0 50 150")
      .attr("refX", "45")
      .attr("refY", "85")
      .attr("orient", "90")
      .append("rect")
      .attr("stroke", this.CategoryStroke)
      .attr("stroke-width", "3")
      .attr("fill", this.CategoryFillColor)
      .attr("width", "45")
      .attr("height", "150")

    this.Svg.append("defs").append("marker")
      .attr("id", "categoryRectTopHover")
      .attr("markerUnits", "userSpaceOnUse")
      .attr("markerWidth", "40")
      .attr("markerHeight", "140")
      .attr("viewBox", "-2 0 50 150")
      .attr("refX", "45")
      .attr("refY", "85")
      .attr("orient", "90")
      .append("rect")
      .attr("stroke", this.CategoryStrokeHover)
      .attr("stroke-width", "3")
      .attr("fill", this.CategoryFillColorHover)
      .attr("width", "45")
      .attr("height", "150")


    this.Svg.append("defs").append("marker")
      .attr("id", "categoryRect")
      .attr("markerUnits", "userSpaceOnUse")
      .attr("markerWidth", "60")
      .attr("markerHeight", "175")
      .attr("viewBox", "-3 0 165 165")
      .attr("refX", "155")
      .attr("refY", "-160")
      .attr("orient", "-60")
      .append("rect")
      .attr("stroke", this.CategoryStroke)
      .attr("stroke-width", "3")
      .attr("fill", this.CategoryFillColor)
      .attr("width", "160")
      .attr("height", "255")

  }

  initializeAddSpine(categoryName) {
    if(this.reedOnly) return false;
    this.projection = this.Svg.append("path")
      .attr("class", "lineSpineAdd")
      .attr("id", "addSpinePath" + categoryName);
     

    this.closest = this.Svg.append("circle")
      .attr("class", "spineCircle addSpineOk")
      .attr("id", "addSpineCircle" + categoryName)
      .attr("r", 6);
  }

  isEnableForm(isEnable: boolean) {
    
    $.grep($("#formGroupChildren input"), function (e, i) {
      if (isEnable) {
        $(e).removeAttr("readonly");
      }
      else {
        $(e).attr("readonly", true);
      }
    });
    $("#customButtonEdit").attr("checked", isEnable)
  }

  addSymbol(size: number) {
    
    this.Svg
      .append('path')
      .attr("id", "endBackBoneSymbol")
      .attr("marker-end", "url(#categoryRect)")
      .attr("transform", "translate(" + (this.Width - 250) + "," + (this.Height / 2) + ") rotate(-30)")
      .attr('d', d3.symbol().type(d3.symbolTriangle).size(size));

    this.Svg.append("text")
      .attr("fill", "orange")
      .attr("x", this.Width - 185)
      .attr("y", (this.Height / 2) + 5)
      .text("Calidad")
  }

  addSpineIntoCategory(spine, categoryId, categoryName) {
    
    if(this.reedOnly) { return false};
    var children = this.getChildrenOfCategory(categoryId);
    if (children.length >= this.MaxSpinPerCategory) {

      Swal.fire({
        // title: "No se puede agregar a esta categoría más de " + this.MaxSpinPerCategory + " espinas",
        title: "No se puede agregar a esta categoría más de tres espinas",
        buttonsStyling: false,
        confirmButtonClass: "btn btn-success",
        confirmButtonText: "Aceptar"
      }).then(result => {

        if (result.value) {
          return;
        }

      });

    }
    else {
      let options = {
        id: categoryId,
        type: "modal",
        classModal: "modal-sm",
        title: categoryName,
        isInactiveEscape: true,
        isShowCustomButton: false
      }
      this.LastD3SpineSelected = spine;
      this.isEnableForm(true);
      this.openModal(options);
    }


  }

  createSpinCategory(position: string) {
    
    var category = this.getX1X2SpineCategory(position)
    var context = this;

    for (let i = 0; i < category.categories.length; i++) {

      let contentCategory = this.Svg.append("g").attr("transform", "translate(" + 5 + "," + 1 + ")");

      contentCategory.append("path")
        .attr("d", this.GeneratePath(this.generateCoordsPath(i == 0 ? this.InitNode - 100 : category.coordX1[i - 1] - 100,
          (this.Height / 2),
          ((category.coordX2[i] - 100) * (69)) / 100,
          position == "top" ? ((this.Height * 9) / 100) : ((this.Height * (90)) / 100))))
        .attr("stroke", "black")
        .attr("stroke-width", this.LineStroke)
        .attr("id", category.categories[i].id)
        .attr("title", category.categories[i].name)
        .attr("marker-end", position == "top" ? "url(#categoryRectTop)" : "url(#categoryRectBottom)")
        .on("mouseover", function () {
          context.mouseOverPath(this);
        })
        .on("mouseout", function () {
          context.mouseOutPath(this);
        })
        .on("mousemove", function () {
          context.displaySpine(context, d3.mouse(this), context.getCoordsCategory(this.id), this.id, position);
          context.displayCircleRadius(context, d3.mouse(this));
        })
        .on("click", function () {
          let spine = d3.select("#addSpineCircle" + this.id) as any;
          if (spine._groups[0][0] != null && spine.attr("class").indexOf("addSpineOk") > -1 && d3.select("#radiusMouse").attr("stroke") == "green") {
            context.addSpineIntoCategory(d3.select("#addSpinePath" + this.id), this.id, category.categories[i].name);
            context.destroyAddSpine(this.id);
          }
        })

      contentCategory.append("text")
        .attr("fill", "black")
        .attr("x", ((category.coordX2[i] - 100) * (70)) / 100)
        .attr("y", position == "top" ? ((this.Height * 5) / 100) : ((this.Height * (94)) / 100))
        .attr("alignment-baseline", "middle")
        .attr("text-anchor", "middle")
        .attr("font-size", "12px")
        .text(function (d, j) { return category.categories[i].name })

    }

    return category.categories;
  }

  createBackBoneNode() {
    
    var categoryTop = this.getX1X2SpineCategory("top");
    var categoryBottom = this.getX1X2SpineCategory("bottom");
    var this_ = this;
    if (categoryTop.categories.length >= categoryBottom.categories.length) {

      for (let i = 0; i < categoryTop.categories.length; i++) {

        this.Svg.append("path")
          .attr("d", this.GeneratePath(this.generateCoordsPath(i == 0 ? this.InitNode - 100 : categoryTop.coordX1[i - 1] - 100,
            (this.Height / 2),
            categoryTop.coordX1[i] ? categoryTop.coordX1[i] - 100 : this.Width - 264,
            (this.Height / 2))))
          .attr("stroke", "black")
          .attr("id", "backBone" + i)
          .attr("stroke-width", this.BackBoneStrokeWidth)
          .style("cursor", "pointer");
      }

      for (let i = 0; i < categoryTop.categories.length; i++) {

        this.Svg.append("g")
          .attr("transform", "translate(" + (i == 0 ? this.InitNode - 100 : categoryTop.coordX1[i - 1] - 100) + "," + (this.Height / 2) + ")")
          .append("circle")
          .attr("stroke", this.BackBoneNodeStrokeColor)
          .attr("stroke-width", "6")
          .attr("fill", "white")
          .attr("r", "15")
          .attr("x1", (i == 0 ? this.InitNode - 100 : categoryTop.coordX1[i - 1] - 100))
          .attr("y1", (this.Height / 2))
          .attr("class", "circleNode")
          .on("mouseover", function () {
            d3.select(this).attr("fill", this_.BackBoneNodeHoverFillColor);
            d3.select(this).attr("cursor", "pointer");
          })
          .on("mouseout", function () {
            d3.select(this).attr("fill", "white");
            d3.select(this).attr("cursor", "none");
          });
      }

    }
    else {

      for (let i = 0; i < categoryBottom.categories.length; i++) {

        this.Svg.append("path")
          .attr("d", this.GeneratePath(this.generateCoordsPath(i == 0 ? this.InitNode - 100 : categoryBottom.coordX1[i - 1] - 100,
            (this.Height / 2),
            categoryBottom.coordX1[i] ? categoryBottom.coordX1[i] - 100 : this.Width - 264,
            (this.Height / 2))))
          .attr("stroke", "black")
          .attr("id", "backBone" + i)
          .attr("stroke-width", this.BackBoneStrokeWidth)
          .style("cursor", "pointer");
      }

      for (let i = 0; i < categoryBottom.categories.length; i++) {

        this.Svg.append("g")
          .attr("transform", "translate(" + (i == 0 ? this.InitNode - 100 : categoryBottom.coordX1[i - 1] - 100) + "," + (this.Height / 2) + ")")
          .append("circle")
          .attr("stroke", this.BackBoneNodeStrokeColor)
          .attr("stroke-width", "6")
          .attr("fill", "white")
          .attr("r", "15")
          .on("mouseover", function () {
            d3.select(this).attr("fill", this_.BackBoneNodeHoverFillColor);
            d3.select(this).attr("cursor", "pointer");
          })
          .on("mouseout", function () {
            d3.select(this).attr("fill", "white");
            d3.select(this).attr("cursor", "none");
          });
      }

    }
  }

  createContentDiagram() {
    
    if (!$("#" + this.Id).length) {
      var fishBoneContent = document.createElement("div");
      $(fishBoneContent).addClass("centerDiagram").attr("id", this.Id).appendTo("#contentFish");

      this.Svg = d3.select("#" + this.Id).append('svg');
      this.Svg.attr('width', this.Width);
      this.Svg.attr('height', this.Height);
    }

  }

  createCustomButtonContent(childName, categoryId, categoryName, lineTo) {

    let coords: any = this.getCoordsCategoryForSpine(childName.replace(/\s/g, ''), categoryId);
    let coordsCategory: any = this.getCoordsCategory(categoryId);
    let position: string = this.getPositionCategory(categoryId);

    let textSpine = this.Svg.append("text")
      .attr("dy", "3")
      .attr("parentCategory", categoryId)
      .attr("parentCategoryName", categoryName)
      .attr("currentChild", childName)
      .attr('font-family', 'FontAwesome')
      .attr("class", "fa removeUnderline")
      .attr("id", childName.replace(/\s/g, '') + "_customButton")
      .style("fill", this.ChildSpineTextColor)
 ;
      

    let x1 = position[0] == "top" ? ((coordsCategory.x1) - 52) : ((coordsCategory.x1) + 15)
    if (coords.x2 > x1) {
      textSpine.attr("text-anchor", "start").attr("transform", "translate(" + (parseFloat(lineTo[0]) + 20) + "," + lineTo[1] + ")")
    }
    else {
      textSpine.attr("text-anchor", "end").attr("transform", "translate(" + (parseFloat(lineTo[0]) - 20) + "," + lineTo[1] + ")")
    }

    textSpine.html(this.createCustomButton(childName)).transition()
      .duration(200).ease(d3.easeBounce);

    this.onCreateEventClick("view", childName.replace(/\s/g, ''));
  }

  onCreateEventClick(typeButton, nameSpine) {
    $(document).ready(function(){
      $('.myTip').tooltip()
    });
    $(document).off('click', "#" + typeButton + "_" + nameSpine + "")
    switch (typeButton) {
      case "view":
        $(document).on('click', "#" + typeButton + "_" + nameSpine + "", { _context: this }, this.onClickEdit);
      //   $(document).on("mouseover","#" + typeButton + "_" + nameSpine + "" ,  function () {
      //     $(document).on("mouseover","#" + typeButton + "_" + nameSpine + "").tool
      // });
        break;
      default:
        break;
    }
  }

  onClickEdit(param) {

    
    var this_: any = param;
    let options = {
      id: $(this_.currentTarget.parentElement).attr("parentCategory"),
      type: "modal",
      classModal: "modal-sm",
      title: $(this_.currentTarget.parentElement).attr("parentCategoryName"),
      isInactiveEscape: true,
      isShowCustomButton: true
    }
    

    let category = param.data._context.getCategory($(this_.currentTarget.parentElement).attr("parentCategory"));
    
    param.data._context.openModal(options);
    param.data._context.setModalEdition(category, $(this_.currentTarget.parentElement).attr("currentChild"));

    const catId = $(this_.currentTarget.parentElement).attr("parentCategory");
    const boneId = $(this_.currentTarget.parentElement).attr("currentChild").replace(/\s/g, '');
    const selector = `.children-of-${catId}#${boneId}`;

    param.data._context.LastD3SpineSelected = d3.select(selector);

    // param.data._context.LastD3SpineSelected = d3.select("#" + $(this_.currentTarget.parentElement).attr("currentChild").replace(/\s/g,''));
    param.preventDefault()

  }

  onEditCustomButtonClick(param) {
    param.data._context.isEnableForm(param.currentTarget.checked);
    let spineChildDelete = $(param.data._context.LastD3SpineSelected._groups[0][0]).next().attr("currentChild");
  }

  onDeleteCustomButtonClick(param) {
    
    let spineChildDelete = $(param.data._context.LastD3SpineSelected._groups[0][0]).attr("id");
    let spineChildName = $(param.data._context.SpineEditingSelected).attr("SpineChildName").replace(/\s/g, '');
    
    var this_ = param.data._context;

    Swal.fire({
      title: "¿Esta seguro que desea eliminar " + spineChildName + "?",
      type: 'warning',
      showCancelButton: true,
      confirmButtonClass: 'btn btn-success',
      cancelButtonClass: 'btn btn-danger',
      confirmButtonText: 'Aceptar',
      cancelButtonText: 'Cancelar',
      buttonsStyling: false
    }).then((result) => {
      if (result.value) {
        // let category = this_.getCategory($(this_.LastD3SpineSelected._groups[0][0]).siblings(`[id*=${spineChildDelete}]`).attr("parentCategory"));
        let category = this_.getCategory($(this_.LastD3SpineSelected._groups[0][0]).attr('class').split('children-of-')[1]);


        for (let i = 0; i < category.BoneSpineChild.length; i++) {
          if (category.BoneSpineChild[i].SpineChildName.replace(/\s/g, '') === spineChildDelete) {
            category.BoneSpineChild.splice(i, 1);
            this_.destroyCustomButtonContent(spineChildDelete.replace(/\s/g, ''), category.CategoryId)
            this_.SpineEditingSelected = null;
          }
        }
        $("#messageModal").modal("hide");
        this_.getData();
        this_.clearFormGroup();
      }

    });

  }

  openModal(options) {
    
    if(this.reedOnly){options.isShowCustomButton = false}
    $(document).off('click', '#buttonAcceptClick');
    $(document).off('click', '#buttonCancelClick');
    $(document).off('click', '#customButtonEdit');
    $(document).off('click', '#customButtonDelete');

    var defaultOp = {
      id: "",
      title: "",
      type: "modal",
      classModal: "",
      modalMinHeight: 0,
      isCenterModal: true,
      isInactiveEscape: false
    }

    var op = $.extend(defaultOp, options);

    var jModal = $("#messageModal");

    if (op.type == "modal") {
      $(jModal).detach();

      let messageHTML = `<div class="modal fade" id="messageModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="font: 12px Tahoma, Geneva, sans-serif; color: black;">
      <div class="modal-content">
        <div class="modal-header">                         
                <h4 style="display:none;" id="categoryId"></h4>
                <h4 class="modal-title" id="titleMessage"></h4>                 
                <div id="customButton" style="display: inline-flex;">
                      <div class="togglebutton" style="display:inline-block;">
                         <label>
                            Editar<input type="checkbox" id="customButtonEdit" checked='true'>
                            <span class="toggle"></span>
                         </label>
                      </div>
                      <button  mat-raised-button mat-min-fab id="customButtonDelete" style="display:inline-block;" class="btn btn-danger btn-round btn-fab">
                          <i class="material-icons" title="Eliminar">delete_forever</i>
                      </button>

                </div>
       
        </div>
        <div class="modal-body">
          <div id="bodyMessage" class="text-center">
            <div id="modalChildCategory" class="container-fluid">
              <div class="row">
                <div class="col-xs-12">
                  <div class="form-group" id="formGroupChildren" style="padding-left:10%">
                    <input type="text" class="form-control" id="childName" placeholder="Descripción" style="margin:10px">
                    <small class="form-text text-muted" style="text-align: left;margin-left: 10px;">¿Por qué?</small>
                    <input type="text" class="form-control" id="causeChildName-1" placeholder="Causa" style="margin:10px">
                    <small class="form-text text-muted" style="text-align: left;margin-left: 10px;">¿Por qué?</small>
                    <input type="text" class="form-control" id="causeChildName-2" placeholder="Causa" style="margin:10px">
                    <small class="form-text text-muted" style="text-align: left;margin-left: 10px;">¿Por qué?</small>
                    <input type="text" class="form-control" id="causeChildName-3" placeholder="Causa" style="margin:10px">
                    <small class="form-text text-muted" style="text-align: left;margin-left: 10px;">¿Por qué?</small>
                    <input type="text" class="form-control" id="causeChildName-4" placeholder="Causa" style="margin:10px">
                    <small class="form-text text-muted" style="text-align: left;margin-left: 10px;">¿Por qué?</small>
                    <input type="text" class="form-control" id="causeChildName-5" placeholder="Causa" style="margin:10px">
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          ${this.reedOnly ? '' : '<button mat-raised-button type="button" id="buttonAcceptClick" class="btn btn-link" title="Aceptar"> Aceptar</button>'}
          <button mat-raised-button type="button" id="buttonCancelClick" class="btn btn-danger btn-link" ${this.reedOnly ? 'title="Aceptar"' : 'title="Cancelar"'}  data-dismiss="modal"> ${this.reedOnly ? 'Aceptar' : 'Cancelar'}</button>
        </div>
      </div>
    </div>
    </div>`;

      $(messageHTML).appendTo(".main-content");

      jModal = $("#messageModal");
      jModal.find(".modal-dialog").addClass(op.classModal)
      jModal.find("#customButton").addClass(op.isShowCustomButton ? "showCustomButton" : "hideCustomButton")
      jModal.find("#titleMessage").text(op.title);
      jModal.find("#categoryId").text(op.id);



      $(document).on('click', '#buttonAcceptClick', { _context: this }, this.onClickAccept);
      $("#buttonAcceptClick").removeAttr("data-dismiss");

      $(document).on('click', '#buttonCancelClick', { _context: this }, this.onClickCancel);
      $("#buttonCancelClick").removeAttr("data-dismiss");

      $(document).on('click', '#customButtonEdit', { _context: this }, this.onEditCustomButtonClick);
      $("#customButtonEdit").removeAttr("data-dismiss");

      $(document).on('click', '#customButtonDelete', { _context: this }, this.onDeleteCustomButtonClick);
      $("#customButtonDelete").removeAttr("data-dismiss");


      if (op.modalMinHeight > 0) {
        jModal.find(".modal-body").css("min-height", (op.modalMinHeight));
      };

      jModal.modal({
        show: true,
        keyboard: false,
        backdrop: 'static'
      });
    }

    if (op.isCenterModal) {
      var self = this;
      $('.modal').on('show.bs.modal', self.centerModal);

      $(window).on("resize", function () {
        $('.modal:visible').each(self.centerModal);
      });

    }
    this.reedOnly ? $('#messageModal').css("z-index", 1061) : false;
  }

  onClickAccept(param) {

    const nameSpine = $("#childName").val();
    const catId = $("#categoryId").text();
    if (nameSpine == '' || nameSpine == null) return;
    if (!isNaN(nameSpine[0])) {
      Swal.fire({
        // title: "No se puede agregar a esta categoría más de " + this.MaxSpinPerCategory + " espinas",
        title: "El nombre de la causa no puede comenzar con un número",
        buttonsStyling: false,
        confirmButtonClass: "btn btn-success",
        confirmButtonText: "Aceptar"
      });
      return;
    }

    var characterReg = /[`~!@#$%^&*()_°¬|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
    if(characterReg.test(nameSpine)) {
     Swal.fire({
        title: "No ingrese caracteres especiales",
        buttonsStyling: false,
        confirmButtonClass: "btn btn-success",
        confirmButtonText: "Aceptar"
      });
      return;
    }

    if ($(`.children-of-${catId}#${nameSpine}`).length > 0) {
      Swal.fire({
        // title: "No se puede agregar a esta categoría más de " + this.MaxSpinPerCategory + " espinas",
        title: "Ya existe una causa con ese nombre",
        buttonsStyling: false,
        confirmButtonClass: "btn btn-success",
        confirmButtonText: "Aceptar"
      });
      return;
    }

    let causes = param.data._context.getCauses();

    if (param.data._context.SpineEditingSelected) {

      let lastChildName = param.data._context.SpineEditingSelected.SpineChildName;
      let boneSpineChild = param.data._context.getBoneSpineChildSelected($("#categoryId").text())
      boneSpineChild.SpineChildName = $("#childName").val();
      boneSpineChild.Cause = [];
      causes.forEach(element => {
        boneSpineChild.AddCause(element);
      });

      $("#messageModal").modal('hide');

      param.data._context.createPathSpine($("#categoryId").text());
      var this_ = param.data._context;
      setTimeout(() => {
        var lineTo = d3.select("#" + lastChildName.replace(/\s/g, '')).attr("d").split("L")[1].split(',');
        this_.destroyCustomButtonContent(lastChildName.replace(/\s/g, ''), $("#categoryId").text());
        this_.createCustomButtonContent($("#childName").val(), $('#categoryId').text(), $("#titleMessage").text(), lineTo);
        this_.clearFormGroup();
        this_.SpineEditingSelected = null;
        this_.getData();
      }, 200)
    }
    else {

      let category = param.data._context.getCategory($("#categoryId").text());
      let boneSpineChild = new BoneSpineChild();
      boneSpineChild.SpineChildName = $("#childName").val();
      causes.forEach(element => {
        boneSpineChild.AddCause(element);
      });

      category.AddBoneSpineChild(boneSpineChild);
      $("#messageModal").modal('hide');

      param.data._context.createPathSpine($("#categoryId").text());
      var this_ = param.data._context
      setTimeout(() => {

        boneSpineChild.AddCoords(this_.getCoordsCategoryForSpine($("#childName").val().replace(/\s/g, ''), $("#categoryId").text()));
        var lineTo = this_.LastD3SpineSelected.attr("d").split("L")[1].split(',');
        this_.createCustomButtonContent($("#childName").val(), $("#categoryId").text(), $("#titleMessage").text(), lineTo);
        this_.clearFormGroup();
        this_.getData();
      }, 200)
    }

  }

  onClickCancel(param) {
    $("#messageModal").modal("hide");
    param.data._context.clearFormGroup();
    if (param.data._context.SpineEditingSelected) param.data._context.SpineEditingSelected = null;
  }

  centerModal() {
    $(this).css('display', 'block');
    var $dialog = $(this).find(".modal-dialog");
    var offset = ($(window).height() - $dialog.height()) / 2;
    $dialog.css("margin-top", offset);
  }

  createCustomButton(childName) {
    debugger
    let shortName
    if (childName.length>10){
      shortName = childName.substr(0,10)+ "...";}
    else {
      shortName=childName;
    }

    return "<a class='myTip' href='#' id='view_" + childName.replace(/\s/g, '') + "'data-toggle='tooltip' data-trigger='hover'  title='"+childName+"'>" + shortName + "</a>";
  }

  createPathSpine(categoryName) {
    var this_ = this;
    let spineChild = this.Svg.append("path")
      .attr("class", "children-of-" + categoryName)
      .attr("d", this.LastD3SpineSelected.attr("d"))
      .on("mouseover", function () {
        this_.mouseOverCircle(this);
      })
      .on("mouseout", function () {
        this_.mouseOutCircle(this);
      })
      .style("stroke", "black")
      .style("stroke-width", this.LineStrokeChildren)


    var totalLength = spineChild.node().getTotalLength();

    let pathChild = spineChild.attr("stroke-dasharray", totalLength + " " + totalLength)
      .attr("stroke-dashoffset", totalLength)
      .attr("marker-end", "url(#circleEnd)")
      .transition().ease(d3.easeBounce)
      .duration(1000)
      .attr("stroke-dashoffset", 0)
      .attr("id", $("#childName").val().replace(/\s/g, ''))
  }

  clearFormGroup() {
    var children = $("#formGroupChildren input");
    for (let index = 0; index < children.length; index++) {
      children[index].value = "";
    }
  }

  getCategory(categoryName) {
    var category;
    $.grep(this._fishBoneData.Category, function (e, i) {
      if (e.CategoryName === categoryName || e.CategoryId === categoryName) {
        category = e;
      };
    });
    return category;
  }

  getCauses() {
    var causes = [];
    $.grep($("#formGroupChildren input"), function (e, i) {
      if (e.value != "" && e.id != "childName") {
        causes.push({ subChildren: e.id, causeChildren: e.value, index: i });
      };
    });
    return causes;
  }

  getBoneSpineChildSelected(categoryName) {
    let boneSpine;
    let category = this.getCategory(categoryName);
    var this_ = this;
    $.grep(category.BoneSpineChild, function (e, i) {
      if (e.SpineChildName === this_.SpineEditingSelected.SpineChildName) {
        boneSpine = e;
      };
    });
    return boneSpine;
  }

  getX1X2SpineCategory(position: string) {
    let arrayX1 = [];
    let arrayX2 = [];
    var categories = $.map(this.Category, (element, index) => {
      if (element.position == position) {
        return element;
      }

    });

    for (let index = 0; index < categories.length; index++) {
      arrayX2.push(!arrayX2[index - 1] ? (this.Width / categories.length) : arrayX2[index - 1] + (this.Width / categories.length));
    }

    for (let index = 0; index < categories.length - 1; index++) {
      arrayX1.push(!arrayX1[index - 1] ? ((this.Width - this.InitNode) / categories.length) + this.InitNode : arrayX1[index - 1] + ((this.Width - this.InitNode) / categories.length));
    }

    return {
      categories: categories
      , coordX1: arrayX1
      , coordX2: arrayX2
    };
  }

  getCoordsCategory(categoryName) {
    let coords = d3.select("#" + categoryName).attr("d").substring(1, d3.select("#" + categoryName).attr("d").length).split('L').map(r => {
      return r.split(',');
    })

    return {
      x1: coords[0][0],
      y1: coords[0][1],
      x2: coords[1][0],
      y2: coords[1][1]
    }
  }

  getCoordsCategoryForSpine(spineName, categoryId) {
    let coords = d3.select(`.children-of-${categoryId}#${spineName}`).attr("d").substring(1, d3.select(`.children-of-${categoryId}#${spineName}`).attr("d").length).split('L').map(r => {
      return r.split(',');
    })


    // let coords = d3.select("#" + categoryName).attr("d").substring(1, d3.select("#" + categoryName).attr("d").length).split('L').map(r => {
    //   return r.split(',');
    // })

    return {
      x1: coords[0][0],
      y1: coords[0][1],
      x2: coords[1][0],
      y2: coords[1][1]
    }
  }

  getFishBoneData() {
    return this._fishBoneData;
  }

  getData() {
    this.GetDataDiagram.next(this._fishBoneData);
  }

  generateCoordsPath(x1, y1, x2, y2) {
    return [{ "x": x1, "y": y1 }, { "x": x2, "y": y2 }];
  }

  getIntersectCircle(c1, c2) {
    var distance = Math.sqrt(
      Math.pow(c2.cx - c1.cx, 2) +
      Math.pow(c2.cy - c1.cy, 2)
    );
    if (distance < (c1.r + c2.r)) {
      return true;
    } else {
      return false;
    }
  }

  getDistanceBetweenPointAndSegmentLine(x, y, x1, y1, x2, y2) {
    var A = x - parseFloat(x1);
    var B = y - parseFloat(y1);
    var C = parseFloat(x2) - parseFloat(x1);
    var D = parseFloat(y2) - parseFloat(y1);

    var dot = A * C + B * D;
    var len_sq = C * C + D * D;
    var param = -1;
    if (len_sq != 0)
      param = dot / len_sq;

    var xx, yy;

    if (param < 0) {
      xx = x1;
      yy = parseFloat(y1);
    }
    else if (param > 1) {
      xx = parseFloat(x2);
      yy = parseFloat(y2);
    }
    else {
      xx = parseFloat(x1) + param * C;
      yy = parseFloat(y1) + param * D;
    }

    var dx = x - xx;
    var dy = y - yy;
    return Math.sqrt(dx * dx + dy * dy);
  }

  getDistanceBetweenTwoPoint(coords) {

    var xs = 0;
    var ys = 0;

    xs = coords.x2 - coords.x1;
    xs = xs * xs;

    ys = coords.y2 - coords.y1;
    ys = ys * ys;

    return Math.sqrt(xs + ys);
  }

  getPositionCategory(categoryName) {

    var position = $.map(this.Category, (element, index) => {
      if (element.id == categoryName) {
        return element.position;
      }
    });

    return position;

  }

  getChildrenOfCategory(categoryName) {
    return $("path[class^='children-of-" + categoryName + "']");
  }

  displaySpine(context, p2, coordsCategory, categoryName, position) {

    context.destroyAddSpine(categoryName);
    context.initializeAddSpine(categoryName);

    p2[0] = (p2[0] + 5)
    let p0 = context.tp0,
      p1 = context.tp1

    if (position == "bottom") {
      p0 = context.bp0;
      p1 = context.bp1;
    }

    var t = context.pointLineSegmentParameter(p2, p0, p1),
      x10 = p1[0] - p0[0],
      y10 = p1[1] - p0[1],
      p3 = []

    if (position == "top") {

      p3 = [((context.getDistanceBetweenTwoPoint(coordsCategory) / 2) + 10) <= p2[1] ? (p2[0] - 55) : (p2[0] + 55), p0[1] + t * y10];
      context.closest.attr("transform", "translate(" + p3 + ")").classed("addSpineOk", Math.abs(t - .5) < .45);
      p3 = [((context.getDistanceBetweenTwoPoint(coordsCategory) / 2) + 10) <= p2[1] ? (p2[0] - 50) : (p2[0] + 50), p0[1] + t * y10];
      context.projection.attr("d", "M" + p2 + "L" + p3)
    }
    else {
      p3 = [((context.getDistanceBetweenTwoPoint(coordsCategory) / 2) - 30) <= (p2[1] - (context.Height / 2)) ? (p2[0] - 55) : (p2[0] + 55), p0[1] + t * y10];
      context.closest.attr("transform", "translate(" + p3 + ")").classed("addSpineOk", Math.abs(t - .5) < .45);
      p3 = [((context.getDistanceBetweenTwoPoint(coordsCategory) / 2) - 30) <= (p2[1] - (context.Height / 2)) ? (p2[0] - 50) : (p2[0] + 50), p0[1] + t * y10];
      context.projection.attr("d", "M" + p2 + "L" + p3)
    }
  }

  displayCircleRadius(context, mouseCoords) {
    var circleRadius = context.Svg.append("circle")
      .attr("r", 15)
      .attr("stroke", "green")
      .attr("stroke-width", 2)
      .attr("id", "radiusMouse")
      .attr("fill", "none")
      .style("stroke-dasharray", "5 5");

    circleRadius.attr("cx", mouseCoords[0]).attr("cy", mouseCoords[1]);

    var nearby = [],
      c1 = { cx: mouseCoords[0], cy: mouseCoords[1], r: 10 };
    var lastPathChecked = "";
    context.Svg.selectAll("path").each(function (non, i, e) {
      switch (this.nodeName) {
        case "path":
          if (this.id && !context.getCategory(this.id)) {

            if (lastPathChecked != this.id) {
              lastPathChecked = this.id;
              let coords = d3.select("#" + this.id).attr("d").substring(1, d3.select("#" + this.id).attr("d").length).split('L').map(r => {
                return r.split(',');
              });

              var c2 = {
                cx: + coords[0][0],
                cy: + coords[0][1],
                r: 12
              };

              if (context.getIntersectCircle(c1, c2))
                nearby.push(this.id);
            }
          }
          break;
        default:
          break
      }
    });

    if (nearby.length > 1) {
      circleRadius.attr("stroke", "red");
    }
    else if (nearby.length && nearby[0] == lastPathChecked) {
      circleRadius.attr("stroke", "green");
    }
  }

  destroyAddSpine(categoryName) {
    d3.select("#addSpinePath" + categoryName).remove();
    d3.select("#addSpineCircle" + categoryName).remove();
    d3.select("#radiusMouse").remove();
  }

  destroyCustomButtonContent(childName, categoryId) {
    const sel = `[parentCategory=${categoryId}]#${childName}`

    d3.select(`[parentCategory=${categoryId}]#${childName}_customButton`).remove();
    // d3.select("#" + childName + "_customButton").remove();
    d3.select(`.children-of-${categoryId}#${childName}`).remove();
    $("#edit_" + childName + "").off("click");
    $("#delete_" + childName + "").off("click");

  }

  dynamicSort(property) {
    var sortOrder = 1;
    if (property[0] === "-") {
      sortOrder = -1;
      property = property.substr(1);
    }
    return function (a, b) {
      var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
      return result * sortOrder;
    }
  }

  pointLineSegmentParameter(p2, p0, p1) {
    var x10 = p1[0] - p0[0], y10 = p1[1] - p0[1],
      x20 = p2[0] - p0[0], y20 = p2[1] - p0[1];
    return (x20 * x10 + y20 * y10) / (x10 * x10 + y10 * y10);
  }

  setFishBoneData(categories) {
    this._fishBoneData = this.DataSource || {}
    if (!Object.keys(this._fishBoneData).length) {
      this._fishBoneData = new FishBoneData();
      this._fishBoneData.Category = $.map(categories, (e, i) => {
        let category = new Category();
        category.CategoryName = e.name;
        category.CategoryId = e.id
        return category;
      });
    }
  }

  mouseOverPath(path: any) {
    this.setHoverPath(path, true);
    this.setHoverCircleNode(path, true);
    this.setHoverChildren(path, true);
    this.setHoverCategoryRect(path, true);

    d3.select("#endBackBoneSymbol")
      .style("stroke", this.ColorHover)
      .style("fill", this.ColorHover);

    d3.select(path)
      .style("cursor", "pointer")
      .style('stroke', this.ColorHover)
      .style('stroke-width', this.ColorHover)
      .style("fill", this.ColorHover);
  }

  mouseOutPath(path) {
    this.setHoverPath(path, false);
    this.setHoverCircleNode(path, false);
    this.setHoverChildren(path, false);
    this.setHoverCategoryRect(path, false);

    d3.select("#endBackBoneSymbol").style('stroke', "black").style("fill", "black");

    d3.selectAll("path")
      .style("stroke", "black")
      .style('opacity', this.LineStroke);

    this.destroyAddSpine(path.id)
  }

  mouseOutCircle(circle) {
    d3.select(circle).attr("marker-end", "url(#circleEnd)");
    d3.select(circle).attr("cursor", "none");
  }

  mouseOverCircle(circle) {
    d3.select(circle).attr("marker-end", "url(#circleEndHover)");
    d3.select(circle).attr("cursor", "pointer");
  }

  setModalEdition(category, currentChild) {
    this.isEnableForm(false);
    for (let i = 0; i < category.BoneSpineChild.length; i++) {
      if (category.BoneSpineChild[i].SpineChildName == currentChild) {
        $("#childName").val(category.BoneSpineChild[i].SpineChildName);

        for (let f = 0; f < category.BoneSpineChild[i].Cause.length; f++) {
          $("#" + category.BoneSpineChild[i].Cause[f].subChildren).val(category.BoneSpineChild[i].Cause[f].causeChildren);
        }
        this.SpineEditingSelected = category.BoneSpineChild[i];
      }
    }
  }

  setHoverPath(path, isOver) {
    if (isOver) {
      var backBonePath = $("path[id^='backBone']");
      var backBoneWithDistance = [];
      let mouseCoords = d3.mouse(path);

      for (let index = 0; index < backBonePath.length; index++) {

        const element = backBonePath[index];
        let coords = this.getCoordsCategory(element.id)
        let distance = this.getDistanceBetweenPointAndSegmentLine((mouseCoords[0] + 100), mouseCoords[1], coords.x1, coords.y1, coords.x2, coords.y2);
        backBoneWithDistance.push({ minDistance: distance, elementPath: element, position: index });
      }

      var lastIndex = 0
      backBoneWithDistance.sort(this.dynamicSort("minDistance"));
      var self = this;
      $(backBoneWithDistance).each(function (i, e) {
        if (e.position >= lastIndex) {
          lastIndex = e.position;
          d3.select(e.elementPath).style("stroke", self.ColorHover);
        }
      });

    }

  }

  setHoverCircleNode(path, isOver) {
    if (isOver) {
      var circleNode = $("circle[class^='circleNode']");
      var circleNodeWithDistance = [];
      let mouseCoords = d3.mouse(path);

      for (let index = 0; index < circleNode.length; index++) {
        const element = circleNode[index];
        let distance = this.getDistanceBetweenPointAndSegmentLine((mouseCoords[0] + 100), mouseCoords[1], element.getAttribute("x1"), element.getAttribute("y1"), parseFloat(element.getAttribute("x1")) + 50, element.getAttribute("y1"));
        circleNodeWithDistance.push({ minDistance: distance, elementPath: element, position: index });
      }

      var lastIndex = 0
      circleNodeWithDistance.sort(this.dynamicSort("minDistance"));
      $(circleNodeWithDistance).each(function (i, e) {
        if (e.position >= lastIndex) {
          lastIndex = e.position;
          d3.select(e.elementPath).attr("fill", "#ffc107");
        }
      });
    }
    else {
      var circleNode = $("circle[class^='circleNode']");
      for (let index = 0; index < circleNode.length; index++) {
        const element = circleNode[index];
        d3.select(element).attr("fill", "white");
      }
    }


  }

  setHoverChildren(path, isOver) {
    if (isOver) {
      var childrenPath = this.getChildrenOfCategory(path.id);
      var this_ = this;
      $(childrenPath).each(function (i, e) {
        d3.select(e).style("stroke", this_.ColorHover);
        d3.select(e).attr("marker-end", "url(#circleEndHover)")
      });
    }
    else {
      var childrenPath = this.getChildrenOfCategory(path.id);
      $(childrenPath).each(function (i, e) {
        d3.select(e).attr("marker-end", "url(#circleEnd)")
      });

    }

  }

  setHoverCategoryRect(path, isOver) {
    let position = this.getPositionCategory(path.id);

    if (isOver) {

      if (position == "top") {
        d3.select(path).attr("marker-end", "url(#categoryRectTopHover)")
      }
      else {
        d3.select(path).attr("marker-end", "url(#categoryRectBottomHover)")
      }
    }
    else {
      if (position == "top") {
        d3.select(path).attr("marker-end", "url(#categoryRectTop)")
      }
      else {
        d3.select(path).attr("marker-end", "url(#categoryRectBottom)")
      }
    }

  }

  render() {
    let data = this.getFishBoneData();
    for (let i = 0; i < data.Category.length; i++) {
      this.renderPathSpine(data.Category[i].CategoryId, data.Category[i].CategoryName, data.Category[i].BoneSpineChild);
    }
  }

  renderPathSpine(categoryId, categoryName, bonesSpine) {
    for (let i = 0; i < bonesSpine.length; i++) {
      const element = bonesSpine[i];
      let coords = element.Coords[0];
      var this_ = this;

      let spineChild = this.Svg.append("path")
        .attr("class", "children-of-" + categoryId)
        .attr("d", this.GeneratePath(this.generateCoordsPath(coords.x1, coords.y1, coords.x2, coords.y2)))
        .on("mouseover", function () {
          this_.mouseOverCircle(this);
        })
        .on("mouseout", function () {
          this_.mouseOutCircle(this);
        })
        .style("stroke", "black")
        .style("stroke-width", this.LineStrokeChildren)

      var totalLength = spineChild.node().getTotalLength(); 

      let pathChild = spineChild.attr("stroke-dasharray", totalLength + " " + totalLength)
        .attr("stroke-dashoffset", totalLength)
        .attr("marker-end", "url(#circleEnd)")
        .transition().ease(d3.easeBounce)
        .duration(1000)
        .attr("stroke-dashoffset", 0)
        .attr("id", element.SpineChildName.replace(/\s/g, ''))

      setTimeout(() => {

        var lineTo = spineChild.attr("d").split("L")[1].split(',');
        this.createCustomButtonContent(element.SpineChildName, categoryId, categoryName, lineTo);
      }, 200);
    }
  }

}

//#region Clases

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
  public CategoryId = "";
  public CategoryName: string = "";
  public BoneSpineChild: any = [];
  public AddBoneSpineChild = function (spineChild) { this.BoneSpineChild.push(spineChild); };
}

    //#endregion
