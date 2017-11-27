import { Component, Input, OnInit, Output } from '@angular/core';
import { SimpleQuestion, SimpleQuestionType } from '../../models/questions/simple-question';
import { SimpleResponseAnswer } from '../../models/questions/response-answer';
import { QuestionnaireValidationService } from '../../services/questionnaire-validation.service';
import { QuestionnaireValidationPhase, ValidationMessage, ValidationState } from '../../models/validation-message';

@Component({
  selector: 'app-simple-question',
  templateUrl: './simple-question.component.html',
  styleUrls: ['./simple-question.component.scss']
})
export class SimpleQuestionComponent implements OnInit {
  @Input()
  question: SimpleQuestion;
  @Output()
  responses: SimpleResponseAnswer[] = [];

  subtitle: string;
  validationMessage: ValidationMessage = new ValidationMessage('Nicht beantwortet', ValidationState.notValidated);
  validationResult: ('radio-false' | 'radio-true' | undefined)[] = [];

  private _isValidationLocked = false;

  constructor(private validationService: QuestionnaireValidationService) {
    this.validationService.questionnaireValidationPhaseChange.subscribe(_ => {
      if (_ === QuestionnaireValidationPhase.validationLocked) {
        this._isValidationLocked = true;
      }
    });
  }

  ngOnInit() {
    this.question.answers.forEach(_ => {
      this.responses.push(new SimpleResponseAnswer(_));
      this.validationResult.push(undefined);
    });

    switch (this.question.simpleQuestionType) {
      case SimpleQuestionType.singleChoice:
        this.subtitle = 'Kreuzen sie die zutreffende Aussage an.';
        break;
      case SimpleQuestionType.multipleChoice:
        const correctAnswersCount = this.question.answers.filter(_ => _.isCorrect).length;
        this.subtitle = `Kreuzen sie die zutreffenden ${correctAnswersCount} Aussagen an.`;
        break;
      case SimpleQuestionType.trueFalse:
        this.subtitle = 'Kreuzen sie die zutreffenden Aussagen an.';
        break;
    }
  }

  onInputChange(answerId: string): void {
    const response = this.responses.filter(_ => _.answer.id === answerId)[0];

    if (this.question.simpleQuestionType === SimpleQuestionType.singleChoice) {
      this.responses.forEach(_ => _.value = false);
      response.value = true;
    } else {
      response.value = !response.value;
    }

    this.validate();
  }

  validate(): void {
    this.validationResult = new Array(this.question.answers.length);
    let isValid = true;
    for (let i = 0; i < this.question.answers.length; i++) {
      const expectedAnswer = this.question.answers[i].isCorrect;
      if (expectedAnswer) {
        if (!this.responses[i].value) {
          isValid = false;
        }

        this.validationResult[i] = 'radio-true';
      } else if (this.responses[i].value) {
        isValid = false;
        this.validationResult[i] = 'radio-false';
      }
    }

    const state = isValid ? ValidationState.valid : ValidationState.invalid;
    this.validationService.setQuestionState(this.question, state);
    this.validationMessage = new ValidationMessage(isValid ? 'Richtige Antwort' : 'Falsche Antwort', state);
  }

  isValidationLocked(): boolean {
    return this._isValidationLocked;
  }
}
