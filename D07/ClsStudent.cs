namespace D07
{
    public class ClsStudent
    {
        public int Id { get; }
        public string Name { get; }

        public ClsStudent(int id, string name)
        {
            if (id <= 0) throw new ArgumentException("Invalid student ID");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");

            Id = id;
            Name = name;
        }

        public void Subscribe(ClsSubject subject)
            => subject.OnExamStarted += OnExamStarted;

        private void OnExamStarted(object? sender, ClsExamEventArgs e)
            => Console.WriteLine($"\nStudent {Name} notified: {e.Exam.ExamType} started for Subject {e.Exam.Subject.Name}.\n");
    }
}
