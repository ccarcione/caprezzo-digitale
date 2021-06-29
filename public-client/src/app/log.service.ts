import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

const url = 'api/Log';
@Injectable({
  providedIn: 'root'
})
export class LogService {

  constructor(private http: HttpClient) { }

  async logInformation(obj: any): Promise<void> {
    await this.http.post(`${url}/Information`, obj).toPromise();
  }

  async logDebug(obj: any): Promise<void> {
    await this.http.post(`${url}/Debug`, obj).toPromise();
  }

  async logTrace(obj: any): Promise<void> {
    await this.http.post(`${url}/Trace`, obj).toPromise();
  }

  async logCritical(obj: any): Promise<void> {
    await this.http.post(`${url}/Critical`, obj).toPromise();
  }

  async logWarning(obj: any): Promise<void> {
    await this.http.post(`${url}/Warning`, obj).toPromise();
  }

  async logError(obj: any): Promise<void> {
    await this.http.post(`${url}/Error`, obj).toPromise();
  }
}
