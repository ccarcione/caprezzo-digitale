import { Component, OnInit } from '@angular/core';
import { SwPush } from '@angular/service-worker';
import { environment } from '@env/environment';
import { TranslateService } from '@ngx-translate/core';
import { NotificationsService } from '@theme/notifications/notifications.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-notifications-list',
  templateUrl: './notifications-list.component.html',
  styleUrls: ['./notifications-list.component.scss']
})
export class NotificationsListComponent implements OnInit {

  subscriptionToNotificationsKey: string = "subscription-to-notifications";
  subscriptionToNotificationsValue: boolean = localStorage.getItem(this.subscriptionToNotificationsKey) === "true";
  pushSubscription: any;

  constructor(private notificationsService: NotificationsService,
    private swPush: SwPush,
    private toastr: ToastrService,
    private translate: TranslateService) {}

  ngOnInit(): void {
    this.swPush.messages.subscribe(message => console.info(message));
    if(this.subscriptionToNotificationsValue) {
      this.subscribeToNotifications();
    }
  }

  subscribeToNotifications() {
    if(!this.swPush.isEnabled) {
      console.warn("Notifications not allowed.");
      this.updateSubscriptionObject(undefined);
      return;
    }

    this.swPush.requestSubscription({
      serverPublicKey: environment.VAPID_PUBLIC_KEY
    })
    .then(sub => {
      this.updateSubscriptionObject(sub);
      console.info("requestSubscription OK.", sub);
    })
    .catch(err => {
      console.warn("Could not subscribe to notifications", err);
      this.updateSubscriptionObject(undefined);
    });
  }

  unsubscribeToNotifications() {
    if(this.pushSubscription !== undefined) {
      this.swPush.unsubscribe();
      this.notificationsService.deleteSubscription(this.pushSubscription)
        .subscribe(() => this.updateSubscriptionObject(undefined));
    }
  }

  async updateSubscriptionObject(sub?: PushSubscription) {
    try {
      if(sub === undefined) {
        this.pushSubscription = undefined;
        this.subscriptionToNotificationsValue = false;
        localStorage.setItem(this.subscriptionToNotificationsKey, "" + this.subscriptionToNotificationsValue);
      } else {
        this.pushSubscription = JSON.parse(JSON.stringify(sub));
        await this.notificationsService.saveSubscription(this.pushSubscription)
          .subscribe(s => {
            this.subscriptionToNotificationsValue = true;
            localStorage.setItem(this.subscriptionToNotificationsKey, "" + this.subscriptionToNotificationsValue);
          });
      }
    } catch (e) {
      console.error(e);
      this.unsubscribeToNotifications();
    }
  }

  sendNotification() {
    if(this.pushSubscription === undefined) {
      this.toastr.warning(this.translate.instant('notification-list.enable_notification_toast_message'));
      return;
    }
    this.notificationsService.sendNotification(this.pushSubscription).subscribe();
  }

}
