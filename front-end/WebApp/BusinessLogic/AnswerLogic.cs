using WebApp.Models;
using WebApp.Services;

namespace WebApp.BusinessLogic
{
    public class AnswerLogic
    {
        public static double ANSWER_THRESHOLD = 0.62;

        public static async Task<Answer?> CheckAnswer(string answer, List<Answer> answers)
        {
            Dictionary<string, double>? similarity = await AnswerService.CheckAnswer(answer, answers);

            KeyValuePair<string, double> similar = similarity.MaxBy(similar => similar.Value);

            if (similar.Value < ANSWER_THRESHOLD)
            {
                return null;
            }

            return answers.Where(a => a.Text == similar.Key).FirstOrDefault();
        }
    }
}
