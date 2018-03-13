import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { QuestionnairesComponent } from './questionnaires/questionnaires.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { QuestionnaireComponent } from './questionnaire/questionnaire.component';
import { QuestionnairesService } from './services/questionnaires.service';
import { ApiService } from './services/api.service';
import { SimpleQuestionComponent } from './questions/simple-question/simple-question.component';
import { AssignmentQuestionComponent } from './questions/assignment-question/assignment-question.component';
import { TextQuestionComponent } from './questions/text-question/text-question.component';
import { ValidationMessageComponent } from './validation-message/validation-message.component';
import { QuestionnaireValidationService } from './services/questionnaire-validation.service';
import { WindowService } from './services/window.service';
import { LoadingSpinnerComponent } from './utils/loading-spinner/loading-spinner.component';

const appRoutes: Routes = [
  {path: '', component: QuestionnairesComponent},
  {path: 'questionnaire/:presetId/:name', component: QuestionnaireComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    QuestionnairesComponent,
    QuestionnaireComponent,
    SimpleQuestionComponent,
    AssignmentQuestionComponent,
    TextQuestionComponent,
    ValidationMessageComponent,
    LoadingSpinnerComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    NgbModule.forRoot()
  ],
  providers: [
    QuestionnairesService,
    ApiService,
    QuestionnaireValidationService,
    WindowService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
