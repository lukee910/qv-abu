import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { QuestionnairesService } from '../services/questionnaires.service';
import { Question, QuestionType } from '../models/questions/question';
import { QuestionnaireValidationService } from '../services/questionnaire-validation.service';
import { ValidationState, ValidationStateToString } from '../models/validation-message';
import { WindowService } from '../services/window.service';
import { RevisionEntity } from '../models/revision-entity';

@Component({
  selector: 'app-questionnaire',
  templateUrl: './questionnaire.component.html',
  styleUrls: ['./questionnaire.component.scss']
})
export class QuestionnaireComponent implements OnInit {
  public validationResult: { [state: string]: number; };
  public questionnaires: RevisionEntity[];
  public names: string[] = [];
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
      this.questionnaires = JSON.parse(localStorage.getItem('questionnaire.' + params['presetId']));
    });
  }

  ngOnInit() {
    this.questionnaires.forEach(_ => {
      this.service.getPreview(_.id, _.revision)
        .subscribe(_ => this.names.push(_.name));
    });
    this.service.getQuestionsForQuestionnaires(this.questionnaires)
      .subscribe(_ => {
        this.questions = _;
        this.validationService.initQuestionnaire(this.questions);
      })
    // this.service.getPreview(this.id, this.revision).subscribe(_ => this.name = _.name);
    // this.service.getQuestionsForQuestionnaires(null/*this.id, this.revision*/)
    //   .subscribe(_ => {
    //     this.questions = _;
    //     this.validationService.initQuestionnaire(this.questions);
    //   });
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
