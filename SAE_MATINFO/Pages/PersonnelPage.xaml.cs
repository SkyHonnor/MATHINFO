using SAE_MATINFO.Model;
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
    /// Logique d'interaction pour PersonnelPage.xaml
    /// </summary>
    public partial class PersonnelPage : Page
    {
        public PersonnelPage(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
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
            ApplicationData applicationData = (ApplicationData)DataContext;
            Personnel personnel = (Personnel)DataGrid.SelectedItem;

            personnel.Delete();

            applicationData.Personnels.Remove(personnel);
        }
    }
}
