import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { BachecaService } from './bacheca.service';
import { Messaggio } from './models/messaggio';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DashboardComponent implements OnInit {

  messages: Messaggio[] = [];
  sub: Subscription = new Subscription();

  constructor(private cdr: ChangeDetectorRef, private bachecaService: BachecaService) {}

  ngOnInit() {
    this.sub.add(
      this.bachecaService.messages$.subscribe(l => {
        this.messages = l;
        console.info(l);
      }
    ));
  }
}
