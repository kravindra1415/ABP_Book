import { Component, OnInit } from '@angular/core';
import jsPDF from 'jspdf';

@Component({
  selector: 'app-pdfgenerator',
  templateUrl: './pdfgenerator.component.html',
  styleUrls: ['./pdfgenerator.component.scss'],
  
})
export class PDFGeneratorComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  makepdf()
  {
    let pdf=new jsPDF();
    pdf.text("....................have been examined and found duly qualified for the diploma",10,10);
    alert("Do u want to download ?")
    pdf.save();

    
  }
}
