import {Component, OnInit} from '@angular/core';
import {version} from '@app/version';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  version = version;

  constructor() {
  }

  ngOnInit() {
  }

}
