import { Component, Input, OnInit } from '@angular/core';
import { TextQuestion } from '../../models/questions/text-question';

@Component({
  selector: 'app-text-question',
  templateUrl: './text-question.component.html',
  styleUrls: ['./text-question.component.scss']
})
export class TextQuestionComponent implements OnInit {
  @Input()
  question: TextQuestion;

  constructor() { }

  ngOnInit() {
  }

}
