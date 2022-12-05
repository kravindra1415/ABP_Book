import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PDFGeneratorComponent } from './pdfgenerator.component';

describe('PDFGeneratorComponent', () => {
  let component: PDFGeneratorComponent;
  let fixture: ComponentFixture<PDFGeneratorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PDFGeneratorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PDFGeneratorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
