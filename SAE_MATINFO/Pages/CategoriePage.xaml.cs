using SAE_MATINFO.Model;
using SAE_MATINFO.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAE_MATINFO.Pages
{
    /// <summary>
    /// Logique d'interaction pour CategoriePage.xaml
    /// </summary>
    public partial class CategoriePage : Page
    {
        public ApplicationData ApplicationData { get; private set; }

        public ICollectionView Categories { get; set; }

        public CategoriePage(ApplicationData applicationData)
        {
            InitializeComponent();

            ApplicationData = applicationData;

            Categories = CollectionViewSource.GetDefaultView(ApplicationData.Categories);
            Categories.Filter = o =>
            {
                Categorie categorie = (Categorie)o;
                return categorie.NomCategorie.IndexOf(Recherche.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            };

            DataContext = this;
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
            Categorie categorie = new Categorie();

            CategorieWindow categorieWindow = new CategorieWindow(ApplicationData, categorie, CategorieWindow.Type.Create);
            categorieWindow.Owner = Window.GetWindow(this);

            bool result = categorieWindow.ShowDialog().Value;

            if (result)
                ApplicationData.Categories.Add(categorie);
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

            if (categorie == null)
                return;

            CategorieWindow categorieWindow = new CategorieWindow(ApplicationData, categorie, CategorieWindow.Type.Update);
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
            Categorie categorie = (Categorie)DataGrid.SelectedItem;

            if (categorie == null)
                return;

            MessageBoxResult result = MessageBox.Show($"Êtes vous sur de vouloir supprimer {categorie.NomCategorie} ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                categorie.Delete();
                ApplicationData.Categories.Remove(categorie);
            }
        }

        private void Recherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            Categories.Refresh();
        }
    }
}
