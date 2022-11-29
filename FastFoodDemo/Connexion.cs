using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodDemo
{
    internal class Connexion
    {
        public string connetionString = null;
        public SqlConnection cnn;
        // conx create a connection to the database
        public Connexion()
        {
            connetionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IFOOD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cnn = new SqlConnection(connetionString);
            try
            {
                this.cnn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }
        
        // login user method 
        public bool login(string username, string password)
        {
            //prepare sql statement to grab the username and password from the database table user
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[user] WHERE username = @username AND password = @password", cnn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            //execute the sql statement
            SqlDataReader reader = cmd.ExecuteReader();
            // if user found return true else return false
            if (reader.Read())
            {
                // close the reader
                reader.Close();
                return true;
            }
            else
            {
                // close the reader
                reader.Close();
                return false;
            }
        }

        // register user method take username , firstname , lastname , password , email as parameters and insert them into the database table user
        public bool register(
            string username, 
            string firstname, 
            string lastname, 
            string password, 
            string email
        ){
            try{
                Console.WriteLine("registering user");
                Console.WriteLine(username);
                Console.WriteLine(firstname);
                Console.WriteLine(lastname);
                Console.WriteLine(password);
                Console.WriteLine(email);
                //prepare sql statement to insert the username , firstname , lastname , password , email into the database table user
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[user] (username, firstname, lastname, password, email) VALUES (@username, @firstname, @lastname, @password, @email)", cnn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@firstname", firstname);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);
                //execute the sql statement
                SqlDataReader reader = cmd.ExecuteReader();
                // close the reader
                reader.Close();
                return true;    
            }catch{
                return false;
            }
            
        }
        // is_username_unique method take username as parameter and check if it is unique in the database table user
        public bool usernameExists(string username)
        {
            //prepare sql statement to grab the username from the database table user
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[user] WHERE username = @username", cnn);
            cmd.Parameters.AddWithValue("@username", username);
            //execute the sql statement
            SqlDataReader reader = cmd.ExecuteReader();
            // if user found return false else return true
            if (reader.Read())
            {
                // close the reader
                reader.Close();
                return false;
            }
            else
            {
                // close the reader
                reader.Close();
                return true;
            }
        }

        // is email unique method take email as parameter and check if it is unique in the database table user
        public bool emailExists(string email)
        {
            //prepare sql statement to grab the email from the database table user
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[user] WHERE email = @email", cnn);
            cmd.Parameters.AddWithValue("@email", email);
            //execute the sql statement
            SqlDataReader reader = cmd.ExecuteReader();
            // if user found return false else return true
            if (reader.Read())
            {
                // close the reader
                reader.Close();
                return false;
            }
            else
            {
                // close the reader
                reader.Close();
                return true;
            }
        }
    }
}
