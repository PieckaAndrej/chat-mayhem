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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void insertQuestionsButton_Click(object sender, EventArgs e)
        {
            Form questions = new InsertQuestions();
            questions.Show();
            this.Hide();
        }

        private void generateStatisticsButton_Click(object sender, EventArgs e)
        {
            Form statistics = new GenerateStatistics();
            statistics.Show();
            this.Hide();
        }

        private void createQuestionPackButton_Click(object sender, EventArgs e)
        {
            Form questionPack = new CreateQuestionPack();
            questionPack.Show();
            this.Hide();
        }
    }
}
