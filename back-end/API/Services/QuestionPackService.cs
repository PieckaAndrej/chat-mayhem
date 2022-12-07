using Data.DatabaseLayer;
using Data.ModelLayer;

namespace API.Services
{
    public class QuestionPackService
    {
        private IQuestionPackAccess _questionPackAccess;

        private IQuestionAccess _questionAccess;

        public QuestionPackService(IQuestionPackAccess questionPackAccess, IQuestionAccess questionAccess)
        {
            _questionPackAccess = questionPackAccess;
            _questionAccess = questionAccess;
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
                _questionAccess.InsertQuestion(question, questionPackId);
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
                _questionAccess.InsertQuestion(question, qPack.Id);
            }

            return qPack;
        }

        public async Task<QuestionPack> UpdateAsync(QuestionPack questionPack)
        {
            return await _questionPackAccess.UpdateAsync(questionPack);
        }
    }
}