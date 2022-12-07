namespace DesktopApplication
{
    partial class InsertQuestions
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.answerPointsTextBox = new System.Windows.Forms.TextBox();
            this.questionIdTextBox = new System.Windows.Forms.TextBox();
            this.answerTextTextBox = new System.Windows.Forms.TextBox();
            this.answerPointsLabel = new System.Windows.Forms.Label();
            this.AnswerTextLabel = new System.Windows.Forms.Label();
            this.questionIdLabel = new System.Windows.Forms.Label();
            this.addAnswerButton = new System.Windows.Forms.Button();
            this.insertQuestionButton = new System.Windows.Forms.Button();
            this.addQuestionButton = new System.Windows.Forms.Button();
            this.questionPromptLabel = new System.Windows.Forms.Label();
            this.questionPromptTextBox = new System.Windows.Forms.TextBox();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // answerPointsTextBox
            // 
            this.answerPointsTextBox.Enabled = false;
            this.answerPointsTextBox.Location = new System.Drawing.Point(526, 124);
            this.answerPointsTextBox.Name = "answerPointsTextBox";
            this.answerPointsTextBox.Size = new System.Drawing.Size(131, 27);
            this.answerPointsTextBox.TabIndex = 0;
            // 
            // questionIdTextBox
            // 
            this.questionIdTextBox.Location = new System.Drawing.Point(142, 88);
            this.questionIdTextBox.Name = "questionIdTextBox";
            this.questionIdTextBox.Size = new System.Drawing.Size(132, 27);
            this.questionIdTextBox.TabIndex = 1;
            // 
            // answerTextTextBox
            // 
            this.answerTextTextBox.Enabled = false;
            this.answerTextTextBox.Location = new System.Drawing.Point(526, 88);
            this.answerTextTextBox.Name = "answerTextTextBox";
            this.answerTextTextBox.Size = new System.Drawing.Size(131, 27);
            this.answerTextTextBox.TabIndex = 2;
            // 
            // answerPointsLabel
            // 
            this.answerPointsLabel.AutoSize = true;
            this.answerPointsLabel.Enabled = false;
            this.answerPointsLabel.Location = new System.Drawing.Point(417, 127);
            this.answerPointsLabel.Name = "answerPointsLabel";
            this.answerPointsLabel.Size = new System.Drawing.Size(103, 20);
            this.answerPointsLabel.TabIndex = 3;
            this.answerPointsLabel.Text = "Answer Points:";
            // 
            // AnswerTextLabel
            // 
            this.AnswerTextLabel.AutoSize = true;
            this.AnswerTextLabel.Enabled = false;
            this.AnswerTextLabel.Location = new System.Drawing.Point(429, 91);
            this.AnswerTextLabel.Name = "AnswerTextLabel";
            this.AnswerTextLabel.Size = new System.Drawing.Size(91, 20);
            this.AnswerTextLabel.TabIndex = 4;
            this.AnswerTextLabel.Text = "Answer Text:";
            // 
            // questionIdLabel
            // 
            this.questionIdLabel.AutoSize = true;
            this.questionIdLabel.Location = new System.Drawing.Point(44, 91);
            this.questionIdLabel.Name = "questionIdLabel";
            this.questionIdLabel.Size = new System.Drawing.Size(88, 20);
            this.questionIdLabel.TabIndex = 5;
            this.questionIdLabel.Text = "Question Id:";
            // 
            // addAnswerButton
            // 
            this.addAnswerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(165)))));
            this.addAnswerButton.Enabled = false;
            this.addAnswerButton.ForeColor = System.Drawing.Color.White;
            this.addAnswerButton.Location = new System.Drawing.Point(511, 181);
            this.addAnswerButton.Name = "addAnswerButton";
            this.addAnswerButton.Size = new System.Drawing.Size(101, 29);
            this.addAnswerButton.TabIndex = 6;
            this.addAnswerButton.Text = "Add Answer";
            this.addAnswerButton.UseVisualStyleBackColor = false;
            this.addAnswerButton.Click += new System.EventHandler(this.addAnswersButton_Click);
            // 
            // insertQuestionButton
            // 
            this.insertQuestionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(165)))));
            this.insertQuestionButton.Enabled = false;
            this.insertQuestionButton.ForeColor = System.Drawing.Color.White;
            this.insertQuestionButton.Location = new System.Drawing.Point(502, 224);
            this.insertQuestionButton.Name = "insertQuestionButton";
            this.insertQuestionButton.Size = new System.Drawing.Size(118, 29);
            this.insertQuestionButton.TabIndex = 7;
            this.insertQuestionButton.Text = "Insert Question";
            this.insertQuestionButton.UseVisualStyleBackColor = false;
            this.insertQuestionButton.Click += new System.EventHandler(this.insertQuestionButton_Click);
            // 
            // addQuestionButton
            // 
            this.addQuestionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(165)))));
            this.addQuestionButton.ForeColor = System.Drawing.Color.White;
            this.addQuestionButton.Location = new System.Drawing.Point(12, 181);
            this.addQuestionButton.Name = "addQuestionButton";
            this.addQuestionButton.Size = new System.Drawing.Size(120, 29);
            this.addQuestionButton.TabIndex = 8;
            this.addQuestionButton.Text = "Add Question";
            this.addQuestionButton.UseVisualStyleBackColor = false;
            this.addQuestionButton.Click += new System.EventHandler(this.AddQuestion_Click);
            // 
            // questionPromptLabel
            // 
            this.questionPromptLabel.AutoSize = true;
            this.questionPromptLabel.Location = new System.Drawing.Point(12, 127);
            this.questionPromptLabel.Name = "questionPromptLabel";
            this.questionPromptLabel.Size = new System.Drawing.Size(124, 20);
            this.questionPromptLabel.TabIndex = 10;
            this.questionPromptLabel.Text = "Question Prompt:";
            // 
            // questionPromptTextBox
            // 
            this.questionPromptTextBox.Location = new System.Drawing.Point(142, 124);
            this.questionPromptTextBox.Name = "questionPromptTextBox";
            this.questionPromptTextBox.Size = new System.Drawing.Size(132, 27);
            this.questionPromptTextBox.TabIndex = 9;
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Tomato;
            this.backButton.Location = new System.Drawing.Point(626, 385);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(148, 43);
            this.backButton.TabIndex = 11;
            this.backButton.Text = "Back to main menu";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // InsertQuestions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.questionPromptLabel);
            this.Controls.Add(this.questionPromptTextBox);
            this.Controls.Add(this.addQuestionButton);
            this.Controls.Add(this.insertQuestionButton);
            this.Controls.Add(this.addAnswerButton);
            this.Controls.Add(this.questionIdLabel);
            this.Controls.Add(this.AnswerTextLabel);
            this.Controls.Add(this.answerPointsLabel);
            this.Controls.Add(this.answerTextTextBox);
            this.Controls.Add(this.questionIdTextBox);
            this.Controls.Add(this.answerPointsTextBox);
            this.Name = "InsertQuestions";
            this.Text = "InsertQuestions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox answerPointsTextBox;
        private TextBox questionIdTextBox;
        private TextBox answerTextTextBox;
        private Label answerPointsLabel;
        private Label AnswerTextLabel;
        private Label questionIdLabel;
        private Button addAnswerButton;
        private Button insertQuestionButton;
        private Button addQuestionButton;
        private Label questionPromptLabel;
        private TextBox questionPromptTextBox;
        private Button backButton;
    }
}