namespace D07.Questions
{
    public class ClsChooseOneQuestion : ClsQuestion
    {
        private readonly ClsAnswer _correct;

        public ClsChooseOneQuestion(string header, string body, short marks, ClsAnswerList answers, ClsAnswer correct)
            : base(header, body, marks)
        {
            if (answers == null || answers.Count < 2) throw new ArgumentException("At least two answers required.");
            if (correct == null) throw new ArgumentNullException(nameof(correct));
            if (answers.GetAnswerByID(correct.Id) == null)
                throw new ArgumentException("Correct answer must exist in answer list.");

            foreach (var answer in answers.GetAllAnswers())
                Answers.Add(answer);
            _correct = correct;
        }

        public override void Display()
        {
            Console.WriteLine(this);
            foreach (var answer in Answers.GetAllAnswers())
                Console.WriteLine(answer);
        }

        public override bool CheckAnswer(object studentAnswer) => studentAnswer is ClsAnswer ans && ans.Equals(_correct);
    }
}
