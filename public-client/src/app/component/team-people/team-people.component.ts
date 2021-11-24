import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ShellData, ShellService } from 'src/app/layout/shell/shell.service';

@Component({
  selector: 'app-team-people',
  templateUrl: './team-people.component.html',
  styleUrls: ['./team-people.component.css']
})
export class TeamPeopleComponent implements OnInit, OnDestroy, ShellData {
  @ViewChild('titleContainer', { static: true }) titleContainer: ElementRef;

  constructor(private ss: ShellService) { }
  ngOnDestroy(): void {
    this.ss.unregister();
  }

  ngOnInit(): void {
    this.ss.register(this);
  }

}
