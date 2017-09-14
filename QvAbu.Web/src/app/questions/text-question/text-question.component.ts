import { Component, Input, OnInit, Output } from '@angular/core';
import { TextQuestion } from '../../models/questions/text-question';
import { TextResponseAnswer } from '../../models/questions/response-answer';
import { QuestionnaireValidationService } from '../../services/questionnaire-validation.service';
import { QuestionnaireValidationPhase, ValidationMessage, ValidationState } from '../../models/validation-message';

@Component({
  selector: 'app-text-question',
  templateUrl: './text-question.component.html',
  styleUrls: ['./text-question.component.scss']
})
export class TextQuestionComponent implements OnInit {
  @Input()
  question: TextQuestion;
  @Output()
  response: TextResponseAnswer = new TextResponseAnswer();

  private _isValidationLocked = false;
  private _validationMessage: ValidationMessage;

  constructor(private validationService: QuestionnaireValidationService) {
    this.validationService.questionnaireValidationPhaseChange.subscribe(_ => {
      if (_ === QuestionnaireValidationPhase.init) {
        this._isValidationLocked = false;
        this.validationService.setQuestionState(this.question.id, ValidationState.info);
      } else {
        this._isValidationLocked = true;
        this._validationMessage = new ValidationMessage(this.question.answer.text, ValidationState.info);
      }
    });
  }

  ngOnInit() {
    this.validationService.setQuestionState(this.question.id, ValidationState.info);
  }

  isValidationLocked(): boolean {
    return this._isValidationLocked;
  }

  getValidationMessage(): ValidationMessage {
    return this._validationMessage;
  }
}
