using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodDemo
{
    public partial class LoginForm : Form
    {
        private Connexion dbConx = new Connexion();
        public LoginForm()
        {
            InitializeComponent();
            this.error.Visible = false;
            this.error.Text = "Invalid username or password.";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // submit_Click 
        private void submit_Click(object sender, EventArgs e)
        {
            // grab the login From and show it 
            string username = this.username.Text;
            string password = this.password.Text;
            // try to load user from data base if not found show error message
            
            // using the login method from Connexion.cs
            if (this.dbConx.login(username, password))
            {
                // show the myprofile form
                myprofile myprofile = new myprofile();
                myprofile.Show();
                this.Hide();
            }
            else
            {
                this.error.Visible = true;
            }
        }

        // register_Click 
        private void register_Click(object sender, EventArgs e)
        {
            SignupForm signupForm = new SignupForm();
            signupForm.Show();
        
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1();
            Form1.Show();
            this.Close();
        }
    }
}
