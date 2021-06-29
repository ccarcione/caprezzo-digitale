import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { LoadingService } from 'src/app/layout/loading/loading.service';
import { VersionJson, AssetsService } from 'src/app/assets.service';
import { ShellData, ShellService } from 'src/app/layout/shell/shell.service';

@Component({
  selector: 'app-info-about',
  templateUrl: './info-about.component.html',
  styleUrls: ['./info-about.component.css']
})
export class InfoAboutComponent implements OnInit, OnDestroy, ShellData {

  @ViewChild('titleContainer', { static: true }) titleContainer: ElementRef;
  versionJson: VersionJson = new VersionJson();

  constructor(private loadingService: LoadingService,
    private assetsService: AssetsService,
    private ss: ShellService) { }

  ngOnDestroy(): void {
    this.ss.unregister();
  }

  async ngOnInit(): Promise<void> {
    this.ss.register(this);
    this.versionJson = await this.assetsService.getVersion();
    
    this.loadingService.setStatusLoadingApp(false);
  }
}
