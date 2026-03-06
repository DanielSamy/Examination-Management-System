namespace D07.DTOs
{
    public class QuestionDto
    {
        public string Type { get; set; } = "";
        public string Header { get; set; } = "";
        public string Body { get; set; } = "";
        public short Marks { get; set; }

        public bool? Correct { get; set; }//T/F

        public List<AnswerDto>? Answers { get; set; }//One/All
        public List<int>? CorrectIds { get; set; }
    }
}
