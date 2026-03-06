using D07.Exams;

namespace D07
{
    public class ClsExamEventArgs : EventArgs
    {
        public ClsExam Exam { get; }

        public ClsExamEventArgs(ClsExam exam)
        {
            Exam = exam;
        }
    }
}
