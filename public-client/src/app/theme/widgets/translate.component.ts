import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { SettingsService } from '@core';

@Component({
  selector: 'app-translate',
  template: `
    <button mat-icon-button class="matero-toolbar-button" [matMenuTriggerFor]="menu">
      <mat-icon>translate</mat-icon>
    </button>

    <mat-menu #menu="matMenu">
      <button mat-menu-item *ngFor="let lang of langs | keyvalue" (click)="useLanguage(lang.key)"
        style="display: flex;
              flex-direction: row;
              align-items: center;">
        <img [src]="getImageUrl(lang.key)" style="width: 24px; margin-right: 10px;">
        <span>{{ lang.value }}</span>
      </button>
    </mat-menu>
  `,
  styles: [],
})
export class TranslateComponent {
  langs = {
    'it-IT': 'Italiano',
    'en-US': 'English',
    'es-ES': 'Español',
    'de-DE': 'Deutsch',
    'fr-FR': 'Français',
  };

  constructor(private translate: TranslateService, private settings: SettingsService) {
    translate.addLangs(['it-IT', 'en-US', 'es-ES', 'de-DE', 'fr-FR']);
  }

  useLanguage(language: string) {
    this.translate.use(language);
    this.settings.setLanguage(language);
  }

  getImageUrl(key: String) {
    return 'assets/images/' + key + '.svg';
  }
}
