using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Grpc.Core;
using System.Collections;
using System.Web;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace FastFoodDemo
{
    public partial class Form1 : Form
    {   
        // define extranal urls
        private string PREDICT_URL = "https://sea-of-food.herokuapp.com/api/v1/predict";
        // this takes a post request with a json body: {"file": "image as binary file"} 
        // returns a json response: {"prediction": [
        //      {"id": "id as string", "image":"str image url" , "name":"str name" , "percentage":"float"}, ...
        // ]}
        private string DATA_URL = "https://sea-of-food-api.herokuapp.com/recipes/recipes-list/";
        private string DATA_SEARCH_URL = "https://sea-of-food-api.herokuapp.com/recipes/recipes-list/?";
        // this takes a get request with : {"query": "search query"}
        
        
        // function that takes a string and returns a list of recipes from the api data search url 
        private List<JObject> getRecipes(string query)
        {
            // genrate wantend url
            string url = this.DATA_SEARCH_URL + "query=" + query;
            // make the get request and get the response
            // the response shape will be in this format:
            /*
            {
                "max_pages": 1,
                "data": [
                    recipe1,
                    recipe2,
                ]
            */
            // load the first two items in the data list into a list of recipes
            string response = new WebClient().DownloadString(url);
            JObject json = JObject.Parse(response);
            JArray data = (JArray)json["data"];
            // log the data length
            Console.WriteLine(data.Count);
            List<JObject> recipes = new List<JObject>();
            for (int i = 0; i < data.Count; i++)
            {
                recipes.Add((JObject)data[i]);
            }
            // log the recipes
            Console.WriteLine(recipes);
            // return the list of recipes
            return recipes;
        }
        // imageToByte
        public byte[] imageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        // string to double
        public double stringToDouble(string str)
        {
            return Double.Parse(str.Replace(".", ","));
        }
        // double to percentage
        public string doubleToPercentage(double d)
        {
            return (d * 100).ToString("0.0") + "%";
        }

        public Form1()
        {
            InitializeComponent();
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
            loadingLabel.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button2.Height;
            SidePanel.Top = button2.Top;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        // when my profile btn is clicked
        private void button7_Click(object sender, EventArgs e)
        {
            myprofile myprofile = new myprofile();
            myprofile.Show();
            this.Hide();
            
        }

        // when upload button is clicked
        private void uploadButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("upload button clicked");
            // open file dialog and show it to user
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            openFileDialog.Title = "Select an Image File";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {   
                // get image
                Image image = Image.FromFile(openFileDialog.FileName);
                // convert the image to a byte array
                byte[] img_bytes = this.imageToByte(image);
                // client to send post request
                loadingLabel.Visible = true;
                HttpClient httpClient = new HttpClient();
                // create json body as Form data
                MultipartFormDataContent formData = new MultipartFormDataContent
                {
                    // add the image as a byte array to the form data
                    {
                        new ByteArrayContent(img_bytes),
                        "file",
                        openFileDialog.FileName
                    }
                };
                // send the request withou using await
                HttpResponseMessage response = httpClient.PostAsync(PREDICT_URL, formData).Result;
                // read the response as string
                string responseString = response.Content.ReadAsStringAsync().Result;
                // print the response to the debug console
                Debug.WriteLine(responseString);
                // load the response into a json object
                JObject joResponse = JObject.Parse(responseString);
                // get the prediction array from the json object
                JArray predections = (JArray)joResponse["predections"];
                // get the first item in the prediction array
                loadingLabel.Visible = false;
                // load the data to the panel4
                // get the name of the first item
                string name = (string)predections[0]["name"];
                // get the image url of the first item
                string image_url = (string)predections[0]["image"];
                // get the percentage of the first item
                string percentage = (string)predections[0]["percentage"];
                // update the image on thumbnail1
                thumbnail1.ImageLocation = image_url;
                // update the name on titleLabel1 
                titleLabel1.Text = name;
                // update the percentage on percentageLabel1 convert it to number and multiply by 100 and add % sign
                percentageLabel1.Text = doubleToPercentage(stringToDouble(percentage));
                // get the name of the second item
                name = (string)predections[1]["name"];
                // get the image url of the first item
                image_url = (string)predections[1]["image"];
                // get the percentage of the first item
                percentage = (string)predections[1]["percentage"];
                // update the image on thumbnail1
                thumbnail2.ImageLocation = image_url;
                // update the name on titleLabel1 
                titleLabel2.Text = name;
                // update the percentage on percentageLabel1 convert it to number and multiply by 100 and add %
                percentageLabel2.Text = doubleToPercentage(stringToDouble(percentage));
            }
        }

        // when readMoreBtn1_Click we go getRecipes for the titleLabel1.Text
        private void readMoreBtn1_Click(object sender, EventArgs e)
        {
            // get the recipes for the titleLabel1.Text
            List<JObject> recipes = this.getRecipes(titleLabel1.Text);
            // pass the recipes to the recipes form and show it
            RecipesList recipesList = new RecipesList(recipes);
            recipesList.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void mySecondCustmControl1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void percentageLabel2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void loginButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            myprofile myprofile = new myprofile();
            myprofile.ShowDialog();
        }
    }
}
