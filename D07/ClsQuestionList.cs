using D07.Questions;

namespace D07
{
    public class ClsQuestionList : List<ClsQuestion>
    {
        private readonly string _fileName;

        public ClsQuestionList(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("File name required");
            _fileName = fileName;
        }

        public new void Add(ClsQuestion question)
        {
            base.Add(question);

            using StreamWriter sw = new StreamWriter(_fileName, true);

            sw.WriteLine("=================================");
            sw.WriteLine($"Question: {question.Header}");
            sw.WriteLine(question.Body);
            sw.WriteLine($"Marks: {question.Marks}");
            sw.WriteLine();

            sw.WriteLine("Answers:");
            foreach (var answer in question.Answers.GetAllAnswers())
                sw.WriteLine($"   {answer}");

            sw.WriteLine();
            sw.WriteLine($"Added: {DateTime.Now}");
            sw.WriteLine("=================================\n");
        }
    }
}
