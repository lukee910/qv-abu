import { Component, Input, OnInit, Output } from '@angular/core';
import { AssignmentQuestion } from '../../models/questions/assignment-question';
import { AssignmentResponseAnswer } from '../../models/questions/response-answer';
import { AssignmentOption } from '../../models/questions/assignment-option';
import { AssignmentAnswer } from '../../models/questions/assignment-answer';
import { QuestionnaireValidationService } from '../../services/questionnaire-validation.service';
import { ValidationMessage, ValidationState } from '../../models/validation-message';

@Component({
  selector: 'app-assignment-question',
  templateUrl: './assignment-question.component.html',
  styleUrls: ['./assignment-question.component.scss']
})
export class AssignmentQuestionComponent implements OnInit {
  @Input()
  question: AssignmentQuestion;
  @Output()
  responses: AssignmentResponseAnswer[] = [];
  validationMessage: ValidationMessage = new ValidationMessage('Nicht beantwortet', ValidationState.notValidated);
  responseValues: {[ansId: string]: {[optId: string]: 'radio-true'| 'radio-false' | undefined}} = {};

  private _isValidationLocked = false;

  constructor(private validationService: QuestionnaireValidationService) {
    this.validationService.questionnaireValidationPhaseChange.subscribe(_ => {
      this._isValidationLocked = true;
    });
  }

  ngOnInit() {
    this.initResponseValues();
  }

  toChar(int: number): string {
    return String.fromCharCode(97 + int);
  }

  setResponseValue(option: AssignmentOption, answer: AssignmentAnswer): void {
    const filteredResponses = this.responses.filter(_ => _.answer.id === answer.id);
    let response = filteredResponses.length === 0 ? undefined : filteredResponses[0];
    if (!response) {
      response = new AssignmentResponseAnswer();
      response.answer = answer;
      this.responses.push(response);
    }
    response.value = option.id;
    this.validate();
  }

  validate(): void {
    this.initResponseValues();
    let invalidCount = 0;
    this.responses.forEach(response => {
      if (response.value !== response.answer.correctOptionId) {
        this.responseValues[response.answer.id.toString()][response.value.toString()] = 'radio-false';
        this.responseValues[response.answer.id.toString()][response.answer.correctOptionId.toString()] = 'radio-true';
        invalidCount++;
      }
    });

    let state = ValidationState.valid;
    let message = 'Alle Antworten richtig';
    if (invalidCount > 0) {
      state = ValidationState.invalid;
      message = invalidCount + ' Antwort' + (invalidCount > 1 ? 'en' : '') + ' falsch';
    }
    if (this.responses.length < this.question.answers.length) {
      message = 'Nicht komplett beantwortet';
      state = ValidationState.notValidated;
    }

    this.validationService.setQuestionState(this.question, state);
    this.validationMessage = new ValidationMessage(message, state);
  }

  isValidationLocked(): boolean {
    return this._isValidationLocked;
  }

  private initResponseValues(): void {
    this.responseValues = {};
    this.question.answers.forEach(ans => {
      const dict: {[optId: string]: 'radio-true'| 'radio-false' | undefined} = {};
      this.question.options.forEach(opt => {
        dict[opt.id.toString()] = undefined;
      });
      this.responseValues[ans.id.toString()] = dict;
    });
  }
}
