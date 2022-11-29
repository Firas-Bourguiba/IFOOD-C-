using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodDemo
{
    public partial class SignupForm : Form
    {
        private Connexion dbConx = new Connexion();

        // validate email must be a valid email
        private bool validateEmail(string email)
        {
            this.error.Text = "Invalid email address.";
           try
            {
                MailAddress m = new MailAddress(email);
                return true && this.dbConx.emailExists(email);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        // validate password must be at least 8 characters
        // and must contain at least one digit, 
        //one lower case letter, one upper case letter, and one special character
        private bool validatePassword(string password)
        {
            if (password.Length < 8)
            {
                this.error.Text = "Password must be at least 8 characters.";
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                this.error.Text = "Password must contain at least one digit.";
                return false;
            }
            if (!password.Any(char.IsLower))
            {
                this.error.Text = "Password must contain at least one lower case letter.";
                return false;
            }
            if (!password.Any(char.IsUpper))
            {
                this.error.Text = "Password must contain at least one upper case letter.";
                return false;
            }
            if (!password.Any(c => !char.IsLetterOrDigit(c)))
            {
                this.error.Text = "Password must contain at least one special character.";
                return false;
            }
            return true;
        }

        // validate username must be at least 4 characters and unique in data base
        private bool validateUsername(string username)
        {
            Console.WriteLine(username);
            if (username.Length < 4)
            {
                this.error.Text = "Username must be at least 4 characters.";
                return false;
            }
            if (this.dbConx.usernameExists(username))
            {
                this.error.Text = "Username already exists.";
                return false;
            }
            return true;
        }



        public SignupForm()
        {
            InitializeComponent();
            this.error.Visible = false;
            this.error.Text = "Invalid form data.";

            // default values for testing
            this.email.Text = "Test@email.com";
            this.password.Text = "123456";
            this.username.Text = "test";
            this.firstname.Text = "test";
            this.lastname.Text = "test";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void submit_Click(object sender, EventArgs e)
        {
            string email = this.email.Text;
            string password = this.password.Text;
            string username = this.username.Text;
            string firstname = this.firstname.Text;
            string lastname = this.lastname.Text;
            
            // validate username
            // if (!this.validateUsername(username))
            // {
            //     this.error.Visible = true;
            //     return;
            // }
            // validate email
            if (!this.validateEmail(email))
            {
                this.error.Visible = true;
                return;
            }
            // validate password
            if (!this.validatePassword(password))
            {
                this.error.Visible = true;
                return;
            }
            // make the rigster command usib register method from Connexion.cs
            if (this.dbConx.register(
                username, 
                firstname,
                lastname,
                password,
                email
            )){
                // show the myprofile form
                myprofile myprofile = new myprofile();
                myprofile.Show();
                this.Hide();
            }
            else
            {
                this.error.Text = "Error while registering.";
                this.error.Visible = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click_1(object sender, EventArgs e)
        {  
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
