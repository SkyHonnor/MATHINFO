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
    /// Logique d'interaction pour PersonnelPage.xaml
    /// </summary>
    public partial class PersonnelPage : Page
    {
        public PersonnelPage(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Personnel personnel = new Personnel();

            PersonnelWindow personnelWindow = new PersonnelWindow(personnel, PersonnelWindow.Type.Create);
            personnelWindow.Owner = Window.GetWindow(this);

            bool result = (bool)personnelWindow.ShowDialog();

            if (result)
                applicationData.Personnels.Add(personnel);
        }

        private void Button_Click_Update(object sender, MouseButtonEventArgs e)
        {
            Personnel personnel = (Personnel)DataGrid.SelectedItem;

            PersonnelWindow personnelWindow = new PersonnelWindow((Personnel)personnel.Clone(), PersonnelWindow.Type.Create);
            personnelWindow.Owner = Window.GetWindow(this);

            bool result = (bool)personnelWindow.ShowDialog();

            if (result)
            {
                personnel.NomPersonnel = personnelWindow.Personnel.NomPersonnel;
                personnel.PrenomPersonnel = personnelWindow.Personnel.PrenomPersonnel;
                personnel.MailPersonnel = personnelWindow.Personnel.MailPersonnel;

                DataGrid.Items.Refresh();
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Personnel personnel = (Personnel)DataGrid.SelectedItem;

            personnel.Delete();

            applicationData.Personnels.Remove(personnel);
        }

        private void Show_Attributions(object sender, RoutedEventArgs e)
        {
            ApplicationData applicationData = (ApplicationData)DataContext;
            Personnel personnel = (Personnel)DataGrid.SelectedItem;

            AttributionFromPersonnelWindow attributionFromPersonnelWindow = new AttributionFromPersonnelWindow(personnel, applicationData);
            attributionFromPersonnelWindow.Owner = Window.GetWindow(this);

            attributionFromPersonnelWindow.ShowDialog();
        }
    }
}
