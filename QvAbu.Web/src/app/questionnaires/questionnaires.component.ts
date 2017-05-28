import { Component, OnInit } from '@angular/core';
import { QuestionnairePreview } from '../models/questionnaire-preview';

@Component({
  selector: 'app-questionnaires',
  templateUrl: './questionnaires.component.html',
  styleUrls: ['./questionnaires.component.scss']
})
export class QuestionnairesComponent implements OnInit {
  questionnaires: QuestionnairePreview[] = [{
    name: 'Questionnaire 1',
    id: '11111111-1111-1111-1111-111111111111',
    questionsCount: 16
  }, {
    name: 'Questionnaire 2',
    id: '22222222-2222-2222-2222-222222222222',
    questionsCount: 27
  }, {
    name: 'Questionnaire 3',
    id: '33333333-3333-3333-3333-333333333333',
    questionsCount: 21
  }, {
    name: 'Questionnaire 4',
    id: '44444444-4444-4444-4444-444444444444',
    questionsCount: 23
  }, {
    name: 'Questionnaire 5',
    id: '55555555-5555-5555-5555-555555555555',
    questionsCount: 19
  }];

  constructor() { }

  ngOnInit() {
  }

}
