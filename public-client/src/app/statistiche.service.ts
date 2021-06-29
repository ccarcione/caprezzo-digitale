import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

const url = "api/Statistiche";

@Injectable({
  providedIn: 'root'
})
export class StatisticheService {

  constructor(private http: HttpClient) { }

  async AperturaApp() {
    await this.http.post(url.concat('/AperturaApp'), null).toPromise();
  }

  async InstallazioneApp() {
    await this.http.post(url.concat('/InstallazioneApp'), null).toPromise();
  }
}
