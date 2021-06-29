import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

export class VersionJson {
  version: string;
  suffix: string;
  displayVersionText: string;
  appName:string;
}

@Injectable({
  providedIn: 'root'
})
export class AssetsService {

  private versionJson: VersionJson;

  constructor(private http: HttpClient) { }

  async getVersion(): Promise<VersionJson> {
    if (!this.versionJson){
      const json = await this.http.get('assets/version.json', { responseType: 'text' }).toPromise();
      this.versionJson = JSON.parse(json);
      this.versionJson.displayVersionText = 'Versione '.concat(this.versionJson.version);
      if (this.versionJson.suffix)
      {
        this.versionJson.displayVersionText = this.versionJson.displayVersionText.concat(' - ', this.versionJson.suffix)
      }
    }
    return this.versionJson;
  }

  async getPrivacyPolicy(lingua: string): Promise<string> {
    try
    {
      return await this.http.get('assets/privacypolicy/privacypolicy-'+ lingua + '.html', { responseType: 'text' }).toPromise();
    } catch {
      return '';
    }
  }
}
