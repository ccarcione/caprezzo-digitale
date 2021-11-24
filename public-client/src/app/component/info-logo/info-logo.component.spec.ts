import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoLogoComponent } from './info-logo.component';

describe('InfoLogoComponent', () => {
  let component: InfoLogoComponent;
  let fixture: ComponentFixture<InfoLogoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InfoLogoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InfoLogoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
