import { Component, OnInit } from '@angular/core';
import { InstallationService } from '../../services/installation.service';

@Component({
  selector: 'one-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  title = 'Microbuild PWA';
  constructor( public installationService: InstallationService) { }

  ngOnInit() {
  }

}
