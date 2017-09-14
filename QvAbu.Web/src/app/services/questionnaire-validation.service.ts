import { EventEmitter, Injectable } from '@angular/core';
import { Guid } from '../models/guid';
import {
  QuestionnaireValidationPhase, ValidationState, ValidationStates,
  ValidationStateToString
} from '../models/validation-message';

@Injectable()
export class QuestionnaireValidationService {
  questionnaireValidationPhaseChange: EventEmitter<QuestionnaireValidationPhase> = new EventEmitter();

  private validationStates: { [id: string]: ValidationState };

  constructor() { }

  initQuestionnaire(ids: Guid[]): void {
    this.validationStates = {};
    ids.forEach(_ => {
      this.validationStates[_.toString()] = ValidationState.notValidated;
    });

    this.questionnaireValidationPhaseChange.emit(QuestionnaireValidationPhase.init);
  }

  setQuestionState(id: Guid, state: ValidationState): void {
    this.validationStates[id.toString()] = state;
  }

  validate(): { [state: string]: number } {
    const result = {};
    ValidationStates.forEach(_ => {
      result[ValidationStateToString(_)] = 0;
    });

    Object.keys(this.validationStates).forEach(id => {
      result[ValidationStateToString(this.validationStates[id])]++;
    });

    this.questionnaireValidationPhaseChange.emit(QuestionnaireValidationPhase.validationLocked);

    return result;
  }
}
