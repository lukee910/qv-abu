import { Component, Input, OnInit } from '@angular/core';
import { SimpleQuestion, SimpleQuestionType } from '../../models/questions/simple-question';

@Component({
  selector: 'app-simple-question',
  templateUrl: './simple-question.component.html',
  styleUrls: ['./simple-question.component.scss']
})
export class SimpleQuestionComponent implements OnInit {
  @Input()
  question: SimpleQuestion;

  subtitle: string;

  /*question.simpleQuestionType
   ? ('zutreffenden' + (question.isNumberOfAnswersGiven ? ' ' + correctAnswersCount : '') + ' Aussagen')
   : 'zutreffende Aussage'*/

  constructor() { }

  ngOnInit() {
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
}
