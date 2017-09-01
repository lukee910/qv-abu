import { Component, Input, OnInit, Output } from '@angular/core';
import { AssignmentQuestion } from '../../models/questions/assignment-question';
import { AssignmentResponseAnswer } from '../../models/questions/response-answer';
import { AssignmentOption } from '../../models/questions/assignment-option';
import { AssignmentAnswer } from '../../models/questions/assignment-answer';

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

  constructor() { }

  ngOnInit() {
    // this.question.options.forEach(opt => {
    //   this.question.answers.forEach(answer => {
    //     this.selectedValues[opt.id + <string>answer.id] = false;
    //   });
    // });
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
  }
}
