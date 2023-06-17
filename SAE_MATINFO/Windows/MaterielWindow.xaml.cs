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

        public Materiel Materiel { get; set; }
        public ObservableCollection<Categorie> Categories { get; set; }

        public MaterielWindow(Materiel materiel, ObservableCollection<Categorie> categories, Type windowType)
        {
            InitializeComponent();

            Materiel = materiel;
            Categories = categories;

            DataContext = this;
            WindowType = windowType;

            if (WindowType == Type.Create)
                Button.Content = "Créer un matériel";

            if (WindowType == Type.Update)
                Button.Content = "Modifier un matériel";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (WindowType == Type.Create)
                Materiel.Create();

            if (WindowType == Type.Update)
                Materiel.Update();

            DialogResult = true;
        }
    }
}
