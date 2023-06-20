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
    /// Logique d'interaction pour PersonnelPage.xaml
    /// </summary>
    public partial class PersonnelPage : Page
    {
        public ApplicationData ApplicationData { get; private set; }
        public ICollectionView Personnels { get; set; }

        public PersonnelPage(ApplicationData applicationData)
        {
            InitializeComponent();

            ApplicationData = applicationData;

            Personnels = CollectionViewSource.GetDefaultView(ApplicationData.Personnels);
            Personnels.Filter = o =>
            {
                Personnel personnel = (Personnel)o;
                return personnel.MailPersonnel.IndexOf(Recherche.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            };

            DataContext = this;
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            Personnel personnel = new Personnel();

            PersonnelWindow personnelWindow = new PersonnelWindow(ApplicationData, personnel, PersonnelWindow.Type.Create);
            personnelWindow.Owner = Window.GetWindow(this);

            bool result = personnelWindow.ShowDialog().Value;

            if (result)
                ApplicationData.Personnels.Add(personnel);
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            Personnel personnel = (Personnel)DataGrid.SelectedItem;

            if (personnel == null)
                return;

            PersonnelWindow personnelWindow = new PersonnelWindow(ApplicationData, personnel, PersonnelWindow.Type.Update);
            personnelWindow.Owner = Window.GetWindow(this);

            bool result = personnelWindow.ShowDialog().Value;

            if (result)
            {
                DataGrid.SelectedItem = null;

                personnel.NomPersonnel = personnelWindow.Personnel.NomPersonnel;
                personnel.PrenomPersonnel = personnelWindow.Personnel.PrenomPersonnel;
                personnel.MailPersonnel = personnelWindow.Personnel.MailPersonnel;

                DataGrid.SelectedItem = personnel;

                DataGrid.Items.Refresh();
            }
        }


        /// <summary>
        /// Gere l'evenement de clic sur le bouton "Supprimer".
        /// Supprime le personnel selectionné dans le datagrid et
        /// appelle la mehode Delete() pour supprimer le personnel pour ensuite l'enlever da la liste des personnels de l'application. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Personnel personnel = (Personnel)DataGrid.SelectedItem;

            if (personnel == null)
                return;

            MessageBoxResult result = MessageBox.Show($"Êtes vous sur de vouloir supprimer {personnel.NomPersonnel} {personnel.PrenomPersonnel} ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                personnel.Delete();
                ApplicationData.Personnels.Remove(personnel);
            }
        }

        private void Show_Attributions(object sender, MouseButtonEventArgs e)
        {
            Personnel personnel = (Personnel)DataGrid.SelectedItem;

            if (personnel == null)
                return;

            AttributionFromPersonnelWindow attributionFromPersonnelWindow = new AttributionFromPersonnelWindow(ApplicationData, personnel);
            attributionFromPersonnelWindow.Owner = Window.GetWindow(this);

            attributionFromPersonnelWindow.ShowDialog();
        }

        private void Recherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            Personnels.Refresh();
        }
    }
}
