import { Injectable, Injector } from '@angular/core';
import { LOCATION_INITIALIZED } from '@angular/common';
import { TranslateService } from '@ngx-translate/core';
import { SettingsService } from './settings.service';

@Injectable({
  providedIn: 'root',
})
export class TranslateLangService {
  public static userPreferenceLanguageKey: string = 'user-preference-language';

  constructor(
    private injector: Injector,
    private translate: TranslateService,
    private settings: SettingsService
  ) {}

  load() {
    return new Promise<void>(resolve => {
      const locationInitialized = this.injector.get(LOCATION_INITIALIZED, Promise.resolve());
      locationInitialized.then(() => {
        const regexLang = /it-IT|en-US|es-ES|de-DE|fr-FR/;
        const browserLang = navigator.language;
        const defaultLang = browserLang.match(regexLang) ? browserLang : 'it-IT';
        const userPreferenceLang = localStorage.getItem(TranslateLangService.userPreferenceLanguageKey) ?? ''
          .match(regexLang) ? localStorage.getItem(TranslateLangService.userPreferenceLanguageKey) : null;
        const lang = userPreferenceLang ?? defaultLang;

        this.settings.setLanguage(lang);
        this.translate.setDefaultLang(lang);
        this.translate.use(lang).subscribe(
          () => console.log(`Successfully initialized '${lang}' language.'`),
          () => console.error(`Problem with '${lang}' language initialization.'`),
          () => resolve()
        );
      });
    });
  }
}
