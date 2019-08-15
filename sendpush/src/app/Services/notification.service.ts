import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(
    private http:HttpClient
  ) { }

  sendNotifcation(){
  this.http.post(environment.API_BASE +'notifications',"").subscribe();
  }
  sendNotifcationByUserBasis(){
    this.http.post(environment.API_BASE +'notifications/ByUserId',"").subscribe();
  }
}
