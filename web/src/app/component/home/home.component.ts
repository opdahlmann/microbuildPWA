import { Component, OnInit } from '@angular/core';
import { InstallationService } from '../../services/installation.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'one-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  title = 'Microbuild PWA';
  constructor(
    public installationService: InstallationService,
    private notificationService: NotificationService
  ) {
    this.installationService.init();
  }

  ngOnInit() {
  }

  install() {

    this.installationService.install();
  }

  subscribeNotification() {
    this.notificationService.subscribeToNotification();
  }
}
