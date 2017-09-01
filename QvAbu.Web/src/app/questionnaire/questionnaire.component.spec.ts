import { QuestionnaireComponent } from './questionnaire.component';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { QuestionnaireServiceFake } from '../../fakes';
import { Question, QuestionType } from '../models/questions/question';
import { QuestionnairePreview } from '../models/questions/questionnaire-preview';

describe('QuestionnaireComponent', () => {
  let component: QuestionnaireComponent;
  let questionnaireService: QuestionnaireServiceFake;

  const id = 'id';
  const revision = 0;

  beforeEach(() => {
    questionnaireService = new QuestionnaireServiceFake();
    component = new QuestionnaireComponent(<ActivatedRoute><any>{
      params: Observable.of({
        id: id,
        revision: revision
      })
    }, <any>questionnaireService);
  });

  it('should load the route params', () => {
    // Arrange
    // In beforeEach

    // Act
    // Instantiate, in beforeEach

    // Assert
    expect(component.revision).toBe(revision);
    expect(component.id).toBe(id);
  });

  it('should load the questions and preview', () => {
    // Arrange
    const questions: Question[] = [{
      id: 'id1',
      revision: 1,
      text: 'text1',
      type: QuestionType.textQuestion
    }];
    const preview: QuestionnairePreview = {
      revision: 0,
      id: 'id',
      name: 'name',
      questionsCount: 2
    };
    questionnaireService.getPreview.and.returnValue(Observable.of(preview));
    questionnaireService.getQuestionsForQuestionnaire.and.returnValue(Observable.of(questions));

    // Act
    component.ngOnInit();

    // Assert
    expect(component.questions).toEqual(questions);
    expect(component.name).toBe('name');
  });
});
