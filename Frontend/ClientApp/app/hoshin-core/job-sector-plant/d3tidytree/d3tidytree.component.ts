import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';

declare var $: any;

//if not declare, throw error at compilation
declare var window: any;

import * as d3 from "d3";
import { PlantsService } from '../../../core/services/plants.service';
import { FormGroup, FormControlName, FormBuilder, Validators } from '@angular/forms';
import { JobSectorPlantService } from '../job-sector-plant.service';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';

const dx = 25;
const dy = 200;
const width = 1200;
const margin = ({ top: 40, right: 120, bottom: 10, left: 70 });
const Id = "fishBoneDiagram";

var selectedSectors = [];
var sectorEditing;
var selectedJobs = [];
var showSectorForm = false;
var showJobForm = false;
var isFormDisabled = true;

@Component({
    selector: 'app-d3tidytree',
    templateUrl: './d3tidytree.component.html',
    styleUrls: ['./d3tidytree.component.css']
})
export class D3tidytreeComponent implements OnInit, OnDestroy {

    showTree: boolean = false;

    get sectorSelected() {
        return selectedSectors;
    }

    get isFormDisabled() {
        return isFormDisabled;
    }

    get sectorEditing() {
        return sectorEditing;
    }

    get selectedJobs() {
        return selectedJobs;
    }

    get showSectorForm() {
        return showSectorForm;
    }

    get showJobForm() {
        return showJobForm;
    }

    get plant() {
        return this.assignmentsForm.get('plant');
    }

    @BlockUI() blockUI: NgBlockUI;
    private ngUnsubscribe: Subject<void> = new Subject<void>();
    @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

    assignmentsForm: FormGroup;
    allPlants;
    Svg;

    private data = {
        name: '',
        type: '',
        id: '',
        children: null
    }

    plantSelected;

    constructor(
        private _plantsService: PlantsService,
        private fb: FormBuilder,
        private _jobSectorPlantService: JobSectorPlantService
    ) { }

    ngOnInit() {
        this.blockUI.start();
        this.assignmentsForm = this.modelCreate();
        this._plantsService.getAll()
            .takeUntil(this.ngUnsubscribe)
            .subscribe((res) => {
                this.allPlants = res;
                this.blockUI.stop();
                //console.log(res);
            });
        this.plant.valueChanges
        .takeUntil(this.ngUnsubscribe)
        .subscribe(() => {
            
            this.showTree = true;
            setTimeout(() => {
                this.createContentDiagram();
                this.chart2(this.data);
            }, 1)

        });
    }

    ngAfterContentInit() {
        this.createContentDiagram();
        this.chart2(this.data);
    }

    createContentDiagram() {
        if ($("#" + Id).length) {
            $("#" + Id).remove();
        }

        var plantTreeContent = document.createElement("div");
        $(plantTreeContent).addClass("centerDiagram").attr("id", Id).appendTo("#contentFish");

        this.Svg = d3.select("#" + Id).append('svg');

        this.Svg
            .attr("width", width)
            .attr("height", dx)
            .attr("viewBox", [-margin.left, -margin.top, width, dx])
            .style("font", "11px sans-serif")
            .style("padding","6px")
            .style("user-select", "none");
    }

    chart2(data) {
        function diagonal(p) {
            let link = d3.linkHorizontal().x(d => d.y).y(d => d.x);
            return link(p);
        }

        function generateFormSector(d) {
            selectedSectors = d.data.children;
        }

        function generateFormJobs(d) {
            let id = d.data.id == undefined ? d.data.sectorId : d.data.id
            sectorEditing = { id: id, name: d.data.name };
            selectedJobs = d.children;
        }

        function generateEdition(d) {
            switch (d.data.type) {
                case 'plant': {
                    showSectorForm = true;
                    showJobForm = false;
                    generateFormSector(d);
                    break;
                }
                case 'sector': {
                    showSectorForm = false;
                    showJobForm = true;
                    generateFormJobs(d);
                    break;
                }
                case 'job': {
                    showSectorForm = false;
                    //Do nothing, maybe hide the form
                    break;
                }
            }
        }

        const root = d3.hierarchy(data);
        root.x0 = dy / 2;
        root.y0 = 0;
        root.descendants().forEach((d, i) => {
            d.id = i;
            d._children = d.children;
            if (d.depth && d.data.name.length !== 7) d.children = null;
        });

        const gLink = this.Svg.append("g")
            .attr("fill", "none")
            .attr("stroke", "#555")
            .attr("stroke-opacity", 0.4)
            .attr("stroke-width", 1.5);

        const gNode = this.Svg.append("g")
            .attr("cursor", "pointer");

        function update(source, svgg) {
            const duration = d3.event && d3.event.altKey ? 2500 : 250;
            const nodes = root.descendants().reverse();
            const links = root.links();

            // Compute the new tree layout.
            var computeTree = d3.tree().nodeSize([dx, dy]);
            computeTree(root);
            let left = root;
            let right = root;
            root.eachBefore(node => {
                if (node.x < left.x) left = node;
                if (node.x > right.x) right = node;
            });

            const height = right.x - left.x + margin.top + margin.bottom;

            const transition = svgg.transition()
                .duration(duration)
                .attr("height", height)
                .attr("viewBox", [-margin.left, left.x - margin.top, width, height])
                .tween("resize", window.ResizeObserver ? null : () => () => svgg.dispatch("toggle"));

            // Update the nodes…
            const node = gNode.selectAll("g")
                .data(nodes, d => d.id);

            // Enter any new nodes at the parent's previous position.
            const nodeEnter = node.enter().append("g")
                .attr("transform", d => `translate(${source.y0},${source.x0})`)
                .attr("fill-opacity", 0)
                .attr("stroke-opacity", 0)
                .on("click", d => {
                    d.children = d.children ? null : d._children;
                    if (d.data.type == 'plant') {
                        d.children = d._children;
                        generateEdition(d);
                        return;
                    }
                    if (sectorEditing) {
                        if (sectorEditing.id != d.data.id) {
                            d.children = d._children
                            generateEdition(d);
                        }
                    }

                    if (d.children !== null) {
                        //Open empty or with value node
                        generateEdition(d);
                    }
                    else {
                        //close node
                        showSectorForm = false;
                        showJobForm = false;
                    }
                    update(d, svgg);
                });

            nodeEnter.append("circle")
                .attr("r", 2.5)
                .attr("fill", d => d._children ? "#555" : "#999");

            nodeEnter.append("text")
                .attr("dy", "0.31em")
                .attr("x", d => d._children ? -6 : 6)
                .attr("text-anchor", d => d._children ? "end" : "start")
                .text(d => d.data.name)
                .clone(true).lower()
                .attr("stroke-linejoin", "round")
                .attr("stroke-width", 3)
                .attr("stroke", "white");

            // Transition nodes to their new position.
            const nodeUpdate = node.merge(nodeEnter).transition(transition)
                .attr("transform", d => `translate(${d.y},${d.x})`)
                .attr("fill-opacity", 1)
                .attr("stroke-opacity", 1);

            // Transition exiting nodes to the parent's new position.
            const nodeExit = node.exit().transition(transition).remove()
                .attr("transform", d => `translate(${source.y},${source.x})`)
                .attr("fill-opacity", 0)
                .attr("stroke-opacity", 0)


            // Update the links…
            const link = gLink.selectAll("path")
                .data(links, d => d.target.id);

            // Enter any new links at the parent's previous position.
            const linkEnter = link.enter().append("path")
                .attr("d", d => {
                    const o = { x: source.x0, y: source.y0 };
                    return diagonal({ source: o, target: o });
                });

            // Transition links to their new position.
            link.merge(linkEnter).transition(transition)
                .attr("d", diagonal);

            // Transition exiting nodes to the parent's new position.
            link.exit().transition(transition).remove()
                .attr("d", d => {
                    const o = { x: source.x, y: source.y };
                    return diagonal({ source: o, target: o });
                });

            // Stash the old positions for transition.
            root.eachBefore(d => {
                d.x0 = d.x;
                d.y0 = d.y;

            });
        }

        update(root, this.Svg);

        return this.Svg.node();
    }


    modelCreate() {
        return this.fb.group({
            plant: ['', Validators.required],
        });
    }

    changeSelectedPlant(val) {

        let pl = (this.allPlants as Array<any>).find(x => x.plantID == val);
        this.plantSelected = pl;
        this.data.name = pl.name;
        this.data.type = "plant";
        this.data.id = pl.plantID;
        this.data.children = pl.sectors;
        (this.data.children as Array<any>).forEach((el) => {
            el.children = el.jobs;
            el.type = 'sector';
        });
        showJobForm = false;
        showSectorForm = false;
        //console.log(this.data);
        this.createContentDiagram();
        this.chart2(this.data);
    }

    updateTree(ev) {
        this.data.children = ev
        showSectorForm = true;
        //console.log(this.data);
        this.createContentDiagram();
        this.chart2(this.data);

    }

    updateTreeJobs(ev) {
        (this.data.children as Array<any>).find(x => x.id == ev.sector.id || x.sectorId == ev.sector.id).children = ev.jobs;
        this.createContentDiagram();
        this.chart2(this.data);

    }

    enableForms() {
        if (this.assignmentsForm.valid) {
            isFormDisabled = false;
        }
    }

    disableForms() {
        isFormDisabled = true;
        this.changeSelectedPlant(this.plant.value);
    }

    onSubmit() {
        if (this.assignmentsForm.valid) {
            this.blockUI.start();
            this._jobSectorPlantService.submitAssociation(this.data)
                .takeUntil(this.ngUnsubscribe)
                .subscribe(() => {
                    this.disableForms();
                    showJobForm = false;
                    showSectorForm = false;
                    this._plantsService.getAll()
                        .takeUntil(this.ngUnsubscribe)
                        .subscribe((res) => {
                            this.allPlants = res;
                            this.blockUI.stop();
                        });
                });

        }
    }

    ngOnDestroy(){
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
      }
}
