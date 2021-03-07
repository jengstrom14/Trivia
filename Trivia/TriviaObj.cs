using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia
{
    class TriviaObj
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public string Difficulty { get; set; }
        public string Question { get; set; }
        public string Correct_answer { get; set; }
        public string[] Incorrect_answers { get; set; }

        public override string ToString()
        {
            return string.Format("category: {1}{0}type: {2}{0}difficulty: {3}{0}question: {4}{0}correct_answer: {5}{0}incorrect_answers: {6}", 
                System.Environment.NewLine, Category, Type, Difficulty, Question, Correct_answer, String.Join(System.Environment.NewLine, Incorrect_answers));
        }
    }
}
