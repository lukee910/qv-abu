import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionnairesComponent } from './questionnaires.component';
import { QuestionnairesService } from '../services/questionnaires.service';
import { QuestionnaireServiceFake } from '../../fakes';
import { QuestionnairePreview } from '../models/questionnaire-preview';
import { Observable } from 'rxjs/Observable';

describe('QuestionnairesComponent', () => {
  let component: QuestionnairesComponent;
  let questionnairesService: QuestionnaireServiceFake;

  beforeEach(() => {
    questionnairesService = new QuestionnaireServiceFake();
    component = new QuestionnairesComponent(questionnairesService);
  });

  it('should load previews on init', async(() => {
    // Arrange
    const previews: QuestionnairePreview[] = [];
    questionnairesService.getPreviews.and.returnValue(Observable.of(previews));

    // Act
    component.ngOnInit();

    // Assert
    expect(component.questionnaires).toEqual(previews);
  }));
});
