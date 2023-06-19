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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAE_MATINFO.Pages
{
    /// <summary>
    /// Logique d'interaction pour MaterielPage.xaml
    /// </summary>
    public partial class MaterielPage : Page
    {
        public MaterielPage(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }

        /// <summary>
        /// Gere l'evenement de clic sur le bouton "Ajouter un materiel" et
        /// va creer un nouvel objet Materiel, ouvre une fenêtre de Materiel pour la creation
        /// en fournissant les catégories disponibles  que l'utilisateur devra choisir Une fois le Materiel cree, ce dernier va s'ajouter
        /// à la liste des Materiels de l'application et à la liste des Materiels de la catégorie correspondante.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Materiel materiel = new Materiel();

            MaterielWindow materielWindow = new MaterielWindow(materiel, applicationData.Categories, MaterielWindow.Type.Create);
            materielWindow.Owner = Window.GetWindow(this);

            bool result = materielWindow.ShowDialog().Value;

            if (result)
            {
                applicationData.Materiels.Add(materiel);
                applicationData.Categories.ToList().Find(categorie => categorie.IdCategorie == materiel.FKIdCategorie).Materiels.Add(materiel);
            }
        }


        /// <summary>
        /// Gere l'evenement de clic sur le bouton "Modifier".
        /// Recupere la categorie selectionneé dans le datagrid. Ouvre une fenetre de categorie pour la modification.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Materiel materiel = (Materiel)DataGrid.SelectedItem;

            MaterielWindow materielWindow = new MaterielWindow((Materiel)materiel.Clone(), applicationData.Categories, MaterielWindow.Type.Update);
            materielWindow.Owner = Window.GetWindow(this);

            bool result = materielWindow.ShowDialog().Value;

            if (result)
            {
                if (materiel.FKIdCategorie != materielWindow.Materiel.FKIdCategorie)
                {
                    applicationData.Categories.ToList().Find(categorie => categorie.IdCategorie == materiel.Categorie.IdCategorie).Materiels.Remove(materiel);
                    applicationData.Categories.ToList().Find(categorie => categorie.IdCategorie == materielWindow.Materiel.Categorie.IdCategorie).Materiels.Add(materielWindow.Materiel);
                }

                materiel.NomMateriel = materielWindow.Materiel.NomMateriel;
                materiel.Categorie = materielWindow.Materiel.Categorie;
                materiel.CodeBarre = materielWindow.Materiel.CodeBarre;
                materiel.ReferenceConstructeur = materielWindow.Materiel.ReferenceConstructeur;

                Filtre.Items.Refresh();
                DataGrid.Items.Refresh();
            }
        }

        /// <summary>
        /// Gere l'evenement de clic sur le bouton "Supprimer".
        /// Supprime le materiel selectionné dans le datagrid et
        /// appelle la mehode Delete() pour supprimer le materiel pour ensuite l'enlever da la liste des materiels de l'application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Materiel materiel = (Materiel)DataGrid.SelectedItem;

            MessageBoxResult result = MessageBox.Show($"Êtes vous sur de vouloir supprimer {materiel.NomMateriel} ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                materiel.Delete();
                applicationData.Materiels.Remove(materiel);

                applicationData.Categories.ToList().Find(categorie => categorie.IdCategorie == materiel.FKIdCategorie).Materiels.Remove(materiel);
            }
        }

        private void Show_Attributions(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Materiel materiel = (Materiel)DataGrid.SelectedItem;

            AttributionFromMaterielWindow attributionFromMaterielWindow = new AttributionFromMaterielWindow(materiel, applicationData);
            attributionFromMaterielWindow.Owner = Window.GetWindow(this);

            attributionFromMaterielWindow.ShowDialog();
        }
    }
}
