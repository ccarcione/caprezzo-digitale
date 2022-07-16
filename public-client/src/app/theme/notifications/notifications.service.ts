import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private http: HttpClient) { }

  sendNotification(obj: any) {
    return this.http.post(environment.apiUrl + "/api/notification/send-notification-sample", obj);
  }

  saveSubscription(subscription: any) {
    return this.http.post(environment.apiUrl + "/api/notification/save-subscription", subscription);
  }

  deleteSubscription(subscription: any) {
    return this.http.delete(environment.apiUrl + "/api/notification/delete-subscription", { body: subscription })
  }
}
