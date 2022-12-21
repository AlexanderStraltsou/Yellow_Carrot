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
using System.Xml.Linq;
using Yellow_Carrot.Data;
using Yellow_Carrot.Models;
using Yellow_Carrot.Services;

namespace Yellow_Carrot
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        private Recipe loadedRecipe;
        private int thisRecipeId;
        private List<Ingredient> ingredients = new();
        

        public DetailsWindow(int recipeId)
        {
            InitializeComponent();
            
            thisRecipeId = recipeId;

            using (AppDbContext context = new AppDbContext())
            {
                

                LoadRecipe(recipeId);
                
            }
        }


        //Loading chosen recipe with all ingredients and tags to the page for editing

        private void LoadRecipe(int recipeId)
        {
            using (AppDbContext context = new())
            {
                UnitOfWork uow = new(context);
                List<Ingredient> ingredients = uow.IngredientRepo.GetIngredients();
                loadedRecipe = new RecipeRepository(context).GetRecipes(recipeId);
                txtRecipeName.Text = loadedRecipe.Name;
                
                
                foreach (Ingredient ingredient in loadedRecipe.Ingredients)
                {
                    ListViewItem ingredientItem = new ListViewItem();
                    ingredientItem.Tag = ingredient;
                    ingredientItem.Content = $"{ingredient.Name} | {ingredient.Quantity}";
                    
                    lvIngredients.Items.Add(ingredientItem);
                }


                var RecipeTag = context.Tags.Where(x => x.TagId == loadedRecipe.TagId).ToList();

                foreach (var alltags in RecipeTag)
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = alltags;
                    item.Content = alltags.TagName;
                    lvTags.Items.Add(item);
                }

            }
        }

        //Loading Updated Ingredients From Database

        public void LoadIngredients()

        {
            using (AppDbContext context = new())
            {
                UnitOfWork uow = new(context);
                List<Ingredient> ingredients = uow.IngredientRepo.GetIngredients();

                loadedRecipe = new RecipeRepository(context).GetRecipes(thisRecipeId);

                lvIngredients.Items.Clear();

                foreach (Ingredient ingredient in loadedRecipe.Ingredients)
                {
                    ListViewItem ingredientItem = new ListViewItem();
                    ingredientItem.Tag = ingredient;
                    ingredientItem.Content = $"{ingredient.Name} | {ingredient.Quantity}";

                    lvIngredients.Items.Add(ingredientItem);
                }

                
            }
        }

        //Unlocking all the buttons for editing the recipe

        private void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
            
            
            btnUnlock.IsEnabled = false;
            btnAddIngredient.IsEnabled = true;
            btnRemoveIngredient.IsEnabled = true;
            btnSave.IsEnabled = true;
            txtRecipeName.IsEnabled = true;
            lvIngredients.IsEnabled = true;
            txtNewIngredient.IsEnabled = true;
            txtQuantity.IsEnabled = true;
            


        }

        //Back to the menu button

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }

        //Clearing the inputs

        public void ClearUi()
        {
            txtNewIngredient.Clear();
            txtQuantity.Clear();
        }


        //Adding new ingredient to the list and database

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {

            //Making sure both ingredient and quantity are typed in
            if (txtQuantity.Text.Length == 0 || txtNewIngredient.Text.Length == 0)
            {
                MessageBox.Show("Please, make sure you typed in both the ingredient name and quantity!");
            }

            else
            {

                using (AppDbContext context = new AppDbContext())
                {
                    
                    string newIngredientName = txtNewIngredient.Text;
                    string newQuantityName = txtQuantity.Text;
                    Ingredient ingredient = new();
                    Recipe recipe = new();

                    ingredient.Name = newIngredientName;
                    ingredient.Quantity = newQuantityName;
                    ingredient.RecipeId = thisRecipeId;

                    ingredients.Add(ingredient);

                    lvIngredients.Items.Add($"{ingredient.Name} | {ingredient.Quantity}");

                    new IngredientRepository(context).AddIngredient(ingredient);
                    context.SaveChanges();

                    ClearUi();

                    LoadIngredients();


                }
            }

        }

        //Double checking if the ingredient is chosen and deleting it from the list

        private void btnRemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            
            ListViewItem ingredientItem = (ListViewItem)lvIngredients.SelectedItem;


            if (lvIngredients.SelectedItem == null)
            {
                MessageBox.Show("To remove the ingredient choose one of the ingedients from the list, please!");
            }

            else
            {
                
                Ingredient ingredient = (Ingredient)ingredientItem.Tag;
                ingredients.Remove(ingredient);
                
                RemoveIngredientFromDB(ingredient);

                

            }
            
        }

        //Updating the list of ingredients

        private void UpdateIngredients()
        {
            lvIngredients.Items.Clear();

            foreach (Ingredient ingredient in loadedRecipe.Ingredients)
            {
                ListViewItem ingredientItem = new();
                ingredientItem.Content = $"{ingredient.Name} | {ingredient.Quantity}";
                ingredientItem.Tag = ingredient;
                lvIngredients.Items.Add(ingredientItem);
            }

        }

        //Remove ingredient from the database

        private void RemoveIngredientFromDB(Ingredient ingredient)
        {
            using (AppDbContext context = new AppDbContext())
            {


                IngredientRepository ingridentRepository = new(context);
                ingridentRepository.RemoveIngredient(ingredient);

                context.SaveChanges();

                UpdateIngredients();
            }

        }

        //Save changed recipe to the database and go back to the main menu

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (AppDbContext context = new())
            {
                UnitOfWork uow = new(context);

                Recipe recipe = uow.RecipeRepo.GetRecipes(thisRecipeId);

                recipe.Name = txtRecipeName.Text;
                
                uow.RecipeRepo.UpdateRecipe(recipe);
                uow.SaveChanges();

                MainWindow mainWindow = new();
                mainWindow.Show();
                Close();
                
            }
        }
    }
}
