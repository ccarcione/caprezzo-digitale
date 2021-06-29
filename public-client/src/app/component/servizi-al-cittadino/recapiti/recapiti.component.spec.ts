import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecapitiComponent } from './recapiti.component';

describe('RecapitiComponent', () => {
  let component: RecapitiComponent;
  let fixture: ComponentFixture<RecapitiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecapitiComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecapitiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
