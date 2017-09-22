import { Component, Input, OnInit, Output } from '@angular/core';
import { AssignmentQuestion } from '../../models/questions/assignment-question';
import { AssignmentResponseAnswer } from '../../models/questions/response-answer';
import { AssignmentOption } from '../../models/questions/assignment-option';
import { AssignmentAnswer } from '../../models/questions/assignment-answer';
import { QuestionnaireValidationService } from '../../services/questionnaire-validation.service';
import { ValidationState } from '../../models/validation-message';

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

  constructor(private validationService: QuestionnaireValidationService) { }

  ngOnInit() {
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
    let isInvalid = false;
    this.responses.forEach(response => {
      if (response.value !== response.answer.correctOptionId) {
        isInvalid = true;
      }
    });

    this.validationService.setQuestionState(this.question, isInvalid ? ValidationState.invalid : ValidationState.valid);
  }
}
