IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Questionnaires] (
    [ID] uniqueidentifier NOT NULL,
    [Revision] int NOT NULL,
    [Name] nvarchar(max) NULL,
    [Tags] nvarchar(max) NULL,
    CONSTRAINT [PK_Questionnaires] PRIMARY KEY ([ID], [Revision])
);

GO

CREATE TABLE [TextAnswers] (
    [ID] uniqueidentifier NOT NULL,
    [Text] nvarchar(max) NULL,
    CONSTRAINT [PK_TextAnswers] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [Question] (
    [ID] uniqueidentifier NOT NULL,
    [Revision] int NOT NULL,
    [Discriminator] nvarchar(max) NOT NULL,
    [Text] nvarchar(max) NULL,
    [SimpleQuestionType] int NULL,
    [AnswerId] uniqueidentifier NULL,
    CONSTRAINT [PK_Question] PRIMARY KEY ([ID], [Revision]),
    CONSTRAINT [FK_Question_TextAnswers_AnswerId] FOREIGN KEY ([AnswerId]) REFERENCES [TextAnswers] ([ID]) ON DELETE CASCADE
);

GO

CREATE TABLE [AssignmentOptions] (
    [ID] uniqueidentifier NOT NULL,
    [AssignmentQuestionID] uniqueidentifier NULL,
    [AssignmentQuestionRevision] int NULL,
    [Text] nvarchar(max) NULL,
    CONSTRAINT [PK_AssignmentOptions] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_AssignmentOptions_Question_AssignmentQuestionID_AssignmentQuestionRevision] FOREIGN KEY ([AssignmentQuestionID], [AssignmentQuestionRevision]) REFERENCES [Question] ([ID], [Revision]) ON DELETE NO ACTION
);

GO

CREATE TABLE [QuestionnaireQuestions] (
    [ID] uniqueidentifier NOT NULL,
    [QuestionID] uniqueidentifier NULL,
    [QuestionRevision] int NULL,
    [QuestionnaireID] uniqueidentifier NULL,
    [QuestionnaireRevision] int NULL,
    CONSTRAINT [PK_QuestionnaireQuestions] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_QuestionnaireQuestions_Question_QuestionID_QuestionRevision] FOREIGN KEY ([QuestionID], [QuestionRevision]) REFERENCES [Question] ([ID], [Revision]) ON DELETE NO ACTION,
    CONSTRAINT [FK_QuestionnaireQuestions_Questionnaires_QuestionnaireID_QuestionnaireRevision] FOREIGN KEY ([QuestionnaireID], [QuestionnaireRevision]) REFERENCES [Questionnaires] ([ID], [Revision]) ON DELETE NO ACTION
);

GO

CREATE TABLE [SimpleAnswers] (
    [ID] uniqueidentifier NOT NULL,
    [IsCorrect] bit NOT NULL,
    [SimpleQuestionID] uniqueidentifier NULL,
    [SimpleQuestionRevision] int NULL,
    [Text] nvarchar(max) NULL,
    CONSTRAINT [PK_SimpleAnswers] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_SimpleAnswers_Question_SimpleQuestionID_SimpleQuestionRevision] FOREIGN KEY ([SimpleQuestionID], [SimpleQuestionRevision]) REFERENCES [Question] ([ID], [Revision]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AssignmentAnswers] (
    [ID] uniqueidentifier NOT NULL,
    [AssignmentQuestionID] uniqueidentifier NULL,
    [AssignmentQuestionRevision] int NULL,
    [CorrectOptionId] uniqueidentifier NOT NULL,
    [Text] nvarchar(max) NULL,
    CONSTRAINT [PK_AssignmentAnswers] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_AssignmentAnswers_AssignmentOptions_CorrectOptionId] FOREIGN KEY ([CorrectOptionId]) REFERENCES [AssignmentOptions] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_AssignmentAnswers_Question_AssignmentQuestionID_AssignmentQuestionRevision] FOREIGN KEY ([AssignmentQuestionID], [AssignmentQuestionRevision]) REFERENCES [Question] ([ID], [Revision]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_AssignmentAnswers_CorrectOptionId] ON [AssignmentAnswers] ([CorrectOptionId]);

GO

CREATE INDEX [IX_AssignmentAnswers_AssignmentQuestionID_AssignmentQuestionRevision] ON [AssignmentAnswers] ([AssignmentQuestionID], [AssignmentQuestionRevision]);

GO

CREATE INDEX [IX_AssignmentOptions_AssignmentQuestionID_AssignmentQuestionRevision] ON [AssignmentOptions] ([AssignmentQuestionID], [AssignmentQuestionRevision]);

GO

CREATE INDEX [IX_Question_AnswerId] ON [Question] ([AnswerId]);

GO

CREATE INDEX [IX_QuestionnaireQuestions_QuestionID_QuestionRevision] ON [QuestionnaireQuestions] ([QuestionID], [QuestionRevision]);

GO

CREATE INDEX [IX_QuestionnaireQuestions_QuestionnaireID_QuestionnaireRevision] ON [QuestionnaireQuestions] ([QuestionnaireID], [QuestionnaireRevision]);

GO

CREATE INDEX [IX_SimpleAnswers_SimpleQuestionID_SimpleQuestionRevision] ON [SimpleAnswers] ([SimpleQuestionID], [SimpleQuestionRevision]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180228200016_Init', N'2.0.1-rtm-125');

GO
