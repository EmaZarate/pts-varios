import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import * as jsPDF from 'jspdf'
import * as html2canvas from 'html2canvas'
import * as html2pdf from 'html2pdf.js'

declare var $:any;

@Component({
  selector: 'app-export-pdf',
  templateUrl: './export-pdf.component.html',
  styleUrls: ['./export-pdf.component.css']
})
export class ExportPdfComponent {

  @Output() onClick = new EventEmitter<any>();
  @Input() popoverTitle:string;
  @Input() pdfHtml: string;
  @Input() idContent:string;
  @Input() paperSize:string = 'a4';
  @Input() orientation:string = 'portrait';
  @Input() unit:string = 'mm';
  @Input() fileName:string;

  constructor() {}

  onClickExport(event) {
    this.generatePDF();
  }

   generatePDF() {

    if(this.pdfHtml != null && this.pdfHtml != "")  {
      $("#contentExportPDF").append(this.pdfHtml);
      var element = document.getElementById(this.idContent);

      html2pdf(element, {
        margin: 5,
        filename:this.fileName,
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { dpi: 300 },
        jsPDF: { unit: this.unit, format: this.paperSize, orientation: this.orientation }
      });

      setTimeout(() => {
        $("#contentExportPDF").empty();
      }, 1000)
    }

  }

}
