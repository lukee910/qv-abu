import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { QuestionnairesService } from '../services/questionnaires.service';
import { Question } from '../models/questions/question';

@Component({
  selector: 'app-questionnaire',
  templateUrl: './questionnaire.component.html',
  styleUrls: ['./questionnaire.component.scss']
})
export class QuestionnaireComponent implements OnInit {
  public id: string;
  public revision: number;
  public name: number;
  public questions: Question[];

  constructor(private route: ActivatedRoute, private service: QuestionnairesService) {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.revision = params['revision'];
      this.name = params['name'];
    });
  }

  ngOnInit() {
    this.service.getQuestionsForQuestionnaire(this.id, this.revision)
      .subscribe(_ => this.questions = _);
  }

  toJson(q: Question): string {
    return JSON.stringify(q);
  }
}
