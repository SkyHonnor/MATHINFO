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
    /// Logique d'interaction pour AttributionMaterielWindow.xaml
    /// </summary>
    public partial class AttributionMaterielWindow : Window
    {
        public enum Type { Create, Update };

        public Type WindowType { get; private set; }

        public Attribution CurrentAttribution { get; set; }
        public Attribution Attribution { get; set; }

        public AttributionMaterielWindow(Attribution attribution, Type windowType)
        {
            InitializeComponent();

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
            if (Attribution.Commentaire == null)
            {
                MessageBox.Show("Commentaire n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (WindowType == Type.Create)
                Attribution.Create();

            if (WindowType == Type.Update)
            {
                if (CurrentAttribution.FKDateAttribution != Attribution.FKDateAttribution)
                {
                    CurrentAttribution.Delete();
                    Attribution.Create();
                }
                else
                    Attribution.Update();
            }
                

            DialogResult = true;
        }
    }
}
