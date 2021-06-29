import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ServizioNotificheComponent } from './servizio-notifiche.component';

describe('ServizioNotificheComponent', () => {
  let component: ServizioNotificheComponent;
  let fixture: ComponentFixture<ServizioNotificheComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ServizioNotificheComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ServizioNotificheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
