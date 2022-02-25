import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { URLS } from 'src/app/constants/constants';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private httpClient: HttpClient) { }

  ngOnInit() {
    this.httpClient.get(URLS.DOMAIN_URL + 'home').subscribe(data => data);
  }
}
