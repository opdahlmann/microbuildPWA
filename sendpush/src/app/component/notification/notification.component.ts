import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../Services/notification.service';
import { FormControl, FormGroup,FormBuilder,Validators } from '@angular/forms';
import { Notification } from '../../models/notification';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {
  notificationDetailsForm:FormGroup;
  title = 'Microbuild SendPush';
  constructor( private notificationService: NotificationService,
    private formBuilder: FormBuilder,) { 
    
  }

  ngOnInit() {
    this.notificationDetailsForm =this.formBuilder.group({
      title: ['', Validators.required],
      body: ['', Validators.required],
      image: ['https://cdn.glitch.com/132d3b19-5b84-4d9b-8f66-f8c2bd3cb6dc%2Fmicrophone-voice-interface-symbol-pngrepo-com.png?v=1559901747695'],
    })
  }
 
  get f() { return this.notificationDetailsForm.controls; }

  sendNotification(){

    let notification = new Notification();
    notification.Body = this.notificationDetailsForm.value.body;
    notification.Title =this.notificationDetailsForm.value.title;
    notification.Image =this.notificationDetailsForm.value.image;

    this.notificationService.sendNotifcation(notification);
  }
  
  sendNotificationForUserGroup(){
    let userNotification = new Notification();
    userNotification.Body = this.notificationDetailsForm.value.body;
    userNotification.Title =this.notificationDetailsForm.value.title;
    userNotification.Image =this.notificationDetailsForm.value.image;

    this.notificationService.sendNotifcationByUserBasis(userNotification);
  }
}
