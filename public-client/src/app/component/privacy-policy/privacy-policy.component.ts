import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { LoadingService } from 'src/app/layout/loading/loading.service';
import { AssetsService } from 'src/app/assets.service';
import { ShellData, ShellService } from 'src/app/layout/shell/shell.service';

@Component({
  selector: 'app-privacy-policy',
  templateUrl: './privacy-policy.component.html',
  styleUrls: ['./privacy-policy.component.css']
})
export class PrivacyPolicyComponent implements OnInit, OnDestroy, ShellData {

  @ViewChild('titleContainer', { static: true }) titleContainer: ElementRef;

  privacyPolicyITA: string;
  privacyPolicyENG: string;
  privacyPolicyDE: string;

  constructor(private loadingService: LoadingService,
    private assetsService: AssetsService,
    private ss: ShellService) { }
  
  ngOnDestroy(): void {
    this.ss.unregister();
  }

  async ngOnInit(): Promise<void> {
    this.ss.register(this);
    this.privacyPolicyITA = await this.assetsService.getPrivacyPolicy('ita');
    this.privacyPolicyENG = await this.assetsService.getPrivacyPolicy('eng');
    this.privacyPolicyDE = await this.assetsService.getPrivacyPolicy('de');
    this.loadingService.setStatusLoadingApp(false);
  }

  ripristinaPrivacyPolicy(): void {
    localStorage.clear();
    window.location.reload();
  }
}
