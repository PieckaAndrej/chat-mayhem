using DesktopApplication.ControlLayer;
using DesktopApplication.ModelLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApplication.GuiLayer
{
    public partial class GenerateStatistics : Form
    {
        private AnswerControl? _answerControl;

        private List<Question> questions;

        public GenerateStatistics()
        {
            InitializeComponent();

            List<string> prompts = new List<string>();

            questions = new List<Question>();

            questions = _answerControl.GetQuestions().Result;

            foreach (Question question in questions)
            {
                prompts.Add(question.text);
            }

            foreach (String prompt in prompts)
            {
                dropdownList.Items.Add(prompt);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Form main = new MainMenu();
            main.Show();
            this.Hide();
        }

        private void generateStatisticsButton_Click(object sender, EventArgs e)
        {
            int totalCount = 0;

            string prompt = dropdownList.SelectedValue.ToString();

            Question question = null;

            int index = 0;

            while(questions.Count > index)
            {
                if(questions[index].text.Equals(prompt))
                {
                    question = questions[index];
                    break;
                }
                else
                {
                    index++;
                }
            }

            List<Answer> answers = question.answers;

            answers.OrderBy(a => a.answerCount).ToList();

            List<string> sortedTexts = new List<string>();

            foreach(Answer answer in answers)
            {
                sortedTexts.Add(answer.text);
                totalCount += answer.answerCount;
            }

            foreach(string text in sortedTexts)
            {
                AnswerList.Items.Add(text);
            }
        }

        private async void addQuestionsButton_Click(object sender, EventArgs e)
        {
            questions = await _answerControl.GetQuestions();
        }
    }
}
