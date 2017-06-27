import { Component, Input, OnInit } from '@angular/core';
import { AssignmentQuestion } from '../../models/questions/assignment-question';

@Component({
  selector: 'app-assignment-question',
  templateUrl: './assignment-question.component.html',
  styleUrls: ['./assignment-question.component.scss']
})
export class AssignmentQuestionComponent implements OnInit {
  @Input()
  question: AssignmentQuestion;

  constructor() { }

  ngOnInit() {
  }

  toChar(int: number): string {
    return String.fromCharCode(97 + int);
  }
}
