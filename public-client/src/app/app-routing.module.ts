import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AllerteComponent } from './component/allerte/allerte.component';
import { BachecaComponent } from './component/bacheca/bacheca.component';
import { ChangelogComponent } from './component/changelog/changelog.component';
import { EventiComponent } from './component/eventi/eventi.component';
import { FeedbackComponent } from './component/feedback/feedback.component';
import { GalleriaComponent } from './component/galleria/galleria.component';
import { InfoAboutComponent } from './component/info-about/info-about.component';
import { MessaggioComponent } from './component/messaggio/messaggio.component';
import { PrivacyPolicyComponent } from './component/privacy-policy/privacy-policy.component';
import { SentieriComponent } from './component/sentieri/sentieri.component';
import { NegoziEStruttureComponent } from './component/servizi-al-cittadino/negozi-e-strutture/negozi-e-strutture.component';
import { RaccoltaDifferenziataComponent } from './component/servizi-al-cittadino/raccolta-differenziata/raccolta-differenziata.component';
import { RecapitiComponent } from './component/servizi-al-cittadino/recapiti/recapiti.component';
import { TrasportoComponent } from './component/servizi-al-cittadino/trasporto/trasporto.component';
import { UfficiComponent } from './component/servizi-al-cittadino/uffici/uffici.component';
import { ServizioNotificheComponent } from './component/servizio-notifiche/servizio-notifiche.component';
import { TeamPeopleComponent } from './component/team-people/team-people.component';
import { WikiComponent } from './component/wiki/wiki.component';


const routes: Routes = [
  {
    path:'bacheca',
    component: BachecaComponent
  },
  {
    path:'message/:id',
    component: MessaggioComponent
  },
  {
    path:'galleria',
    component: GalleriaComponent
  },
  {
    path:'allerte',
    component: AllerteComponent
  },
  {
    path:'eventi',
    component: EventiComponent
  },
  {
    path:'servizio-notifiche',
    component: ServizioNotificheComponent
  },
  {
    path:'info-sentieri',
    component: SentieriComponent
  },
  {
    path: 'sentieri',
    component: SentieriComponent
  },
  {
    path: 'altro',
    component: SentieriComponent
  },
  {
    path: 'wiki',
    component: WikiComponent
  },
  {
    path: 'servizi-al-cittadino',
    children: [
      {
        path: 'uffici',
        component: UfficiComponent
      },
      {
        path: 'negozi-e-strutture',
        component: NegoziEStruttureComponent
      },
      {
        path: 'raccolta-differenziata',
        component: RaccoltaDifferenziataComponent
      },
      {
        path: 'trasporto',
        component: TrasportoComponent
      },
      {
        path: 'recapiti',
        component: RecapitiComponent
      }
    ]
  },
  {
    path: 'about',
    component: InfoAboutComponent
  },
  {
    path: 'chi-siamo',
    component: TeamPeopleComponent
  },
  {
    path: 'changelog',
    component: ChangelogComponent
  },
  {
    path: 'privacy-policy',
    component: PrivacyPolicyComponent
  },
  {
    path: 'feedback',
    component: FeedbackComponent
  },
  {
    path: '**',
    component: BachecaComponent,
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})


export class AppRoutingModule { }
