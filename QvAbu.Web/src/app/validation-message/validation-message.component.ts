import { Component, Input, OnInit } from '@angular/core';
import { ValidationMessage } from '../models/validation-message';

@Component({
  selector: 'app-validation-message',
  templateUrl: './validation-message.component.html',
  styleUrls: ['./validation-message.component.scss']
})
export class ValidationMessageComponent implements OnInit {
  @Input()
  public message: ValidationMessage;

  constructor() { }

  ngOnInit() {
  }

}
