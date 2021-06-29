import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NegoziEStruttureComponent } from './negozi-e-strutture.component';

describe('NegoziEStruttureComponent', () => {
  let component: NegoziEStruttureComponent;
  let fixture: ComponentFixture<NegoziEStruttureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NegoziEStruttureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NegoziEStruttureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
