using DesktopApplication.ControlLayer;
using DesktopApplication.GuiLayer;
using DesktopApplication.ModelLayer;

namespace DesktopApplication
{
    public partial class InsertQuestions : Form
    {
        private AnswerControl answerControl = new AnswerControl();
        private Question? question = null;
        public InsertQuestions()
        {
            InitializeComponent();
        }

        private void insertQuestionButton_Click(object sender, EventArgs e)
        {
            if (question != null)
            {
                try
                {
                    answerControl.InsertAnswers(question);
                    question = null;
                    answerPointsTextBox.Enabled = false;
                    answerTextTextBox.Enabled = false;
                    answerPointsLabel.Enabled = true;
                    AnswerTextLabel.Enabled = true;
                    insertQuestionButton.Enabled = false;
                    addAnswerButton.Enabled = false;
                }
                catch (Exception exception)
                {
                    var popup = new PopupForm(exception.Message);
                }
            }
        }

        private void addAnswersButton_Click(object sender, EventArgs e)
        {
            if (question != null && question.answers != null)
            {
                question.answers.Add(new Answer(Int32.Parse(answerPointsTextBox.Text), answerTextTextBox.Text));
                answerPointsTextBox.Clear();
                answerTextTextBox.Clear();
            }
        }

        private void AddQuestion_Click(object sender, EventArgs e)
        {
            if (question != null)
            {
                var popup = new PopupForm("By doing this you will delete the current " +
                    "question without adding it to the database.");
                DialogResult dialogResult = popup.ShowDialog();
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            question = new Question(Int32.Parse(questionIdTextBox.Text), questionPromptTextBox.Text, new List<Answer>());
            questionIdTextBox.Clear();
            questionPromptTextBox.Clear();
            answerPointsTextBox.Enabled = true;
            answerTextTextBox.Enabled = true;
            answerPointsLabel.Enabled = true;
            AnswerTextLabel.Enabled = true;
            insertQuestionButton.Enabled = true;
            addAnswerButton.Enabled = true;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Form main = new MainMenu();
            main.Show();
            this.Hide();
        }
    }
}