import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RaccoltaDifferenziataComponent } from './raccolta-differenziata.component';

describe('RaccoltaDifferenziataComponent', () => {
  let component: RaccoltaDifferenziataComponent;
  let fixture: ComponentFixture<RaccoltaDifferenziataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaccoltaDifferenziataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaccoltaDifferenziataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
