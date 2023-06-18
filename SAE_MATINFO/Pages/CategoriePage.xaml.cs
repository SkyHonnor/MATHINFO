using SAE_MATINFO.Model;
using SAE_MATINFO.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAE_MATINFO.Pages
{
    /// <summary>
    /// Logique d'interaction pour CategoriePage.xaml
    /// </summary>
    public partial class CategoriePage : Page
    {
        public CategoriePage(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Categorie categorie = new Categorie();

            CategorieWindow categorieWindow = new CategorieWindow(categorie, CategorieWindow.Type.Create);
            categorieWindow.Owner = Window.GetWindow(this);

            bool result = (bool)categorieWindow.ShowDialog();

            if (result)
                applicationData.Categories.Add(categorie);
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Categorie categorie = (Categorie)DataGrid.SelectedItem;

            CategorieWindow categorieWindow = new CategorieWindow((Categorie)categorie.Clone(), CategorieWindow.Type.Update);
            categorieWindow.Owner = Window.GetWindow(this);

            bool result = (bool)categorieWindow.ShowDialog();

            if (result)
            {
                categorie.NomCategorie = categorieWindow.Categorie.NomCategorie;
                DataGrid.Items.Refresh();
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Categorie categorie = (Categorie)DataGrid.SelectedItem;

            categorie.Delete();

            applicationData.Categories.Remove(categorie);
        }
    }
}
