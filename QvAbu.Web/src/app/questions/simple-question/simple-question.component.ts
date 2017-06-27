import { Component, Input, OnInit } from '@angular/core';
import { SimpleQuestion } from '../../models/questions/simple-question';

@Component({
  selector: 'app-simple-question',
  templateUrl: './simple-question.component.html',
  styleUrls: ['./simple-question.component.scss']
})
export class SimpleQuestionComponent implements OnInit {
  @Input()
  question: SimpleQuestion;

  correctAnswersCount: number;

  constructor() { }

  ngOnInit() {
    this.correctAnswersCount = this.question.answers.filter(_ => _.isCorrect).length;
  }
}
