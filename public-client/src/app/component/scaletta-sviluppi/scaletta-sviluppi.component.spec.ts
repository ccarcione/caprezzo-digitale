import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScalettaSviluppiComponent } from './scaletta-sviluppi.component';

describe('ScalettaSviluppiComponent', () => {
  let component: ScalettaSviluppiComponent;
  let fixture: ComponentFixture<ScalettaSviluppiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScalettaSviluppiComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScalettaSviluppiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
