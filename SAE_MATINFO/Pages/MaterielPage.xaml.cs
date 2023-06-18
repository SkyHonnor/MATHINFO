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

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Materiel materiel = new Materiel();

            MaterielWindow materielWindow = new MaterielWindow(materiel, applicationData.Categories, MaterielWindow.Type.Create);
            materielWindow.Owner = Window.GetWindow(this);

            bool result = (bool)materielWindow.ShowDialog();

            if (result)
            {
                applicationData.Materiels.Add(materiel);
                applicationData.Categories.ToList().Find(categorie => categorie.IdCategorie == materiel.FKIdCategorie).Materiels.Add(materiel);
            }
        }

        private void Button_Click_Update(object sender, MouseButtonEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Materiel materiel = (Materiel)DataGrid.SelectedItem;

            MaterielWindow materielWindow = new MaterielWindow((Materiel)materiel.Clone(), applicationData.Categories, MaterielWindow.Type.Create);
            materielWindow.Owner = Window.GetWindow(this);

            bool result = (bool)materielWindow.ShowDialog();

            if (result)
            {
                materiel.NomMateriel = materielWindow.Materiel.NomMateriel;
                materiel.CodeBarre = materielWindow.Materiel.CodeBarre;
                materiel.ReferenceConstructeur = materielWindow.Materiel.ReferenceConstructeur;

                DataGrid.Items.Refresh();
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Materiel materiel = (Materiel)DataGrid.SelectedItem;

            materiel.Delete();

            applicationData.Materiels.Remove(materiel);
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
