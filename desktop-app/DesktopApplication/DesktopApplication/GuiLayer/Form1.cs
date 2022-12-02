using Data.ModelLayer;
using DesktopApplication.ControlLayer;

namespace DesktopApplication
{
    public partial class Form1 : Form
    {
        private List<Answer> answers = new List<Answer>();
        private AnswerControl answerControl = new AnswerControl();
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            answerControl.CreateAnswers(answers);
            answers.Clear();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            answers.Add(new Answer(Int32.Parse(textBox1.Text), textBox6.Text, Int32.Parse(textBox5.Text)));
            textBox1.Clear();
            textBox6.Clear();
            textBox5.Clear();
        }
    }
}