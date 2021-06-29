import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MessaggioComponent } from './messaggio.component';

describe('MessaggioComponent', () => {
  let component: MessaggioComponent;
  let fixture: ComponentFixture<MessaggioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MessaggioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MessaggioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
