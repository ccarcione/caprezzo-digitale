import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EmailFeedback } from './model/emailFeedback';

const url = "api/Email";

@Injectable({
  providedIn: 'root'
})
export class EmailService {
  

  constructor(private http: HttpClient) { }

  async sendEmail(email: EmailFeedback, saveFeedback: boolean = false) {
    await this.http.post(url, email, {
      params: new HttpParams().set('saveFeedback', String(saveFeedback))
    }).toPromise();
  }
}
