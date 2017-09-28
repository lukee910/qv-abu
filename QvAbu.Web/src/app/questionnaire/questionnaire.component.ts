import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { QuestionnairesService } from '../services/questionnaires.service';
import { Question, QuestionType } from '../models/questions/question';
import { QuestionnaireValidationService } from '../services/questionnaire-validation.service';
import { ValidationState, ValidationStateToString } from '../models/validation-message';
import { WindowService } from '../services/window.service';

@Component({
  selector: 'app-questionnaire',
  templateUrl: './questionnaire.component.html',
  styleUrls: ['./questionnaire.component.scss']
})
export class QuestionnaireComponent implements OnInit {
  public validationResult: { [state: string]: number; };
  public id: string;
  public revision: number;
  public name: string;
  public questions: Question[];
  public refreshInProgress = false;

  //noinspection JSUnusedLocalSymbols
  public questionTypes = {
    simpleQuestion: QuestionType.simpleQuestion,
    assignmentQuestion: QuestionType.assignmentQuestion,
    textQuestion: QuestionType.textQuestion
  };

  constructor(private route: ActivatedRoute,
              private service: QuestionnairesService,
              private validationService: QuestionnaireValidationService,
              public window: WindowService) {
    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
      this.revision = parseInt(params['revision'], 10);
    });
  }

  ngOnInit() {
    this.service.getPreview(this.id, this.revision).subscribe(_ => this.name = _.name);
    this.service.getQuestionsForQuestionnaire(this.id, this.revision)
      .subscribe(_ => {
        this.questions = _;
        this.validationService.initQuestionnaire(this.questions);
      });
  }

  //noinspection JSUnusedGlobalSymbols
  toJson(q: Question): string {
    return JSON.stringify(q);
  }

  validate(): void {
    this.validationResult = this.validationService.validate();
    this.window.scrollTo(0, 0);
  }

  getAlertClass(): string {
    if (this.validationResult[ValidationStateToString(ValidationState.invalid)] > 0) {
      return 'danger';
    }

    if (this.validationResult[ValidationStateToString(ValidationState.notValidated)] > 0) {
      return 'warning';
    }

    return 'success';
  }

  // TODO: Unit Test this
  reset(): void {
    this.refreshInProgress = true;
    this.validationService.initQuestionnaire(this.questions);
    this.validationResult = undefined;
    setTimeout(() => {
      this.refreshInProgress = false;
    });
  }
}
