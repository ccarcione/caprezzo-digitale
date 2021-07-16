import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

const url = "api/Statistiche";

@Injectable({
  providedIn: 'root'
})
export class StatisticheService {
  guid: string = localStorage.getItem('device-guid');

  constructor(private http: HttpClient) { }

  async AperturaApp() {
    await this.http.post(url.concat('/AperturaApp/', this.guid), null).toPromise();
  }

  async InstallazioneApp() {
    await this.http.post(url.concat('/InstallazioneApp/', this.guid), null).toPromise();
  }
}
