import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllerteComponent } from './allerte.component';

describe('AllerteComponent', () => {
  let component: AllerteComponent;
  let fixture: ComponentFixture<AllerteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllerteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllerteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
