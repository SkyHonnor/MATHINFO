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

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {

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
