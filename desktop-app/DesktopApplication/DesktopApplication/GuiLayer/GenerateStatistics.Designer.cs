namespace DesktopApplication.GuiLayer
{
    partial class GenerateStatistics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backButton = new System.Windows.Forms.Button();
            this.dropdownList = new System.Windows.Forms.ComboBox();
            this.questionLabel = new System.Windows.Forms.Label();
            this.answerStatisticsLabel = new System.Windows.Forms.Label();
            this.generateStatisticsButton = new System.Windows.Forms.Button();
            this.AnswerList = new System.Windows.Forms.ListBox();
            this.addQuestionsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Tomato;
            this.backButton.Location = new System.Drawing.Point(640, 395);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(148, 43);
            this.backButton.TabIndex = 12;
            this.backButton.Text = "Back to main menu";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // dropdownList
            // 
            this.dropdownList.FormattingEnabled = true;
            this.dropdownList.Location = new System.Drawing.Point(45, 95);
            this.dropdownList.Name = "dropdownList";
            this.dropdownList.Size = new System.Drawing.Size(175, 28);
            this.dropdownList.TabIndex = 13;
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Location = new System.Drawing.Point(45, 49);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(110, 20);
            this.questionLabel.TabIndex = 14;
            this.questionLabel.Text = "Select question";
            // 
            // answerStatisticsLabel
            // 
            this.answerStatisticsLabel.AutoSize = true;
            this.answerStatisticsLabel.Location = new System.Drawing.Point(402, 49);
            this.answerStatisticsLabel.Name = "answerStatisticsLabel";
            this.answerStatisticsLabel.Size = new System.Drawing.Size(117, 20);
            this.answerStatisticsLabel.TabIndex = 15;
            this.answerStatisticsLabel.Text = "Answer statistics";
            // 
            // generateStatisticsButton
            // 
            this.generateStatisticsButton.Location = new System.Drawing.Point(45, 154);
            this.generateStatisticsButton.Name = "generateStatisticsButton";
            this.generateStatisticsButton.Size = new System.Drawing.Size(139, 29);
            this.generateStatisticsButton.TabIndex = 16;
            this.generateStatisticsButton.Text = "Generate Statistics";
            this.generateStatisticsButton.UseVisualStyleBackColor = true;
            this.generateStatisticsButton.Click += new System.EventHandler(this.generateStatisticsButton_Click);
            // 
            // AnswerList
            // 
            this.AnswerList.FormattingEnabled = true;
            this.AnswerList.ItemHeight = 20;
            this.AnswerList.Location = new System.Drawing.Point(402, 95);
            this.AnswerList.Name = "AnswerList";
            this.AnswerList.Size = new System.Drawing.Size(150, 104);
            this.AnswerList.TabIndex = 17;
            // 
            // addQuestionsButton
            // 
            this.addQuestionsButton.Location = new System.Drawing.Point(161, 46);
            this.addQuestionsButton.Name = "addQuestionsButton";
            this.addQuestionsButton.Size = new System.Drawing.Size(124, 27);
            this.addQuestionsButton.TabIndex = 18;
            this.addQuestionsButton.Text = "Add Questions";
            this.addQuestionsButton.UseVisualStyleBackColor = true;
            this.addQuestionsButton.Click += new System.EventHandler(this.addQuestionsButton_Click);
            // 
            // GenerateStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.addQuestionsButton);
            this.Controls.Add(this.AnswerList);
            this.Controls.Add(this.generateStatisticsButton);
            this.Controls.Add(this.answerStatisticsLabel);
            this.Controls.Add(this.questionLabel);
            this.Controls.Add(this.dropdownList);
            this.Controls.Add(this.backButton);
            this.Name = "GenerateStatistics";
            this.Text = "GenerateStatistics";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button backButton;
        private ComboBox dropdownList;
        private Label questionLabel;
        private Label answerStatisticsLabel;
        private Button generateStatisticsButton;
        private ListBox AnswerList;
        private Button addQuestionsButton;
    }
}