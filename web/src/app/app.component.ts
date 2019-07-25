import { Component } from '@angular/core';
import { InstallationService } from './services/installation.service';
import { NotificationService } from './services/notification.service';

@Component({
  selector: 'one-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  title = 'pwa';

  constructor(
    private installservice: InstallationService,
    private notificationService:NotificationService
  ){
    this.installservice.init();
    
  }
  ngOnInit(){

  }

  install(){
   
    this.installservice.install();
  }

  subscribeNotification(){
  this.notificationService.subscribeToNotification();
  }

}
