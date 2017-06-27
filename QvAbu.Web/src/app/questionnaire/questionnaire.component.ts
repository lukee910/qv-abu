import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { QuestionnairesService } from '../services/questionnaires.service';
import { Question, QuestionType } from '../models/questions/question';

@Component({
  selector: 'app-questionnaire',
  templateUrl: './questionnaire.component.html',
  styleUrls: ['./questionnaire.component.scss']
})
export class QuestionnaireComponent implements OnInit {
  public id: string;
  public revision: number;
  public name: string;
  public questions: Question[];
  //noinspection JSUnusedLocalSymbols
  private questionTypes = {
    simpleQuestion: QuestionType.simpleQuestion,
    assignmentQuestion: QuestionType.assignmentQuestion,
    textQuestion: QuestionType.textQuestion
  };

  constructor(private route: ActivatedRoute, private service: QuestionnairesService) {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.revision = parseInt(params['revision'], 10);
    });
  }

  ngOnInit() {
    this.service.getPreview(this.id, this.revision).subscribe(_ => this.name = _.name);
    this.service.getQuestionsForQuestionnaire(this.id, this.revision)
      .subscribe(_ => this.questions = _);
  }

  //noinspection JSUnusedGlobalSymbols
  toJson(q: Question): string {
    return JSON.stringify(q);
  }
}
