import { Component, OnInit } from '@angular/core';
import { AssetsService, VersionJson } from 'src/app/assets.service';

@Component({
  selector: 'app-info-logo',
  templateUrl: './info-logo.component.html',
  styleUrls: ['./info-logo.component.css']
})
export class InfoLogoComponent implements OnInit {
  versionJson: VersionJson = new VersionJson();

  constructor(private assetsService: AssetsService) { }

  async ngOnInit(): Promise<void> {
    this.versionJson = await this.assetsService.getVersion();
  }

}
