using D07.Questions;
namespace D07.Exams
{
    public enum EnExamMode : byte
    {
        Queued,
        Starting,
        Finished
    }

    public abstract class ClsExam : ICloneable, IComparable<ClsExam>, IExamBehavior
    {
        public short Time { get; }
        public EnExamMode Mode { get; private set; }
        public ClsQuestionList Questions { get; }
        public ClsSubject Subject { get; }
        public Dictionary<ClsQuestion, object> QuestionAnswerDictionary { get; }
        public abstract string ExamType { get; }

        protected ClsExam(short time, ClsQuestionList questions, ClsSubject subject)
        {
            if (time <= 0 || time > 300) throw new ArgumentException("Time must be 1-300 minutes");
            Questions = questions ?? throw new ArgumentNullException(nameof(questions));
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));

            Time = time;
            Mode = EnExamMode.Queued;
            QuestionAnswerDictionary = new Dictionary<ClsQuestion, object>();
        }

        public virtual void StartExam()
        {
            Mode = EnExamMode.Starting;
            Console.WriteLine($"\n{ExamType} Exam Started");
            Subject.NotifyExamStarted(this);
            StartExamtimer();
        }

        private void StartExamtimer()
        {
            System.Timers.Timer examTimer = new System.Timers.Timer(Time * 60 * 1000);
            examTimer.AutoReset = false; // trigger only once
            examTimer.Elapsed += (sender, e) =>
            {
                examTimer.Stop();
                Console.WriteLine("\nTime is up!");
                Console.WriteLine("Enter the final answer to close the exam");
                Mode = EnExamMode.Finished;
                return;
            };
            examTimer.Start();

            Console.WriteLine($"Exam Timer started: {Time} minutes.");
        }

        public virtual void FinishExam() => Mode = EnExamMode.Finished;

        protected void CorrectExam(out short studentMarks, out short totalMarks)
        {
            studentMarks = 0;
            totalMarks = 0;
            foreach (var question in Questions)
            {
                totalMarks += question.Marks;
                if (QuestionAnswerDictionary.TryGetValue(question, out object? answer) && question.CheckAnswer(answer))
                    studentMarks += question.Marks;
            }
        }

        protected void AdministerExam()
        {

            foreach (var question in Questions)
            {
                if (question is ClsChooseAllQuestion)
                {
                    question.Display();
                    List<byte>? ids = InputHelper.ReadMultipleIDs("Enter IDs separated by comma) ", 1, (byte)question.Answers.Count);

                    if (Mode == EnExamMode.Finished)
                        return;
                    
                    var selectedAnswers = new List<ClsAnswer>();
                    foreach (var id in ids)
                    {
                        var answer = question.Answers.GetAnswerByID(id);
                        if (answer == null)
                            throw new InvalidOperationException($"Answer ID {id} not found.");
                        selectedAnswers.Add(answer);
                    }

                    QuestionAnswerDictionary[question] = selectedAnswers;
                }
                else
                {
                    question.Display();
                    byte id = InputHelper.ReadID("Enter answer ID: ", 1, (byte)question.Answers.Count);
                    
                    if (Mode == EnExamMode.Finished)
                        return;
                    
                    var answer = question.Answers.GetAnswerByID(id);
                    if (answer == null)
                        throw new InvalidOperationException($"Answer ID {id} not found.");

                    QuestionAnswerDictionary[question] = answer;
                }
            }
        }

        public abstract void ShowExam();
        public object Clone() => MemberwiseClone();

        public int CompareTo(ClsExam? other)
        {
            if (other == null) return 1;
            int result = Time.CompareTo(other.Time);
            return result != 0 ? result : Questions.Count.CompareTo(other.Questions.Count);
        }
    }
}
