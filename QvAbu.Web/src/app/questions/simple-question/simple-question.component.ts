import { Component, Input, OnInit, Output } from '@angular/core';
import { SimpleQuestion, SimpleQuestionType } from '../../models/questions/simple-question';
import { SimpleResponseAnswer } from '../../models/questions/response-answer';
import { QuestionnaireValidationService } from '../../services/questionnaire-validation.service';
import { ValidationState } from '../../models/validation-message';

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

  constructor(private validationService: QuestionnaireValidationService) { }

  ngOnInit() {
    this.question.answers.forEach(_ => {
      this.responses.push(new SimpleResponseAnswer(_));
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

  validate(): void {
    this.validationService.setQuestionState(this.question, ValidationState.valid);
  }
}
