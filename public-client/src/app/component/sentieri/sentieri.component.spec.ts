import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SentieriComponent } from './sentieri.component';

describe('SentieriComponent', () => {
  let component: SentieriComponent;
  let fixture: ComponentFixture<SentieriComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SentieriComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SentieriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
