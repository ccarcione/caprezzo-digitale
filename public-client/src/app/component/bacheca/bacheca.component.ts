import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Messaggio } from '../../model/messaggio';
import { MessageService } from '../../message.service'
import { Subscription } from 'rxjs';
import { ElementRef } from '@angular/core';
import { NgxMasonryComponent } from 'ngx-masonry';
import { LoadingService } from 'src/app/layout/loading/loading.service';
import { ShellData, ShellService } from 'src/app/layout/shell/shell.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { PdfViewerComponent } from '../pdf-viewer/pdf-viewer.component';

@Component({
  selector: 'app-bacheca',
  templateUrl: './bacheca.component.html',
  styleUrls: ['./bacheca.component.css']
})
export class BachecaComponent implements OnInit, OnDestroy, ShellData {

  @ViewChild('titleContainer', { static: true }) titleContainer: ElementRef;
  
  // get reference
  @ViewChild(NgxMasonryComponent) masonry: NgxMasonryComponent;

  messages: Messaggio[];
  sub: Subscription = new Subscription();
  intervalSub: Subscription = new Subscription();

  constructor(private messageService:MessageService,
    private loadingService: LoadingService,
    public element: ElementRef,
    private ss: ShellService,
    public dialog: MatDialog) { }

  ngOnDestroy(): void {
    this.ss.unregister();
  }

  ngOnInit() {
    this.ss.register(this);
    
    this.loadingService.setStatusLoadingApp(true);
    this.sub.add(
      this.messageService.messages$.subscribe(l => {
        this.messages =l;
        this.loadingService.setStatusLoadingApp(false);
      }
    ));
  }

  openMessage(message:Messaggio) {
    message.showMore = !message.showMore;
    // after the order of items has changed
    this.masonry.layout();
  }

  itemsLoaded() {
    console.log('itemsloaded');
  }

  openPdfViewerPopup(filePath: string, descrizione: string) {
    let config = new MatDialogConfig();
    config = {
      width: '100vw',
      height:  '100vh',
      maxWidth: '100vw',
      maxHeight: '100vh',
      hasBackdrop: false,
      data: { filePath: filePath, descrizione: descrizione },
    };
    const dialogRef = this.dialog.open(PdfViewerComponent, config);
  }
}
