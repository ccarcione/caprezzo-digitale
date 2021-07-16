import { ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Config, LayoutStyle, Media } from 'ng-opengallery';
import { LoadingService } from 'src/app/layout/loading/loading.service';
import { ShellData, ShellService } from 'src/app/layout/shell/shell.service';
import { PageInfo } from 'src/app/model/pagination-result';
import { GalleriaService } from '../../galleria.service';

@Component({
  selector: 'app-galleria',
  templateUrl: './galleria.component.html',
  styleUrls: ['./galleria.component.css']
})
export class GalleriaComponent implements OnInit, OnDestroy, ShellData {

  isLoadingMoreImage: boolean = false;

  @ViewChild('titleContainer', { static: true }) titleContainer: ElementRef;

  pagination: PageInfo = new PageInfo();
  imagesData = [];
  pageSize = 15;

  config: Config = {
    diaporamaDuration: 3,
    layout: LayoutStyle.SIMPLE,
    prefMediaHeight: 250,
    spacing: 2,
    viewerEnabled: true,
    viewerFullsize: true,
    enableAutoPlay: true,
    effectClass: null
  }

  constructor(private galleriaService: GalleriaService,
    private loadingService: LoadingService,
    private ss: ShellService) { }

  ngOnDestroy(): void {
    this.ss.unregister();
  }
  
  async ngOnInit() {
    this.ss.register(this);
    this.loadingService.setStatusLoadingApp(true);
    this.pagination.currentPage = 1;
    this.pagination.pageSize = this.pageSize;
    const paginationResult = await this.galleriaService.getGalleria(this.pagination);
    setTimeout(() => { this.loadingService.setStatusLoadingApp(false) }, 2000);
    this.imagesData = paginationResult.data.map(x => new Media(x, ''));
    this.pagination = paginationResult.pagination;
  }

  async showMoreImages() {
    this.isLoadingMoreImage = true;
    this.pagination.currentPage++;
    const paginationResult = await this.galleriaService.getGalleria(this.pagination);
    this.imagesData.push(...paginationResult.data.map(x => new Media(x, '')));
    this.pagination = paginationResult.pagination;
    this.isLoadingMoreImage = false;
  }

  onChange(event) {
    console.log('CHANGE event:');
    console.log(event);
  }
  
  onError(event) {
    console.log('ERROR event:');
    console.log(event);
  }
  
  onSelection(event) {
    console.log('SELECTION event:');
    console.log(event);
    this.ss.forceCloseDrawer();
  }
  
  onOpen(event) {
    console.log('OPEN event:');
    console.log(event);
    this.ss.resetBehaviorOpenProperyDrawer();
  }
}
