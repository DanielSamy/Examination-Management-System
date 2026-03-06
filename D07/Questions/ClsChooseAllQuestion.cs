namespace D07.Questions
{
    public class ClsChooseAllQuestion : ClsQuestion
    {
        private readonly List<ClsAnswer> _correctAnswers;

        public ClsChooseAllQuestion(string header, string body, short marks, ClsAnswerList answers, List<ClsAnswer> correct)
            : base(header, body, marks)
        {
            if (answers == null || answers.Count < 2) throw new ArgumentException("At least two answers required.");
            if (correct == null || !correct.Any()) throw new ArgumentException("At least one correct answer required.");

            foreach (var a in answers.GetAllAnswers()) Answers.Add(a);

            foreach (var c in correct)
                if (Answers.GetAnswerByID(c.Id) == null)
                    throw new ArgumentException($"Correct answer ID {c.Id} not found in answers.");

            _correctAnswers = new List<ClsAnswer>(correct);
        }

        public override void Display()
        {
            Console.WriteLine(this);
            foreach (var answer in Answers.GetAllAnswers())
                Console.WriteLine(answer);
        }

        public override bool CheckAnswer(object studentAnswer)
        {
            if (studentAnswer is not List<ClsAnswer> list) return false;
            if (list.Count != _correctAnswers.Count) return false;
            return _correctAnswers.Count == list.Count && !_correctAnswers.Except(list).Any();
        }
    }
}
