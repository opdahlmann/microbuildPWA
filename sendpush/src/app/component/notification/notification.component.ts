import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../Services/notification.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {

  constructor( private notificationService: NotificationService) { }

  ngOnInit() {
  }
 

  sendNotification(){
    this.notificationService.sendNotifcation();
  }
}
