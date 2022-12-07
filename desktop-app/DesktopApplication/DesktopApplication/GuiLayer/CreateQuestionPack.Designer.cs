namespace DesktopApplication.GuiLayer
{
    partial class CreateQuestionPack
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
            this.addQuestion = new System.Windows.Forms.Button();
            this.authorTextBox = new System.Windows.Forms.TextBox();
            this.createQuestionPackLabel = new System.Windows.Forms.Label();
            this.categoryTextBox = new System.Windows.Forms.TextBox();
            this.authorLabel = new System.Windows.Forms.Label();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.addQuestionToPackLabel = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.Questions = new System.Windows.Forms.ListBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.questionPackNameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.insertQuestionPackButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.addQuestionTextBox = new System.Windows.Forms.TextBox();
            this.questionLabel = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // addQuestion
            // 
            this.addQuestion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(165)))));
            this.addQuestion.ForeColor = System.Drawing.Color.White;
            this.addQuestion.Location = new System.Drawing.Point(226, 290);
            this.addQuestion.Name = "addQuestion";
            this.addQuestion.Size = new System.Drawing.Size(113, 34);
            this.addQuestion.TabIndex = 0;
            this.addQuestion.Text = "Add Question";
            this.addQuestion.UseVisualStyleBackColor = false;
            this.addQuestion.Click += new System.EventHandler(this.addQuestion_Click);
            // 
            // authorTextBox
            // 
            this.authorTextBox.Location = new System.Drawing.Point(171, 119);
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.Size = new System.Drawing.Size(125, 27);
            this.authorTextBox.TabIndex = 1;
            // 
            // createQuestionPackLabel
            // 
            this.createQuestionPackLabel.AutoSize = true;
            this.createQuestionPackLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.createQuestionPackLabel.Location = new System.Drawing.Point(284, 9);
            this.createQuestionPackLabel.Name = "createQuestionPackLabel";
            this.createQuestionPackLabel.Size = new System.Drawing.Size(195, 28);
            this.createQuestionPackLabel.TabIndex = 2;
            this.createQuestionPackLabel.Text = "Create question pack";
            // 
            // categoryTextBox
            // 
            this.categoryTextBox.Location = new System.Drawing.Point(171, 168);
            this.categoryTextBox.Name = "categoryTextBox";
            this.categoryTextBox.Size = new System.Drawing.Size(125, 27);
            this.categoryTextBox.TabIndex = 3;
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(111, 122);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(54, 20);
            this.authorLabel.TabIndex = 4;
            this.authorLabel.Text = "Author";
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Location = new System.Drawing.Point(96, 175);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(69, 20);
            this.categoryLabel.TabIndex = 5;
            this.categoryLabel.Text = "Category";
            // 
            // addQuestionToPackLabel
            // 
            this.addQuestionToPackLabel.AutoSize = true;
            this.addQuestionToPackLabel.Location = new System.Drawing.Point(107, 236);
            this.addQuestionToPackLabel.Name = "addQuestionToPackLabel";
            this.addQuestionToPackLabel.Size = new System.Drawing.Size(212, 20);
            this.addQuestionToPackLabel.TabIndex = 6;
            this.addQuestionToPackLabel.Text = "Add question to question pack";
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.Questions);
            this.groupBox.Controls.Add(this.nameLabel);
            this.groupBox.Location = new System.Drawing.Point(405, 71);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(289, 303);
            this.groupBox.TabIndex = 7;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Question Pack";
            // 
            // Questions
            // 
            this.Questions.FormattingEnabled = true;
            this.Questions.ItemHeight = 20;
            this.Questions.Location = new System.Drawing.Point(6, 67);
            this.Questions.Name = "Questions";
            this.Questions.Size = new System.Drawing.Size(277, 204);
            this.Questions.TabIndex = 9;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(6, 34);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(49, 20);
            this.nameLabel.TabIndex = 8;
            this.nameLabel.Text = "Name";
            // 
            // questionPackNameLabel
            // 
            this.questionPackNameLabel.AutoSize = true;
            this.questionPackNameLabel.Location = new System.Drawing.Point(21, 78);
            this.questionPackNameLabel.Name = "questionPackNameLabel";
            this.questionPackNameLabel.Size = new System.Drawing.Size(144, 20);
            this.questionPackNameLabel.TabIndex = 9;
            this.questionPackNameLabel.Text = "Question pack name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(171, 71);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(125, 27);
            this.nameTextBox.TabIndex = 8;
            // 
            // insertQuestionPackButton
            // 
            this.insertQuestionPackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(165)))));
            this.insertQuestionPackButton.ForeColor = System.Drawing.Color.White;
            this.insertQuestionPackButton.Location = new System.Drawing.Point(107, 353);
            this.insertQuestionPackButton.Name = "insertQuestionPackButton";
            this.insertQuestionPackButton.Size = new System.Drawing.Size(189, 42);
            this.insertQuestionPackButton.TabIndex = 11;
            this.insertQuestionPackButton.Text = "Insert question pack";
            this.insertQuestionPackButton.UseVisualStyleBackColor = false;
            this.insertQuestionPackButton.Click += new System.EventHandler(this.insertQuestionPackButton_Click);
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Tomato;
            this.backButton.Location = new System.Drawing.Point(640, 395);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(148, 43);
            this.backButton.TabIndex = 13;
            this.backButton.Text = "Back to main menu";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // addQuestionTextBox
            // 
            this.addQuestionTextBox.Location = new System.Drawing.Point(95, 290);
            this.addQuestionTextBox.Name = "addQuestionTextBox";
            this.addQuestionTextBox.Size = new System.Drawing.Size(125, 27);
            this.addQuestionTextBox.TabIndex = 14;
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Location = new System.Drawing.Point(21, 293);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(68, 20);
            this.questionLabel.TabIndex = 15;
            this.questionLabel.Text = "Question";
            // 
            // CreateQuestionPack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.questionLabel);
            this.Controls.Add(this.addQuestionTextBox);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.insertQuestionPackButton);
            this.Controls.Add(this.questionPackNameLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.addQuestionToPackLabel);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.categoryTextBox);
            this.Controls.Add(this.createQuestionPackLabel);
            this.Controls.Add(this.authorTextBox);
            this.Controls.Add(this.addQuestion);
            this.Name = "CreateQuestionPack";
            this.Text = "CreateQuestionPack";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button addQuestion;
        private TextBox authorTextBox;
        private Label createQuestionPackLabel;
        private TextBox categoryTextBox;
        private Label authorLabel;
        private Label categoryLabel;
        private Label addQuestionToPackLabel;
        private GroupBox groupBox;
        private Label nameLabel;
        private Label questionPackNameLabel;
        private TextBox nameTextBox;
        private ListBox Questions;
        private Button insertQuestionPackButton;
        private Button backButton;
        private TextBox addQuestionTextBox;
        private Label questionLabel;
    }
}