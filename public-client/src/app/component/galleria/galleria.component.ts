import { ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Gallery } from 'angular-gallery';
import { NgxMasonryComponent, NgxMasonryOptions } from 'ngx-masonry';
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

  public masonryOptions: NgxMasonryOptions = {
    gutter: 10,
  };

  @ViewChild('titleContainer', { static: true }) titleContainer: ElementRef;
  @ViewChild(NgxMasonryComponent) masonry: NgxMasonryComponent;

  pagination: PageInfo = new PageInfo();
  masonryImages = [];
  pageSize = 15;

  constructor(private gallery: Gallery,
    private galleriaService: GalleriaService,
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
    this.masonryImages = paginationResult.data;
    this.pagination = paginationResult.pagination;
  }

  async showMoreImages() {
    this.isLoadingMoreImage = true;
    this.pagination.currentPage++;
    const paginationResult = await this.galleriaService.getGalleria(this.pagination);
    this.masonryImages.push(...paginationResult.data);
    this.pagination = paginationResult.pagination;
  }

  itemsLoaded() {
    console.log('itemsloaded');
  }

  showGallery(index: number) {
    let images = [];
    this.masonryImages.forEach(x => images.push({ path: x }));

    const prop = {
        images,
        index
    };
    this.gallery.load(prop);
  }

  checkStopLoading()
  {
    var images = document
      .getElementById('galleria-masonryImages')
      .getElementsByTagName('img');
    var completeList = [];
    for(var i = 0; i < images.length; i++) {
      completeList.push(images[i].complete);
    }
    if( completeList.filter(x => x == false).length == 0) {
      setTimeout(() => { this.loadingService.setStatusLoadingApp(false) }, 500);
      setTimeout(() => { this.isLoadingMoreImage = false; }, 500);
    }
  }
}
