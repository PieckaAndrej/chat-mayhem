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
    public partial class CreateQuestionPack : Form
    {
        private QuestionPack questionPack;

        private List<Question> questions;

        private AnswerControl answerControl;

        public CreateQuestionPack()
        {
            InitializeComponent();

            questions = new List<Question>();

            questionPack = new QuestionPack();

            answerControl = new AnswerControl();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Form main = new MainMenu();
            main.Show();
            this.Hide();
        }

        private void addQuestion_Click(object sender, EventArgs e)
        {
            questions.Add(new Question(addQuestionTextBox.Text));
            Questions.Items.Add(addQuestionTextBox.Text);
            addQuestionTextBox.Clear();
            nameLabel.Text = nameTextBox.Text;
        }

        private async void insertQuestionPackButton_Click(object sender, EventArgs e)
        {
            questionPack.Questions = questions;
            questionPack.Author = authorTextBox.Text;
            questionPack.Category = categoryTextBox.Text;
            questionPack.Name = nameTextBox.Text;
            questionPack.CreationDate = DateTime.Now;
            questionPack.Tags = null;
            nameLabel.Text = nameTextBox.Text;
            await answerControl.InsertQuestionPack(questionPack);
            authorTextBox.Clear();
            categoryTextBox.Clear();
            nameTextBox.Clear();
            addQuestionTextBox.Clear();
            Questions.Items.Clear();
            questions.Clear(); 
        }
    }
}
