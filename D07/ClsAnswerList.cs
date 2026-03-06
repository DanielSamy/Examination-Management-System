namespace D07
{
    public class ClsAnswerList
    {
        private List<ClsAnswer> _answers = new();

        public int Count => _answers.Count;

        public void Add(ClsAnswer answer)
        {
            if (answer == null) throw new ArgumentNullException(nameof(answer));
            if (_answers.Any(a => a.Id == answer.Id))
                throw new InvalidOperationException($"Answer with ID {answer.Id} already exists.");

            _answers.Add(answer);
        }

        public ClsAnswer? GetAnswerByID(int id) => _answers.FirstOrDefault(a => a.Id == id);

        public List<ClsAnswer> GetAllAnswers() => new List<ClsAnswer>(_answers);
    }
}
