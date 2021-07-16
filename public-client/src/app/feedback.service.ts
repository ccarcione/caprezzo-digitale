import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Feedback } from './model/Feedback';

const url = "api/Feedback";

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  constructor(private http: HttpClient) { }

  async save(feedback: Feedback) {
    await this.http.post(url, feedback).toPromise();
  }
}
