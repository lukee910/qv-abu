import { TestBed, inject } from '@angular/core/testing';

import { QuestionnaireValidationService } from './questionnaire-validation.service';
import { QuestionnaireValidationPhase, ValidationState } from '../models/validation-message';
import { Guid } from '../models/guid';
import { EventEmitterFake } from '../../fakes';

describe('ValidationService', () => {
  let testee: QuestionnaireValidationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [QuestionnaireValidationService]
    });
  });

  beforeEach(inject([QuestionnaireValidationService], (t: QuestionnaireValidationService) => {
    testee = t;
  }));

  it('should update the questionnaireValidationPhaseChange event emitter when init', () => {
    // Arrange
    const eventEmitterFake = new EventEmitterFake();
    testee.questionnaireValidationPhaseChange = <any>eventEmitterFake;

    // Act
    testee.initQuestionnaire([]);

    // Assert
    expect(eventEmitterFake.emit).toHaveBeenCalledWith(QuestionnaireValidationPhase.init);
  });

  it('should update the questionnaireValidationPhaseChange event emitter when validate', () => {
    const eventEmitterFake = new EventEmitterFake();
    testee.questionnaireValidationPhaseChange = <any>eventEmitterFake;

    // Act
    testee.initQuestionnaire([]);
    testee.getValidationResult();

    // Assert
    expect(eventEmitterFake.emit).toHaveBeenCalledWith(QuestionnaireValidationPhase.init);
    expect(eventEmitterFake.emit).toHaveBeenCalledWith(QuestionnaireValidationPhase.validationLocked);
    expect(eventEmitterFake.emit).toHaveBeenCalledTimes(2);
  });

  it('should keep tally of the different validation states', () => {
    // Arrange
    const ids: Guid[] = [
      'id1',
      'id2',
      'id3',
      'id4',
      'id5'
    ];

    testee.initQuestionnaire(ids);
    testee.setQuestionState('id1', ValidationState.info);
    testee.setQuestionState('id2', ValidationState.valid);
    testee.setQuestionState('id3', ValidationState.invalid);
    testee.setQuestionState('id3', ValidationState.valid); // Overwrite invalid
    // id4 not set
    testee.setQuestionState('id5', ValidationState.invalid);

    // Act
    const result = testee.getValidationResult();

    // Assert
    expect(result).toEqual({
      'valid': 2,
      'invalid': 1,
      'info': 1,
      'notValidated': 1
    });
  });
});
