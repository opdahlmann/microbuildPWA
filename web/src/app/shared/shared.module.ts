import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InstallationComponent } from './installation/installation.component';
import { NotificationComponent } from './notification/notification.component';



@NgModule({
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  declarations: [InstallationComponent, NotificationComponent],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
