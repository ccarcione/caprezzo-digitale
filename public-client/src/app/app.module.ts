import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { ShellMenuDirective } from './layout/shell/shell-menu.directive';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ShellComponent } from './layout/shell/shell.component';
import {MatButtonModule} from '@angular/material/button';
import { BachecaComponent } from './component/bacheca/bacheca.component';
import {MatListModule} from '@angular/material/list';
import {MatCardModule} from '@angular/material/card';
import { MessaggioComponent } from './component/messaggio/messaggio.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {MatExpansionModule} from '@angular/material/expansion';
import { NgxMasonryModule } from 'ngx-masonry';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { LoadingComponent } from './layout/loading/loading.component';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { GalleriaComponent } from './component/galleria/galleria.component';
import { AllerteComponent } from './component/allerte/allerte.component';
import { EventiComponent } from './component/eventi/eventi.component';
import { ServizioNotificheComponent } from './component/servizio-notifiche/servizio-notifiche.component';
import { SentieriComponent } from './component/sentieri/sentieri.component';
import { WikiComponent } from './component/wiki/wiki.component';
import { WorkInProgressComponent } from './layout/work-in-progress/work-in-progress.component';
import { ScalettaSviluppiComponent } from './component/scaletta-sviluppi/scaletta-sviluppi.component';
import { InfoAboutComponent } from './component/info-about/info-about.component';
import { RecapitiComponent } from './component/servizi-al-cittadino/recapiti/recapiti.component';
import { UfficiComponent } from './component/servizi-al-cittadino/uffici/uffici.component';
import { NegoziEStruttureComponent } from './component/servizi-al-cittadino/negozi-e-strutture/negozi-e-strutture.component';
import { RaccoltaDifferenziataComponent } from './component/servizi-al-cittadino/raccolta-differenziata/raccolta-differenziata.component';
import { TrasportoComponent } from './component/servizi-al-cittadino/trasporto/trasporto.component';
import { ConnectionServiceModule, ConnectionServiceOptions, ConnectionServiceOptionsToken } from 'ngx-connection-service';
import { FeedbackComponent } from './component/feedback/feedback.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { StarRatingModule } from 'angular-star-rating';
import { ChangelogComponent } from './component/changelog/changelog.component';
import { MatTabsModule } from '@angular/material/tabs';
import { PrivacyPolicyComponent } from './component/privacy-policy/privacy-policy.component';
import { TeamPeopleComponent } from './component/team-people/team-people.component';
import { GlobalErrorHandler } from './global-error-handler';
import { ApiKeyAuthInterceptor } from './interceptor/ApiKeyAuth.interceptor';
import { NgOpengalleryModule } from 'ng-opengallery';
import { PdfViewerComponent } from './component/pdf-viewer/pdf-viewer.component';
import { NgxExtendedPdfViewerModule } from 'ngx-extended-pdf-viewer';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    AppComponent,
    ShellComponent,
    ShellMenuDirective,
    BachecaComponent,
    MessaggioComponent,
    LoadingComponent,
    GalleriaComponent,
    AllerteComponent,
    EventiComponent,
    ServizioNotificheComponent,
    SentieriComponent,
    WikiComponent,
    WorkInProgressComponent,
    ScalettaSviluppiComponent,
    InfoAboutComponent,
    RecapitiComponent,
    UfficiComponent,
    NegoziEStruttureComponent,
    RaccoltaDifferenziataComponent,
    TrasportoComponent,
    FeedbackComponent,
    ChangelogComponent,
    PrivacyPolicyComponent,
    TeamPeopleComponent,
    PdfViewerComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSidenavModule,
    MatToolbarModule,
    AppRoutingModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatCardModule,
    MatExpansionModule,
    NgxMasonryModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    CommonModule,
    ToastrModule.forRoot({
      tapToDismiss: true,
      newestOnTop: true,
      positionClass: 'toast-top-right',
      progressBar: true,
      progressAnimation: 'decreasing',
      disableTimeOut: false
    }), // ToastrModule added
    MatSnackBarModule,
    ConnectionServiceModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    StarRatingModule.forRoot(),
    MatTabsModule,
    NgOpengalleryModule,
    NgxExtendedPdfViewerModule,
    MatDialogModule,
  ],
  providers: [
    {
      provide: ConnectionServiceOptionsToken,
      useValue: <ConnectionServiceOptions>{
        enableHeartbeat: true,
        heartbeatUrl: '/assets/ping.json',
        requestMethod: 'get',
        heartbeatInterval: 3000
      }
    },
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ApiKeyAuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
