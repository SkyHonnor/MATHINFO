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
using System.Windows.Shapes;

namespace SAE_MATINFO.Windows
{
    /// <summary>
    /// Logique d'interaction pour MaterielWindow.xaml
    /// </summary>
    public partial class MaterielWindow : Window
    {
        public enum Type { Create, Update };

        public Type WindowType { get; private set; }

        public ApplicationData ApplicationData { get; private set; }

        public Materiel Materiel { get; set; }
        public ObservableCollection<Categorie> Categories { get; set; }

        public MaterielWindow(ApplicationData applicationData, Materiel materiel, Type windowType)
        {
            InitializeComponent();

            ApplicationData = applicationData;

            Materiel = materiel;
            Categories = ApplicationData.Categories;

            DataContext = this;
            WindowType = windowType;

            if (WindowType == Type.Create)
                Button.Content = "Créer un matériel";

            if (WindowType == Type.Update)
            {
                Button.Content = "Modifier le matériel";
                Materiel = (Materiel)Materiel.Clone();
            }
        }


        /// <summary>
        /// Gere l'evenement de clic sur le bouton.
        /// Si le type de la fenetre est "Create", on appelle la methode Create() de la classe Materiel.
        /// Si le type de la fenetre est "Update", on appelle la methode Update() de la classe Materiel.
        /// On definit le DialogResult comme true pour confirmer que l'operation a eu succes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Materiel.NomMateriel))
            {
                MessageBox.Show("Nom materiel n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(Materiel.CodeBarre))
            {
                MessageBox.Show("Code barre n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(Materiel.ReferenceConstructeur))
            {
                MessageBox.Show("Reference constructeur n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool find = ApplicationData.Materiels.ToList().Find(materiel => materiel.CodeBarre == Materiel.CodeBarre) != null;
            if (WindowType == Type.Create && find)
            {
                MessageBox.Show("Code barre existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (WindowType == Type.Create)
                Materiel.Create();

            if (WindowType == Type.Update)
                Materiel.Update();

            DialogResult = true;
        }
    }
}
