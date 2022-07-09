import { Injectable } from '@angular/core';
import { SwUpdate } from '@angular/service-worker';
import { interval } from 'rxjs';
import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class PwaUpdateService {

  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom'
  updateSubscription: any;

  constructor(public updates: SwUpdate,
    private _snackBar: MatSnackBar) {
  }

  public checkForUpdates(): void {
      this.updateSubscription = this.updates.available.subscribe(event => this.promptUser());

      if (this.updates.isEnabled) {
          // Required to enable updates on Windows and ios.
          this.updates.activateUpdate();

          this.updates.checkForUpdate().then(() => {
            console.log('checking for updates at startup');
          });
          interval(10 * 60 * 1000).subscribe(() => {
            this.updates.checkForUpdate().then(() => {
              console.log('checking for updates');
            });
          });

      }

      // Important: on Safari (ios) Heroku doesn't auto redirect links to their https which allows the installation of the pwa like usual
      // but it deactivates the swUpdate. So make sure to open your pwa on safari like so: https://example.com then (install/add to home)
  }

  promptUser(): void {
      this.updates.activateUpdate().then(() => {
          console.log('update found');
          // remove flag from localStorage
          console.log('clean ignorePwaKey');
          localStorage.removeItem('toast-ignore-install-pwa');
          this._snackBar.open('E\' stato trovato un aggiornamento.', 'Aggiorna e Riavvia', {
            horizontalPosition: this.horizontalPosition,
            verticalPosition: this.verticalPosition,
          })
          .afterDismissed().subscribe(() => {
            // Reload your application here
            console.log('restart');
            window.location.reload();
          });
      });
  }
}
