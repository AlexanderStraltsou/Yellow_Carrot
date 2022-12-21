using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yellow_Carrot.Data;
using Yellow_Carrot.Models;
using Yellow_Carrot.Services;

namespace Yellow_Carrot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UpdateUi();
        }

        //Loading available recipes

        public void UpdateUi()
        {


            using (AppDbContext context = new AppDbContext())
            {
                List<Recipe> recipes = context.Recipes.ToList();

                foreach (Recipe recipe in recipes)
                {
                    ListViewItem item = new ListViewItem();

                    item.Tag = recipe;
                    item.Content = recipe.Name;
                    lvRecipes.Items.Add(item);

                }

            }

        }

        //Double checking if the recipe is chosen and removing it from the list and database

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lvRecipes.SelectedItem as ListViewItem;

            if (selectedItem != null)
            {
                Recipe recipe = selectedItem.Tag as Recipe;

                //Asking user if the recipe should be removed

                MessageBoxResult messageBox = MessageBox.Show("Are you sure you want to remove this recipe?", "Warning!", MessageBoxButton.YesNo);
                if (messageBox == MessageBoxResult.Yes)
                {
                    using (AppDbContext context = new AppDbContext())
                    {

                        recipe = context.Recipes.Where(x => x == recipe).FirstOrDefault();

                        context.Recipes.Remove(recipe);
                        context.SaveChanges();

                    }
                }

                lvRecipes.Items.Clear();
                UpdateUi();

            }
            else
            {
                MessageBox.Show("First select a recipe to remove!");
            }
        }

        //Opening another window to add a new recipe

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new();
            addRecipeWindow.Show();
            Close();
        }

        //Opening a chosen recipe with all details in another window

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lvRecipes.SelectedItem as ListViewItem;
            Recipe? selectedRecipe = selectedItem.Tag as Recipe;

            DetailsWindow detailsWindow = new(selectedRecipe.RecipeId);
            detailsWindow.Show();
            Close();
        }


        //Enabling "delete" and "details" buttons when the recipe is chosen

        private void lvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
            btnDetails.IsEnabled = true;
        }
    }
}
