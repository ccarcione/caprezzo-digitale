import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UfficiComponent } from './uffici.component';

describe('UfficiComponent', () => {
  let component: UfficiComponent;
  let fixture: ComponentFixture<UfficiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UfficiComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UfficiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
