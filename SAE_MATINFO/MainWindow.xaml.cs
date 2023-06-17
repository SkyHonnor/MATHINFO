using SAE_MATINFO.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAE_MATINFO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Categorie(object sender, RoutedEventArgs e)
        {
            applicationData.CurrentPageIndex = 0;
        }

        private void Button_Click_Materiel(object sender, RoutedEventArgs e)
        {
            applicationData.CurrentPageIndex = 1;
        }

        private void Button_Click_Personnel(object sender, RoutedEventArgs e)
        {
            applicationData.CurrentPageIndex = 2;
        }

        private void Button_Click_Attribution(object sender, RoutedEventArgs e)
        {
            applicationData.CurrentPageIndex = 3;
        }
    }
}
