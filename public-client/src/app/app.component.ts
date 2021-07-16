import { Component, OnInit } from '@angular/core';
import { PwaUpdateService } from './pwa-update-service.service';
import { ActiveToast, ToastrService } from 'ngx-toastr';
import { LoadingService } from './layout/loading/loading.service';
import { ConnectionService } from 'ngx-connection-service';
import { StatisticheService } from './statistiche.service';
import { AssetsService, VersionJson } from './assets.service';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  showLoadingComponent: boolean = true;
  heartbeatOption: number = 3000;
  showNextStatusNetworkToast: boolean = false;
  offlineToast: ActiveToast<any>;
  privacyPolicyDisplayed: string;
  privacyPolicyDisplayedKey: string = 'privacy-policy-displayed';
  versionJson: VersionJson = new VersionJson();
  isHandset: boolean;

  constructor(
    private loadingService: LoadingService,
    public pwaUpdateService: PwaUpdateService,
    private toastr: ToastrService,
    private connectionService: ConnectionService,
    private statisticheService: StatisticheService,
    private assetsService: AssetsService,
    private breakpointObserver: BreakpointObserver) {
      this.loadingService.isLoading$.subscribe(status =>{
        this.showLoadingComponent = status;
      });

      this.privacyPolicyDisplayed = localStorage.getItem(this.privacyPolicyDisplayedKey);

      setTimeout(() => {
        this.connectionService.monitor().subscribe(currentState => {
          if (currentState.hasNetworkConnection && currentState.hasInternetAccess) {
            if (this.showNextStatusNetworkToast)
            {
              this.toastr.clear(this.offlineToast.toastId);
              this.toastr.success("Di nuovo online");
              this.showNextStatusNetworkToast = false;
            }
          } else {
            if (!this.showNextStatusNetworkToast)
            {
              this.offlineToast = this.toastr.warning("Nessuna connessione", "", { disableTimeOut: true, tapToDismiss: false });
              this.showNextStatusNetworkToast = true;
            }
          }
        });
      }, this.heartbeatOption);

      // controlla e se non presente genera guid in local storage.
      if (!localStorage.getItem('device-guid')) {
        localStorage.setItem('device-guid', uuidv4());
      }
    }
  
  async ngOnInit(): Promise<void> {
    this.versionJson = await this.assetsService.getVersion();

    this.breakpointObserver.observe(Breakpoints.Handset).subscribe((state: BreakpointState) => {
      this.isHandset = state.matches;
    });

    this.pwaUpdateService.checkForUpdates();
    this.toastr.info('Benvenuto!');
    
    this.statisticheService.AperturaApp();
  }

  closePrivacyPolicy(): void {
    localStorage.setItem(this.privacyPolicyDisplayedKey, 'true');
    this.privacyPolicyDisplayed = 'true';
  }
}
