using DesktopApplication.ControlLayer;
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
    public partial class LoginMenu : Form
    {
        private LoginControl loginControl;
        public LoginMenu()
        {
            loginControl = new LoginControl();  

            InitializeComponent();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            string response = await loginControl.GetToken(usernameTextBox.Text, passwordTextBox.Text);

            if (response.Equals("error"))
            {
                errorLabel.Text = "Inputed credentials are not correct";
                errorLabel.Visible = true;
            }
            else
            {
                Form menu = new MainMenu();
                menu.Show();
                this.Hide();
            }
        }
    }
}
