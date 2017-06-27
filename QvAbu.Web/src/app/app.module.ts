import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { QuestionnairesComponent } from './questionnaires/questionnaires.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { QuestionnaireComponent } from './questionnaire/questionnaire.component';
import { QuestionnairesService } from './services/questionnaires.service';
import { ApiService } from './services/api.service';
import { SimpleQuestionComponent } from './questions/simple-question/simple-question.component';

const appRoutes: Routes = [
  {path: '', component: QuestionnairesComponent},
  {path: 'questionnaire/:id/:revision/:name', component: QuestionnaireComponent}
  // {
  //   path: 'heroes',
  //   component: HeroListComponent,
  //   data: { title: 'Heroes List' }
  // },
  // { path: '',
  //   redirectTo: '/heroes',
  //   pathMatch: 'full'
  // },
  // { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    QuestionnairesComponent,
    QuestionnaireComponent,
    SimpleQuestionComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot(appRoutes),
    NgbModule.forRoot()
  ],
  providers: [
    QuestionnairesService,
    ApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
