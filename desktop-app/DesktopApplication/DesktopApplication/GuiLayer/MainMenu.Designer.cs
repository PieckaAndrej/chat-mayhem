namespace DesktopApplication.GuiLayer
{
    partial class MainMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.insertQuestionsButton = new System.Windows.Forms.Button();
            this.generateStatisticsButton = new System.Windows.Forms.Button();
            this.createQuestionPackButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Cooper Black", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(261, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chat Mayhem";
            // 
            // insertQuestionsButton
            // 
            this.insertQuestionsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(165)))));
            this.insertQuestionsButton.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.insertQuestionsButton.ForeColor = System.Drawing.Color.White;
            this.insertQuestionsButton.Location = new System.Drawing.Point(282, 159);
            this.insertQuestionsButton.Name = "insertQuestionsButton";
            this.insertQuestionsButton.Size = new System.Drawing.Size(243, 51);
            this.insertQuestionsButton.TabIndex = 1;
            this.insertQuestionsButton.Text = "Insert Questions";
            this.insertQuestionsButton.UseVisualStyleBackColor = false;
            this.insertQuestionsButton.Click += new System.EventHandler(this.insertQuestionsButton_Click);
            // 
            // generateStatisticsButton
            // 
            this.generateStatisticsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(165)))));
            this.generateStatisticsButton.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.generateStatisticsButton.ForeColor = System.Drawing.Color.White;
            this.generateStatisticsButton.Location = new System.Drawing.Point(282, 261);
            this.generateStatisticsButton.Name = "generateStatisticsButton";
            this.generateStatisticsButton.Size = new System.Drawing.Size(243, 51);
            this.generateStatisticsButton.TabIndex = 2;
            this.generateStatisticsButton.Text = "Generate Statistics";
            this.generateStatisticsButton.UseVisualStyleBackColor = false;
            this.generateStatisticsButton.Click += new System.EventHandler(this.generateStatisticsButton_Click);
            // 
            // createQuestionPackButton
            // 
            this.createQuestionPackButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(65)))), ((int)(((byte)(165)))));
            this.createQuestionPackButton.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.createQuestionPackButton.ForeColor = System.Drawing.Color.White;
            this.createQuestionPackButton.Location = new System.Drawing.Point(282, 364);
            this.createQuestionPackButton.Name = "createQuestionPackButton";
            this.createQuestionPackButton.Size = new System.Drawing.Size(243, 55);
            this.createQuestionPackButton.TabIndex = 3;
            this.createQuestionPackButton.Text = "Create Question Pack";
            this.createQuestionPackButton.UseVisualStyleBackColor = false;
            this.createQuestionPackButton.Click += new System.EventHandler(this.createQuestionPackButton_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(813, 474);
            this.Controls.Add(this.createQuestionPackButton);
            this.Controls.Add(this.generateStatisticsButton);
            this.Controls.Add(this.insertQuestionsButton);
            this.Controls.Add(this.label1);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button insertQuestionsButton;
        private Button generateStatisticsButton;
        private Button createQuestionPackButton;
    }
}