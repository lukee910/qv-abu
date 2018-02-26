using System.Collections.Generic;

namespace QvAbu.Data.Models.Questions.ReadModel
{
    public class QuestionnairePreview : RevisionEntity
    {
        public string Name { get; set; }
        public int QuestionsCount { get; set; }
        public List<string> Tags { get; set; }
    }
}
