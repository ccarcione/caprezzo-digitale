import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
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

  constructor(private cdr: ChangeDetectorRef,
    public bachecaService: BachecaService,
    private translate: TranslateService) {}

  ngOnInit() {  }

  openPdfViewerPopup(urlPdfImmagineCopertina: string, titolo: string) {

  }

  openMessage(message: Messaggio) {

  }
}
