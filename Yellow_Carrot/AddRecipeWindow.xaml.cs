using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Yellow_Carrot.Data;
using Yellow_Carrot.Models;
using Yellow_Carrot.Services;

namespace Yellow_Carrot
{
    /// <summary>
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        private List<Ingredient> ingredients = new();
        private List<Tags> Tags = new();

        public AddRecipeWindow()
        {
            InitializeComponent();

            //Load tags from DB
            UpdateTags();

        }


        //Loading tags from the database
        private void UpdateTags()
        {
            using (AppDbContext context = new())
            {
                List<Tags> tags = new TagsRepository(context).GetTags();

                foreach (Tags Tags in tags)
                {
                    ComboBoxItem cbItem = new();
                    cbItem.Content = Tags.TagName;
                    cbItem.Tag = Tags;

                    cbTags.Items.Add(cbItem);
                }
            }
        }

        //Exit back to the main menu

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            //Making sure all info added before adding the ingredient to the list
            if (txtQuantity.Text.Length == 0 || txtNewIngredient.Text.Length == 0)
            {
                MessageBox.Show("Please, make sure you typed in both the ingredient name and quantity!");
            }

            //Adding ingredient to the list
            else
            {
                string newIngredientName = txtNewIngredient.Text;
                string newQuantityName = txtQuantity.Text;
                Ingredient ingredient = new();

                ingredient.Name = newIngredientName;
                ingredient.Quantity = newQuantityName;
                ingredients.Add(ingredient);

                lvIngredients.Items.Add($"{ingredient.Name} | {ingredient.Quantity}");

                ClearUi();
            }
            

        }

        //Clearing inputs
        public void ClearUi()
        {
            txtNewIngredient.Clear();
            txtQuantity.Clear();
            txtTags.Clear();
        }

        //Add a tag to the list and update the list

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            using (AppDbContext context = new())
            {
                string newTag = txtTags.Text;
                Tags tags = new();
                tags.TagName = newTag;
                Tags.Add(tags);
                cbTags.Items.Add($"{tags.TagName}");

                //Adding a tag to the database

                new TagsRepository(context).AddTags(tags);
                context.SaveChanges();


                //Updating UI and loading a list with updated tags
                ClearUi();
                cbTags.Items.Clear();
                UpdateTags();


            }
            
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            //Making sure all the info added correctly
            
            if (ingredients.Count == 0 || txtRecipeName.Text.Length == 0 || cbTags.SelectedItem == null)
            {
                MessageBox.Show("Please, add all the info before you save the recipe!");
            }

            //Saving recipe, chosen tag and ingredients to the database and going back to the main menu

            else
            {
                string recipe = txtRecipeName.Text;
                Tags selectedTag = (Tags)((ComboBoxItem)cbTags.SelectedItem).Tag;

                using (AppDbContext context = new())
                {
                    Recipe newRecipe = new();
                    newRecipe.Name = recipe;
                    newRecipe.Ingredients = ingredients;

                    newRecipe.TagId = selectedTag.TagId;

                    new RecipeRepository(context).AddRecipe(newRecipe);
                    context.SaveChanges();

                    MainWindow mainWindow = new();
                    mainWindow.Show();
                    Close();
                }
            }
            

        }
    }
}
