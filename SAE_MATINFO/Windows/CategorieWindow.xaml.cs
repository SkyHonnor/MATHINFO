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

        public CategorieWindow(object dataContext, Type windowType)
        {
            InitializeComponent();

            DataContext = dataContext;
            WindowType = windowType;

            if (WindowType == Type.Create)
                Button.Content = "Créer une catégorie";

            if (WindowType == Type.Update)
                Button.Content = "Modifier une catégorie";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (WindowType == Type.Create)
                ((Categorie)DataContext).Create();

            if (WindowType == Type.Update)
                ((Categorie)DataContext).Update();

            DialogResult = true;
        }
    }
}
