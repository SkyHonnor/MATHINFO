using SAE_MATINFO.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour AttributionMaterielWindow.xaml
    /// </summary>
    public partial class AttributionMaterielWindow : Window
    {
        public enum Type { Create, Update };

        public Type WindowType { get; private set; }

        public ApplicationData ApplicationData { get; private set; }

        public Attribution CurrentAttribution { get; set; }
        public Attribution Attribution { get; set; }

        public AttributionMaterielWindow(ApplicationData applicationData, Attribution attribution, Type windowType)
        {
            InitializeComponent();

            ApplicationData = applicationData;

            CurrentAttribution = attribution;
            Attribution = attribution;

            DataContext = this;
            WindowType = windowType;

            if (WindowType == Type.Create)
                Button.Content = "Créer une attribution";

            if (WindowType == Type.Update)
            {
                Button.Content = "Modifier l'attribution";
                Attribution = (Attribution)Attribution.Clone();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError((DependencyObject)Commentaire))
            {
                MessageBox.Show("Commentaire n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool find = ApplicationData.Attributions.ToList().Find(attribution =>
                attribution.FKIdPersonnel == Attribution.FKIdPersonnel &&
                attribution.FKIdMateriel == Attribution.FKIdMateriel &&
                attribution.FKDateAttribution == Attribution.FKDateAttribution
            ) != null;

            if (WindowType == Type.Create && find)
            {
                MessageBox.Show("Cette attribution existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (WindowType == Type.Create)
                CreateAttribution();

            if (WindowType == Type.Update)
            {
                if (CurrentAttribution.FKDateAttribution != Attribution.FKDateAttribution)
                {
                    if (find)
                    {
                        MessageBox.Show("Cette attribution existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    CurrentAttribution.Delete();
                    CreateAttribution();
                }
                else
                    Attribution.Update();
            }
                

            DialogResult = true;
        }

        private void CreateAttribution()
        {
            Attribution.Create();

            Attribution.Personnel = ApplicationData.Personnels.ToList().Find(personnel => personnel.IdPersonnel == Attribution.FKIdPersonnel);
            Attribution.Materiel = ApplicationData.Materiels.ToList().Find(materiel => materiel.IdMateriel == Attribution.FKIdMateriel);

            foreach (Personnel personnel in ApplicationData.Personnels)
                if (personnel.IdPersonnel == Attribution.FKIdPersonnel)
                    personnel.Attributions.Add(Attribution);

            foreach (Materiel materiel in ApplicationData.Materiels)
                if (materiel.IdMateriel == Attribution.FKIdMateriel)
                    materiel.Attributions.Add(Attribution);
        }
    }
}
