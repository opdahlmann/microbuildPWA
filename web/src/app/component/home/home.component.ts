import { Component, OnInit } from '@angular/core';
import { InstallationService } from '../../services/installation.service';
import { NotificationService } from '../../services/notification.service';
import { SwPush } from '@angular/service-worker';
import { Observable } from 'rxjs';
import { map, catchError, take } from 'rxjs/operators';
@Component({
  selector: 'one-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  title = 'Microbuild PWA';
 _subscription: any;
   _isSubscribed:boolean;

  constructor(
    public installationService: InstallationService,
    private notificationService: NotificationService,
    private _swPush:SwPush
  ) {
    this.installationService.init();
   

    // this._swPush.subscription
    // .pipe(take(1))
    // .subscribe(pushSubscription => {
    //   this._subscription = pushSubscription;
    //     if( this._subscription=== null){
    //       this._isSubscribed =false;
    //     }
    //     else{
    //       this._isSubscribed =true;
    //     }
    //   console.log('[App] pushSubscription', pushSubscription)

    // })

  }

  ngOnInit() {
    const subscription = localStorage.getItem('subscription');
    if( subscription=== null){
            this._isSubscribed =false;
          }
          else{
            this._isSubscribed =true;
            this._subscription = subscription;
          }
  }

  install() {

    this.installationService.install();
  }

  async subscribeNotification() {
    await this.notificationService.subscribeToNotification((x => {
      if(x!=null){
        localStorage.setItem('subscription', JSON.stringify(x));
        this._isSubscribed =true;
       }
    }).bind(this));
  }

  unsubscribe(){
    this.notificationService.unsubscribe(JSON.parse(this._subscription).Id).subscribe(
      x=>{
        if(x==true){
          localStorage.removeItem('subscription');
          this._swPush.unsubscribe();
          this._isSubscribed =false;
        }
      }
    );
  }
}
