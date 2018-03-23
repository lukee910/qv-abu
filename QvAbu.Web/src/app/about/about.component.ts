import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {
  version: string;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<string>('/assets/version.json').subscribe(value => {
      this.version = value;
    });
  }
}
