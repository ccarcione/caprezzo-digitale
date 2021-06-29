import { ElementRef } from '@angular/core';
import { ViewChild } from '@angular/core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ShellData, ShellService } from 'src/app/layout/shell/shell.service';

@Component({
  selector: 'app-allerte',
  templateUrl: './allerte.component.html',
  styleUrls: ['./allerte.component.css']
})
export class AllerteComponent implements OnInit, OnDestroy, ShellData {

  @ViewChild('titleContainer', { static: true }) titleContainer: ElementRef;

  constructor(private ss: ShellService) { }
  
  ngOnDestroy(): void {
    this.ss.unregister();
  }
  
  async ngOnInit(): Promise<void> {
    this.ss.register(this);
  }

}
