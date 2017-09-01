import { Component, Input, OnInit, Output } from '@angular/core';
import { TextQuestion } from '../../models/questions/text-question';
import { TextResponseAnswer } from '../../models/questions/response-answer';

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

  constructor() { }

  ngOnInit() {
  }

}
