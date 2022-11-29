using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodDemo
{
    public partial class RecipesList : Form
    {
        // slugs
        private JObject recipe1 = new JObject();
        private JObject recipe2 = new JObject();
        private JObject recipe3 = new JObject();
        private List<JObject> recipes;

        public RecipesList()
        {
            InitializeComponent(); 
        }

        public RecipesList(List<JObject> recipes)
        {
            InitializeComponent();
            this.error.Visible = false;
            this.recipes = recipes;
            // add the recipes to the list view
            if (recipes.Count > 0)
            {
                this.recipe1 = recipes[0];
                this.titleLabel1.Text = (string)recipe1["name"];
                this.thumbnail1.ImageLocation = (string)recipe1["thumbnail"];
            }else{
                // hide the first recipe
                this.titleLabel1.Hide();
                this.thumbnail1.Hide();
            }
            if (recipes.Count > 1)
            {
                this.recipe2 = recipes[1];
                this.titleLabel2.Text = (string)recipe2["name"];
                this.thumbnail2.ImageLocation = (string)recipe2["thumbnail"];
            }else{
                // hide the second recipe
                this.titleLabel2.Hide();
                this.thumbnail2.Hide();
            }
            if (recipes.Count > 2)
            {
                this.recipe3 = recipes[2];
                this.titleLabel3.Text = (string)recipe3["name"];
                this.thumbnail3.ImageLocation = (string)recipe3["thumbnail"];
            }else{
                // hide the third recipe
                this.titleLabel3.Hide();
                this.thumbnail3.Hide();
            }
            if (!(recipes.Count > 0))
            {
                this.error.Text = "No Recipes Found.";
                this.error.Visible = true;
            }
        }
    }
}
