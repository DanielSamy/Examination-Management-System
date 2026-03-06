using D07.Questions;
using System.Text.Json;

namespace D07.DTOs
{
    public static class QuestionLoader
    {
        private static readonly Random _random = new Random();

        public static ClsQuestionList LoadRandom(string filePath, byte numberOfQuestions = 5)
        {
            if (numberOfQuestions <= 0)
                throw new ArgumentException("Number of questions must be greater than zero.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Questions file not found.");

            string json = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            
            var dtos = JsonSerializer.Deserialize<List<QuestionDto>>(json, options)
                       ?? throw new Exception("Invalid JSON format.");

            if (numberOfQuestions > dtos.Count)
                throw new ArgumentException("Requested number exceeds available questions.");

            var selectedDtos = dtos
                .OrderBy(q => _random.Next())
                .Take(numberOfQuestions)
                .ToList();

            ClsQuestionList questionList = new ClsQuestionList("QuestionsLog.txt");

            foreach (var dto in selectedDtos)
            {
                ClsQuestion question = CreateQuestion(dto);
                questionList.Add(question);
            }

            return questionList;
        }

        private static ClsQuestion CreateQuestion(QuestionDto dto)
        {
            return dto.Type switch
            {
                "TrueFalse" => new ClsTrueFalseQuestion(dto.Header, dto.Body, dto.Marks, dto.Correct ?? false),

                "ChooseOne" => CreateChooseOne(dto),

                "ChooseAll" => CreateChooseAll(dto),

                 _ => throw new NotSupportedException($"Question type {dto.Type} not supported.")
            };
        }

        private static ClsChooseOneQuestion CreateChooseOne(QuestionDto dto)
        {
            if (dto.Answers == null || dto.CorrectIds == null)
                throw new Exception("Invalid ChooseOne question format.");

            ClsAnswerList list = new ClsAnswerList();

            foreach (var answer in dto.Answers)
                list.Add(new ClsAnswer(answer.Id, answer.Text));

            var correct = list.GetAnswerByID(dto.CorrectIds.FirstOrDefault())
                          ?? throw new Exception("Correct answer ID not found.");

            return new ClsChooseOneQuestion(dto.Header, dto.Body, dto.Marks, list, correct);
        }

        private static ClsChooseAllQuestion CreateChooseAll(QuestionDto dto)
        {
            if (dto.Answers == null || dto.CorrectIds == null)
                throw new Exception("Invalid ChooseAll question format.");

            ClsAnswerList list = new ClsAnswerList();

            foreach (var ans in dto.Answers)
                list.Add(new ClsAnswer(ans.Id, ans.Text));

            var correctAnswers = dto.CorrectIds
                .Select(id => list.GetAnswerByID(id)
                    ?? throw new Exception($"Correct answer ID {id} not found."))
                .ToList();

            return new ClsChooseAllQuestion(dto.Header, dto.Body, dto.Marks, list, correctAnswers);
        }
    }
}
