using SAE_MATINFO.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SAE_MATINFO.Windows
{
    /// <summary>
    /// Logique d'interaction pour PersonnelWindow.xaml
    /// </summary>
    public partial class PersonnelWindow : Window
    {
        public enum Type { Create, Update };

        public Type WindowType { get; private set; }
        
        public ApplicationData ApplicationData { get; private set; }
        public Personnel Personnel { get; set; }

        public PersonnelWindow(ApplicationData applicationData, Personnel personnel, Type windowType)
        {
            InitializeComponent();

            ApplicationData = applicationData;
            Personnel = personnel;

            DataContext = this;
            WindowType = windowType;

            if (WindowType == Type.Create)
                Button.Content = "Créer un personnel";

            if (WindowType == Type.Update)
            {
                Button.Content = "Modifier le personnel";
                Personnel = (Personnel)Personnel.Clone();
            }    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError((DependencyObject)NomPersonnel))
            {
                MessageBox.Show("Nom personnel n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Validation.GetHasError((DependencyObject)PrenomPersonnel))
            {
                MessageBox.Show("Prénom personnel n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (Validation.GetHasError((DependencyObject)MailPersonnel))
            {
                MessageBox.Show("Mail personnel n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool find = ApplicationData.Personnels.ToList().Find(personnel => personnel.MailPersonnel == Personnel.MailPersonnel && personnel.IdPersonnel != Personnel.IdPersonnel) != null;
            if (find)
            {
                MessageBox.Show("Mail personnel existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (WindowType == Type.Create)
            {
                Personnel.Create();

                Personnel.Attributions = new ObservableCollection<Attribution>(ApplicationData.Attributions.ToList().FindAll(attribution => attribution.FKIdMateriel == Personnel.IdPersonnel));
            }

            if (WindowType == Type.Update)
                Personnel.Update();

            DialogResult = true;
        }
    }
}
