import { TestBed, inject } from '@angular/core/testing';

import { QuestionnaireValidationService } from './questionnaire-validation.service';
import { QuestionnaireValidationPhase, ValidationState } from '../models/validation-message';
import { EventEmitterFake } from '../../fakes';
import { RevisionEntity } from '../models/revision-entity';

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
    testee.validate();

    // Assert
    expect(eventEmitterFake.emit).toHaveBeenCalledWith(QuestionnaireValidationPhase.init);
    expect(eventEmitterFake.emit).toHaveBeenCalledWith(QuestionnaireValidationPhase.validationLocked);
    expect(eventEmitterFake.emit).toHaveBeenCalledTimes(2);
  });

  it('should keep tally of the different validation states', () => {
    // Arrange
    const ids: RevisionEntity[] = [{
      id: 'id1',
      revision: 1
    }, {
      id: 'id1',
      revision: 2
    }, {
      id: 'id2',
      revision: 1
    }, {
      id: 'id3',
      revision: 1
    }, {
      id: 'id4',
      revision: 1
    }];

    testee.initQuestionnaire(ids);
    testee.setQuestionState(ids[0], ValidationState.info);
    testee.setQuestionState(ids[1], ValidationState.valid);
    testee.setQuestionState(ids[2], ValidationState.invalid);
    testee.setQuestionState(ids[2], ValidationState.valid); // Overwrite invalid
    // id4 not set
    testee.setQuestionState(ids[4], ValidationState.invalid);

    // Act
    const result = testee.validate();

    // Assert
    expect(result).toEqual({
      'valid': 2,
      'invalid': 1,
      'info': 1,
      'notValidated': 1
    });
  });

  it('should not set question state if in invalid phase', () => {
    // Arrange

    // Act
    testee.setQuestionState({id: 'id1', revision: 1}, ValidationState.valid);
    const result = testee.validate();

    // Assert
    expect(result).toEqual({
      'valid': 0,
      'invalid': 0,
      'info': 0,
      'notValidated': 0
    });
  });
});
