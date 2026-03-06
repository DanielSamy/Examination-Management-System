namespace D07
{
    public class ClsAnswer : IComparable<ClsAnswer>
    {
        public int Id { get; }
        public string Text { get; }

        public ClsAnswer(int id, string text)
        {
            if (id <= 0) throw new ArgumentException("Answer ID must be > 0");
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Answer text cannot be empty");

            Id = id;
            Text = text;
        }

        public override string ToString() => $"{Id}. {Text}";

        public override bool Equals(object? obj)
            => ReferenceEquals(this, obj) || obj is ClsAnswer other && Id == other.Id && Text == other.Text;

        public override int GetHashCode() => HashCode.Combine(Id, Text);

        public int CompareTo(ClsAnswer? other) => Id.CompareTo(other?.Id ?? 1);
    }
}
