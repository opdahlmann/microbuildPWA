import { Injectable } from '@angular/core';
import { SwPush } from '@angular/service-worker';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
saveditem:any
  constructor(
    private http: HttpClient,
    private _swPush :SwPush
  
  ) { 
   
  }
   subscribeToNotification(fn) {
    return   this._swPush.requestSubscription({
        serverPublicKey: environment.VAPID_PUBLIC_KEY
    })
    .then(sub =>this.sendToServer(sub).subscribe(fn) )
    .catch(err => console.error('Could not subscribe to notifications', err));
 
  }
  sendToServer(params:PushSubscription):any{
     return this.http.post(environment.API_BASE+'subscriptions', params);
  }

  unsubscribe(Id:string):any{
    return this.http.delete(environment.API_BASE+'subscriptions/delete/'+ Id);
  }
}
