import { EventEmitter, Injectable } from '@angular/core';
import { Guid } from '../models/guid';
import {
  QuestionnaireValidationPhase, ValidationState, ValidationStates,
  ValidationStateToString
} from '../models/validation-message';
import { RevisionEntity } from '../models/revision-entity';

@Injectable()
export class QuestionnaireValidationService {
  questionnaireValidationPhaseChange: EventEmitter<QuestionnaireValidationPhase> = new EventEmitter();

  private validationStates: { [id: string]: ValidationState };
  private validationPhase: QuestionnaireValidationPhase = null;

  constructor() { }

  initQuestionnaire(ids: RevisionEntity[]): void {
    this.validationStates = {};
    ids.forEach(_ => {
      this.validationStates[this.getEntityId(_)] = ValidationState.notValidated;
    });

    this.setValidationPhase(QuestionnaireValidationPhase.init);
  }

  setQuestionState(entity: RevisionEntity, state: ValidationState): void {
    if (this.validationPhase === null) {
      return;
    }

    this.validationStates[this.getEntityId(entity)] = state;
  }

  validate(): { [state: string]: number } {

    const result = {};
    ValidationStates.forEach(_ => {
      result[ValidationStateToString(_)] = 0;
    });

    if (!this.validationStates) {
      return result;
    }

    Object.keys(this.validationStates).forEach(id => {
      result[ValidationStateToString(this.validationStates[id])]++;
    });

    this.setValidationPhase(QuestionnaireValidationPhase.validationLocked);

    return result;
  }

  private setValidationPhase(phase: QuestionnaireValidationPhase): void {
    this.validationPhase = phase;
    this.questionnaireValidationPhaseChange.emit(phase);
  }

  private getEntityId(entity: RevisionEntity): string {
    return entity.id.toString() + entity.revision;
  }
}
