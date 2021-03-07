using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia
{
    class Form1Model
    {
        private int qIndex = 0;
        public int QuestionNumber = 1;

        public List<TriviaObj> Questions { get; set; }
        public TriviaObj CurQuestion { get; set; }
        public string CategoryText { get; set; }
        public string Difficulty_Text { get; set; }
        public string QuestionText { get; set; }
        public List<string> Responses { get; set; }

        public Form1Model()
        {
            Questions = TriviaApi.GetTriviaQuestionsNoAnime();
            CurQuestion = Questions[qIndex];
            SetTextFields();
        }

        public void NextQuestion()
        {
            qIndex++;
            QuestionNumber++;
            if (qIndex >= Questions.Count)
            {
                Questions = TriviaApi.GetTriviaQuestionsNoAnime();
                qIndex = 0;
            }
            CurQuestion = Questions[qIndex];
            SetTextFields();
        }

        private void SetTextFields()
        {
            CategoryText = CurQuestion.Category;
            Difficulty_Text = CurQuestion.Difficulty;
            QuestionText = CurQuestion.Question;
            var responses = new List<string>();
            responses.AddRange(CurQuestion.Incorrect_answers);
            responses.Add(CurQuestion.Correct_answer);
            Responses = responses.OrderBy(x => Guid.NewGuid()).ToList();
        }

    }
}
