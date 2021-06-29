import { OnDestroy } from '@angular/core';
import { ElementRef } from '@angular/core';
import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { ShellData, ShellService } from 'src/app/layout/shell/shell.service';

@Component({
  selector: 'app-eventi',
  templateUrl: './eventi.component.html',
  styleUrls: ['./eventi.component.css']
})
export class EventiComponent implements OnInit, OnDestroy, ShellData {

  @ViewChild('titleContainer', { static: true }) titleContainer: ElementRef;

  constructor(private ss: ShellService) { }

  ngOnDestroy(): void {
    this.ss.unregister();
  }

  ngOnInit(): void {
    this.ss.register(this);
  }

}
