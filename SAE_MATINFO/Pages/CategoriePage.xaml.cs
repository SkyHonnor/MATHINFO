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

        /// <summary>
        /// Gere l'evenement de clic sur le bouton "Ajouter une categorie" et
        /// va creer une nouvelle categorie à partir des données de l'application
        /// Ouvre une fenêtre de categorie pour la creation et ajoute la catégorie créée à la liste des categories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Categorie categorie = new Categorie();

            CategorieWindow categorieWindow = new CategorieWindow(categorie, CategorieWindow.Type.Create);
            categorieWindow.Owner = Window.GetWindow(this);

            bool result = categorieWindow.ShowDialog().Value;

            if (result)
                applicationData.Categories.Add(categorie);
        }


        /// <summary>
        /// Gere l'evenement de clic sur le bouton "Modifier".
        /// Recupere la categorie selectionneé dans le datagrid. Ouvre une fenetre de categorie pour la modification.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            Categorie categorie = (Categorie)DataGrid.SelectedItem;

            CategorieWindow categorieWindow = new CategorieWindow((Categorie)categorie.Clone(), CategorieWindow.Type.Update);
            categorieWindow.Owner = Window.GetWindow(this);

            bool result = categorieWindow.ShowDialog().Value;

            if (result)
            {
                categorie.NomCategorie = categorieWindow.Categorie.NomCategorie;
                DataGrid.Items.Refresh();
            }
        }

        /// <summary>
        /// Gere l'evenement de clic sur le bouton "Supprimer".
        /// Supprime la categorie selectionneé dans le datagrid et
        /// appelle la mehode Delete() pour supprimer la categorie pour ensuite l'enlever da la liste des categories de l'application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Categorie categorie = (Categorie)DataGrid.SelectedItem;

            MessageBoxResult result = MessageBox.Show($"Êtes vous sur de vouloir supprimer {categorie.NomCategorie} ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                categorie.Delete();
                applicationData.Categories.Remove(categorie);
            }
        }
    }
}
