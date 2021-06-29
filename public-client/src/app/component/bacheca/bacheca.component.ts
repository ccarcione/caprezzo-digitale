import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Messaggio } from '../../model/messaggio';
import { MessageService } from '../../message.service'
import { Subscription } from 'rxjs';
import { ElementRef } from '@angular/core';
import { NgxMasonryComponent } from 'ngx-masonry';
import { Gallery } from 'angular-gallery';
import { LoadingService } from 'src/app/layout/loading/loading.service';
import { ShellData, ShellService } from 'src/app/layout/shell/shell.service';

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
    private gallery: Gallery,
    private ss: ShellService) { }

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

  showImage(url:String) {
    let images = [];
    images.push({ path: url });
    const index = 0;
    const prop = {
      images,
      index
    };
    this.gallery.load(prop);
  }
}
