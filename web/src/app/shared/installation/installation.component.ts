import { Component, OnInit } from '@angular/core';
import { InstallationService } from '../../services/installation.service';

@Component({
  selector: 'one-installation',
  templateUrl: './installation.component.html',
  styleUrls: ['./installation.component.sass']
})
export class InstallationComponent implements OnInit {

  constructor(
    private installationService:InstallationService
  ) { }

  ngOnInit() {
  }

  onclickInstall(){
    this.installationService.install();
  }

}
