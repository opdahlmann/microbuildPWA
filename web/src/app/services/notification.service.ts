import { Injectable } from '@angular/core';
import { SwPush } from '@angular/service-worker';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(
    private http: HttpClient,
    private _swPush :SwPush
  
  ) { }
  subscribeToNotification() {
    this._swPush.requestSubscription({
        serverPublicKey: environment.VAPID_PUBLIC_KEY
    })
    .then(sub => this.sendToServer(sub))
    .catch(err => console.error('Could not subscribe to notifications', err));
  }
  sendToServer(params:PushSubscription) {
    this.http.post(environment.API_BASE, params).subscribe();
    console.log('success');
  }

  
}
