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

        private List<Answer> answers;

        public GenerateStatistics()
        {
            InitializeComponent();

            _answerControl = new AnswerControl();

            questions = new List<Question>();

            answers = new List<Answer>();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Form main = new MainMenu();
            main.Show();
            this.Hide();
        }

        private async void generateStatisticsButton_Click(object sender, EventArgs e)
        {
            string prompt = dropdownList.Text;

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

            question = await _answerControl.GetQuestionById(question.id);

            answers = question.answers;

            answers.OrderBy(a => a.answerCount).ToList();

            foreach(Answer answer in answers)
            {
                AnswerList.Items.Add(answer.text + " " + answer.answerCount);
            }
        }

        private async void addQuestionsButton_Click(object sender, EventArgs e)
        {
            questions = await _answerControl.GetQuestions();

            foreach (Question question in questions)
            {
                dropdownList.Items.Add(question.text);
            }
        }
    }
}
