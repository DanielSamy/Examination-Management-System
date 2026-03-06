using D07.Exams;

namespace D07
{
    public class ClsSubject
    {
        public string Name { get; }
        private readonly List<ClsStudent>? _students = new();
        public event EventHandler<ClsExamEventArgs>? OnExamStarted;

        public ClsSubject(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Subject name required");
            Name = name;
        }

        public void Enroll(ClsStudent? student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student), "Student cannot be null");
            if (_students != null && _students.Any(s => s.Id == student.Id))
                throw new InvalidOperationException("Student is already enrolled in this subject");
            
            _students?.Add(student);
            student.Subscribe(this);
        }

        protected virtual void RaiseExamStarted(ClsExamEventArgs e)
            => OnExamStarted?.Invoke(this, e);

        public void NotifyExamStarted(ClsExam exam)
            => RaiseExamStarted(new ClsExamEventArgs(exam));
    }
}
