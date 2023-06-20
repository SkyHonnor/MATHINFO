using SAE_MATINFO.Model;
using SAE_MATINFO.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour AttributionPage.xaml
    /// </summary>
    public partial class AttributionPage : Page
    {
        public ApplicationData ApplicationData { get; private set; }

        public ICollectionView Attributions { get; set; }
        public ICollectionView Personnels { get; set; }
        public ICollectionView Materiels { get; set; }

        public AttributionPage(ApplicationData applicationData)
        {
            InitializeComponent();

            ApplicationData = applicationData;

            Attributions = CollectionViewSource.GetDefaultView(ApplicationData.Attributions);
            Attributions.Filter = o =>
            {
                Attribution attribution = (Attribution)o;

                Personnel personnel = (Personnel)DataGridPersonnels.SelectedItem;
                Materiel materiel = (Materiel)DataGridMateriels.SelectedItem;

                if (personnel != null && materiel != null)
                    return attribution.FKIdPersonnel == personnel.IdPersonnel && attribution.FKIdMateriel == materiel.IdMateriel;

                if (personnel != null)
                    return attribution.FKIdPersonnel == personnel.IdPersonnel;
                else if (materiel != null)
                    return attribution.FKIdMateriel == materiel.IdMateriel;
                else
                    return true;
            };

            CollectionViewSource personnelsView = new CollectionViewSource();
            personnelsView.Source = ApplicationData.Personnels;

            Personnels = personnelsView.View;
            Personnels.Filter = o =>
            {
                Personnel personnel = (Personnel)o;
                return personnel.Attributions.Count > 0 && personnel.MailPersonnel.IndexOf(RecherchePersonnel.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            };

            CollectionViewSource materielsView = new CollectionViewSource();
            materielsView.Source = ApplicationData.Materiels;

            Materiels = materielsView.View;
            Materiels.Filter = o =>
            {
                Materiel materiel = (Materiel)o;
                return materiel.Attributions.Count > 0 && materiel.CodeBarre.IndexOf(RechercheMateriel.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            };

            DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridPersonnels.SelectedIndex = -1;
            DataGridMateriels.SelectedIndex = -1;

            Personnels.Refresh();
            Materiels.Refresh();
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Attribution attribution = (Attribution)DataGrid.SelectedItem;

            AttributionMaterielWindow attributionMaterielWindow = new AttributionMaterielWindow(ApplicationData, attribution, AttributionMaterielWindow.Type.Update);
            attributionMaterielWindow.Owner = Window.GetWindow(this);

            bool result = attributionMaterielWindow.ShowDialog().Value;

            if (result)
            {
                DataGrid.SelectedItem = null;

                attribution.FKDateAttribution = attributionMaterielWindow.Attribution.FKDateAttribution;
                attribution.Commentaire = attributionMaterielWindow.Attribution.Commentaire;

                DataGrid.SelectedItem = attribution;

                DataGrid.Items.Refresh();
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Attribution attribution = (Attribution)DataGrid.SelectedItem;

            MessageBoxResult result = MessageBox.Show($"Êtes vous sur de vouloir supprimer ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                attribution.Delete();

                ApplicationData.Attributions.Remove(attribution);

                ApplicationData.Materiels.ToList().Find(materiel => materiel.IdMateriel == attribution.FKIdMateriel).Attributions.Remove(attribution);
                ApplicationData.Personnels.ToList().Find(personnel => personnel.IdPersonnel == attribution.FKIdPersonnel).Attributions.Remove(attribution);

                Attributions.Refresh();
                Personnels.Refresh();
                Materiels.Refresh();
            }
        }

        private void UpdateFilter(object sender, SelectionChangedEventArgs e)
        {
            Attributions.Refresh();
        }

        private void RecherchePersonnel_TextChanged(object sender, TextChangedEventArgs e)
        {
            Personnels.Refresh();
        }

        private void RechercheMateriel_TextChanged(object sender, TextChangedEventArgs e)
        {
            Materiels.Refresh();
        }
    }
}
