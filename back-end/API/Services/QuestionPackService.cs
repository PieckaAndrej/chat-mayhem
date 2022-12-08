using Data.DatabaseLayer;
using Data.ModelLayer;

namespace API.Services
{
    public class QuestionPackService
    {
        private IQuestionPackAccess _questionPackAccess;

        private QuestionService _questionService;

        public QuestionPackService(IQuestionPackAccess questionPackAccess)
        {
            _questionPackAccess = questionPackAccess;
            _questionService = ServiceInjector.QuestionService;
        }

        public QuestionPack GetQuestionPackById(int id)
        {
            return _questionPackAccess.GetQuestionPackById(id);
        }

        public QuestionPack UpdateQuestionPack(int id, QuestionPack questionPack)
        {
            return _questionPackAccess.UpdateQuestionPack(id, questionPack);
        }

        public QuestionPack CreateQuestionPack(QuestionPack questionPack)
        {
            QuestionPack qPack = _questionPackAccess.CreateQuestionPack(questionPack);

            List<Question> questions = qPack.Questions;

            int questionPackId = qPack.Id;

            foreach(Question question in questions)
            {
                _questionService.Insert(question, questionPackId);
            }
            
            return qPack;
        }

        public bool DeleteQuestionPack(int id)
        {
            return _questionPackAccess.DeleteQuestionPack(id);
        }

        public List<QuestionPack> GetAllQuestionPacks()
        {
            return _questionPackAccess.GetAllQuestionPacks();
        }

        public async Task<QuestionPack> InsertAsync(QuestionPack questionPack)
        {
            QuestionPack qPack = await _questionPackAccess.InsertAsync(questionPack);

            foreach (Question question in qPack.Questions)
            {
                _questionService.Insert(question, qPack.Id);
            }

            return qPack;
        }

        public async Task<QuestionPack> UpdateAsync(QuestionPack questionPack)
        {
            foreach (var question in questionPack.Questions)
            {

            }
            return await _questionPackAccess.UpdateAsync(questionPack);
        }
    }
}