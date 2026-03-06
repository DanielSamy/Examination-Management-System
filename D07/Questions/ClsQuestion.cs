namespace D07.Questions
{
    public abstract class ClsQuestion
    {
        public string Header { get; }
        public string Body { get; }
        public short Marks { get; }
        public ClsAnswerList Answers { get; }// T/F

        protected ClsQuestion(string header, string body, short marks)
        {
            if (string.IsNullOrWhiteSpace(header)) throw new ArgumentException("Header cannot be empty");
            if (string.IsNullOrWhiteSpace(body)) throw new ArgumentException("Body cannot be empty");
            if (marks <= 0) throw new ArgumentException("Marks must be greater than 0");

            Header = header;
            Body = body;
            Marks = marks;
            Answers = new ClsAnswerList();
        }

        public abstract void Display();
        public abstract bool CheckAnswer(object studentAnswer);

        public override string ToString() => $"{Header}: {Body} ({Marks} Marks)";

        public override bool Equals(object? obj)
            => ReferenceEquals(this, obj) || obj is ClsQuestion other && Header == other.Header && Body == other.Body && Marks == other.Marks;

        public override int GetHashCode() => HashCode.Combine(Header, Body, Marks);
    }
}
