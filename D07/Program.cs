using D07.Exams;
using D07.DTOs;

namespace D07
{
    class Program
    {
        static void Main()
        {
            ClsSubject subject = new ClsSubject("OOP");
            subject.Enroll(new ClsStudent(1, "Daniel"));

            do
            {
                Console.Clear();

                ClsQuestionList qList = QuestionLoader.LoadRandom("questions.json");

                ClsExam practice = new ClsPracticeExam(1, qList, subject);
                ClsExam final = new ClsFinalExam(5, qList, subject);

                Console.WriteLine("Select Exam Type:\n1 - Practice\n2 - Final");
                byte choice = InputHelper.ReadID("Choice Exam Type: ", 1, 2);

                ClsExam selected = (choice == 1) ? practice : final;

                selected.StartExam();
                selected.ShowExam();
                selected.FinishExam();

                Console.Write("\nDo you want to take another exam? (Y/N): ");

            } while (Console.ReadLine()?.Trim().ToLower() == "y");
        }
    }
}
