import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class InstallationService {
  private _installEnabledEvent: Event;
  private deferredPrompt:any;
  private _isInstallEnabled = false;
  get isInstallEnabled() {
    return this._isInstallEnabled;
  }
  set isInstallEnabled(v) {
    ; // do nothing
  }


  constructor() { }

  init(){
    window.addEventListener('beforeinstallprompt', (e)=>{
      console.log('Install enabled');
      this._isInstallEnabled = true;
      this._installEnabledEvent = e;
    });
    
    window.addEventListener('appinstalled', this._onFinishInstall);
    
  }

install(){
this._installEnabledEvent.preventDefault();
this.deferredPrompt =this._installEnabledEvent;
this.deferredPrompt.prompt();
this.deferredPrompt.userChoice
    .then((choiceResult) => {
      if (choiceResult.outcome === 'accepted') {
        console.log('User accepted installation');
      } else {
        console.log('User dismissed the installation');
      }
      this.deferredPrompt = null;
    });
  }
  private _onFinishInstall(evt: Event) {
    console.log('App installed');
  }

}
