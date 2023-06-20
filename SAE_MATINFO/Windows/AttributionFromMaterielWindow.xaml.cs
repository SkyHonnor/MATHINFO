using SAE_MATINFO.Model;
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
using System.Windows.Shapes;

namespace SAE_MATINFO.Windows
{
    /// <summary>
    /// Logique d'interaction pour AttributionFromMaterielWindow.xaml
    /// </summary>
    public partial class AttributionFromMaterielWindow : Window
    {
        public ApplicationData ApplicationData { get; private set; }

        public Materiel Materiel { get; set; }

        public ICollectionView Attributions { get; set; }
        public ICollectionView Personnels { get; set; }

        public AttributionFromMaterielWindow(ApplicationData applicationData, Materiel materiel)
        {
            InitializeComponent();

            ApplicationData = applicationData;

            Materiel = materiel;

            CollectionViewSource attributionsView = new CollectionViewSource();
            attributionsView.Source = ApplicationData.Attributions;

            Attributions = attributionsView.View;
            Attributions.Filter = o =>
            {
                Attribution attribution = (Attribution)o;
                Personnel personnel = (Personnel)DataGridPersonnels.SelectedItem;

                return attribution.FKIdMateriel == Materiel.IdMateriel && personnel != null && attribution.FKIdPersonnel == personnel.IdPersonnel;
            };

            CollectionViewSource personnelsView = new CollectionViewSource();
            personnelsView.Source = ApplicationData.Personnels;

            Personnels = personnelsView.View;
            Personnels.Filter = o =>
            {
                Personnel personnel = (Personnel)o;
                return personnel.MailPersonnel.IndexOf(Recherche.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            };

            DataContext = this;

            Title.Content = $"Attribution(s) de {Materiel.NomMateriel} ({Materiel.CodeBarre})";
        }

        private void DataGridPersonnels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Attributions.Refresh();
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            Personnel personnel = (Personnel)DataGridPersonnels.SelectedItem;

            if (personnel == null)
            {
                MessageBox.Show("Vous devez selectionner un personnel", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Attribution attribution = new Attribution(personnel.IdPersonnel, Materiel.IdMateriel, DateTime.Today);

            AttributionMaterielWindow attributionMaterielWindow = new AttributionMaterielWindow(ApplicationData, attribution, AttributionMaterielWindow.Type.Create);
            attributionMaterielWindow.Owner = this;

            bool result = attributionMaterielWindow.ShowDialog().Value;

            if (result)
            {
                ApplicationData.Attributions.Add(attribution);

                ApplicationData.Materiels.ToList().Find(materiel => materiel.IdMateriel == attribution.FKIdMateriel).Attributions.Add(attribution);
                ApplicationData.Personnels.ToList().Find(personnel => personnel.IdPersonnel == attribution.FKIdPersonnel).Attributions.Add(attribution);

                Attributions.Refresh();
            }
        }

        private void DataGridAttributions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Attribution attribution = (Attribution)DataGridAttributions.SelectedItem;

            AttributionMaterielWindow attributionMaterielWindow = new AttributionMaterielWindow(ApplicationData, attribution, AttributionMaterielWindow.Type.Update);
            attributionMaterielWindow.Owner = this;

            bool result = attributionMaterielWindow.ShowDialog().Value;

            if (result)
            {
                attribution.FKDateAttribution = attributionMaterielWindow.Attribution.FKDateAttribution;
                attribution.Commentaire = attributionMaterielWindow.Attribution.Commentaire;

                DataGridAttributions.Items.Refresh();
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Attribution attribution = (Attribution)DataGridAttributions.SelectedItem;

            MessageBoxResult result = MessageBox.Show($"Êtes vous sur de vouloir supprimer ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                attribution.Delete();

                ApplicationData.Attributions.Remove(attribution);

                ApplicationData.Materiels.ToList().Find(materiel => materiel.IdMateriel == attribution.FKIdMateriel).Attributions.Remove(attribution);
                ApplicationData.Personnels.ToList().Find(personnel => personnel.IdPersonnel == attribution.FKIdPersonnel).Attributions.Remove(attribution);

                Attributions.Refresh();
            }
        }

        private void Recherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            Personnels.Refresh();
        }
    }
}
