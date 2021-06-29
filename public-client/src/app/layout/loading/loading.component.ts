import { Component, OnInit } from '@angular/core';
import { ConnectionService } from 'ngx-connection-service';
import { LoadingService } from './loading.service';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.css']
})
export class LoadingComponent implements OnInit {

  showOfflineContainer: boolean = false;
  hasNetworkConnection: boolean;
  hasInternetAccess: boolean;

  constructor(private loadingService: LoadingService,
    private connectionService: ConnectionService) {
      this.connectionService.monitor().subscribe(currentState => {
        this.hasNetworkConnection = currentState.hasNetworkConnection;
        this.hasInternetAccess = currentState.hasInternetAccess;
      });
    }

  ngOnInit(): void {
    this.loadingService.isLoading$.subscribe(isLoading => {
      if (isLoading) {
        setTimeout(() => {
          this.showOfflineContainer = !(this.hasInternetAccess && this.hasNetworkConnection);
        }, 5000);
      }
    });
  }

  restartApp() {
    window.location.reload();
  }
}
