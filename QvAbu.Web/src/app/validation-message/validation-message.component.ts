import { Component, Input, OnInit } from '@angular/core';
import { ValidationMessage, ValidationState } from '../models/validation-message';

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

  getAlertClass(): string {
    switch (this.message.validationState) {
      case ValidationState.invalid:
        return 'danger';
      case ValidationState.notValidated:
        return 'warning';
      case ValidationState.info:
        return 'info';
      default:
        return 'success';
    }
  }
}
