import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Feedback } from './model/feedback';

const url = "api/Feedback";

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(private http: HttpClient) { }

  async save(feedback: Feedback) {
    feedback.guid = localStorage.getItem('device-guid');
    await this.http.post(url, feedback).toPromise();
  }
}
