namespace D07.Questions
{
    public class ClsTrueFalseQuestion : ClsQuestion
    {
        private readonly ClsAnswer _correct;

        public ClsTrueFalseQuestion(string header, string body, short marks, bool correctIsTrue)
            : base(header, body, marks)
        {
            Answers.Add(new ClsAnswer(1, "True"));
            Answers.Add(new ClsAnswer(2, "False"));
            _correct = correctIsTrue ? Answers.GetAnswerByID(1)! : Answers.GetAnswerByID(2)!;
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
