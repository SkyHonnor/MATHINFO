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
    /// Logique d'interaction pour CategorieWindow.xaml
    /// </summary>
    public partial class CategorieWindow : Window
    {
        public enum Type { Create, Update };

        public Type WindowType { get; private set; }

        public ApplicationData ApplicationData { get; private set; }
        public Categorie Categorie { get; set; }

        public CategorieWindow(ApplicationData applicationData, Categorie categorie, Type windowType)
        {
            InitializeComponent();

            ApplicationData = applicationData;
            Categorie = categorie;

            DataContext = this;
            WindowType = windowType;

            if (WindowType == Type.Create)
                Button.Content = "Créer une catégorie";

            if (WindowType == Type.Update)
            {
                Button.Content = "Modifier la catégorie";
                Categorie = (Categorie)Categorie.Clone();
            }
        }

        /// <summary>
        /// Gere l'evenement de clic sur le bouton "Supprimer".
        /// Si le type de la fenetre est "Create", on appelle la methode Create() de la classe Categorie.
        /// Si le type de la fenetre est "Update", on appelle la methode Update() de la classe Categorie.
        /// On definit le DialogResult comme true pour confirmer que l'operation a eu succes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Categorie.NomCategorie))
            {
                MessageBox.Show("Nom catégorie n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool find = ApplicationData.Categories.ToList().Find(categorie => categorie.NomCategorie == Categorie.NomCategorie) != null;
            if (WindowType == Type.Create && find)
            {
                MessageBox.Show("Nom catégorie existe déjà", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (WindowType == Type.Create)
            {
                Categorie.Create();

                Categorie.Materiels = new ObservableCollection<Materiel>(ApplicationData.Materiels.ToList().FindAll(materiel => materiel.FKIdCategorie == Categorie.IdCategorie));
            }

            if (WindowType == Type.Update)
                Categorie.Update();

            DialogResult = true;
        }
    }
}
