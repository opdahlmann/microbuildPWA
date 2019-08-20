import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { Notification } from '../models/notification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(
    private http:HttpClient
  ) { }

  sendNotifcation(notification:Notification){
  this.http.post(environment.API_BASE +'notifications',notification).subscribe();
  }
  sendNotifcationByUserBasis(userNotification:Notification){
    this.http.post(environment.API_BASE +'notifications/ByUserId',userNotification).subscribe();
  }
}
