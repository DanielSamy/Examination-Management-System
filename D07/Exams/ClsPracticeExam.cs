namespace D07.Exams
{
    public class ClsPracticeExam : ClsExam
    {
        public override string ExamType => "Practice";
        public ClsPracticeExam(short time, ClsQuestionList questionList, ClsSubject subject) : base(time, questionList, subject) { }

        public override void ShowExam() => AdministerExam();

        public override void FinishExam()
        {
            base.FinishExam();
            CorrectExam(out short studentMarks, out short totalMarks);

            Console.WriteLine("\n--- Final Review ---");
            foreach (var kvp in QuestionAnswerDictionary)
            {
                Console.WriteLine($"Question: {kvp.Key.Body}");
                if (kvp.Value is List<ClsAnswer> list)
                    Console.WriteLine("Your Answer: " + string.Join(", ", list));
                else
                    Console.WriteLine($"Your Answer: {kvp.Value}");
            }
            Console.WriteLine($"Final Grade: {studentMarks}/{totalMarks}");

            SaveResults(studentMarks, totalMarks);
        }

        private void SaveResults(short studentMarks, short totalMarks)
        {
            using StreamWriter sw = new StreamWriter("ExamResults.txt", true);
            sw.WriteLine("=== Exam Result ===");
            sw.WriteLine($"Exam Type: {ExamType}");
            sw.WriteLine($"Subject: {Subject.Name}");
            foreach (var kvp in QuestionAnswerDictionary)
            {
                sw.WriteLine($"Q: {kvp.Key.Body}");
                if (kvp.Value is List<ClsAnswer> list)
                    sw.WriteLine($"Answer: {string.Join(", ", list)}");
                else
                    sw.WriteLine($"Answer: {kvp.Value}");
            }
            sw.WriteLine($"Final Grade: {studentMarks}/{totalMarks}");
            sw.WriteLine($"Time: {DateTime.Now}");
            sw.WriteLine("==================\n");
        }
    }
}
