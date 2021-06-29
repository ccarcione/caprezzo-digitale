import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamPeopleComponent } from './team-people.component';

describe('TeamPeopleComponent', () => {
  let component: TeamPeopleComponent;
  let fixture: ComponentFixture<TeamPeopleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TeamPeopleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TeamPeopleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
