﻿using SAE_MATINFO.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace SAE_MATINFO.Windows
{
    /// <summary>
    /// Logique d'interaction pour AttributionFromPersonnelWindow.xaml
    /// </summary>
    public partial class AttributionFromPersonnelWindow : Window
    {
        public ApplicationData ApplicationData { get; private set; }

        public Personnel Personnel { get; set; }

        public ICollectionView Attributions { get; set; }
        public ICollectionView Materiels { get; set; }

        public AttributionFromPersonnelWindow(ApplicationData applicationData, Personnel personnel)
        {
            InitializeComponent();

            ApplicationData = applicationData;

            Personnel = personnel;

            CollectionViewSource attributionsView = new CollectionViewSource();
            attributionsView.Source = ApplicationData.Attributions;

            Attributions = attributionsView.View;
            Attributions.Filter = o =>
            {
                Attribution attribution = (Attribution)o;
                Materiel materiel = (Materiel)DataGridMateriels.SelectedItem;

                return attribution.FKIdPersonnel == Personnel.IdPersonnel && materiel != null && attribution.FKIdMateriel == materiel.IdMateriel;
            };


            CollectionViewSource materielsView = new CollectionViewSource();
            materielsView.Source = ApplicationData.Materiels;

            Materiels = materielsView.View;
            Materiels.Filter = o =>
            {
                Materiel materiel = (Materiel)o;
                return materiel.CodeBarre.IndexOf(Recherche.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            };

            DataContext = this;

            Title.Content = $"Attribution(s) de {Personnel.NomPersonnel} {Personnel.PrenomPersonnel} ({Personnel.MailPersonnel})";
        }

        private void DataGridMateriels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Attributions.Refresh();
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            Materiel materiel = (Materiel)DataGridMateriels.SelectedItem;

            if (materiel == null)
            {
                MessageBox.Show("Vous devez selectionner un materiel", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Attribution attribution = new Attribution(Personnel.IdPersonnel, materiel.IdMateriel, DateTime.Today);

            AttributionMaterielWindow attributionMaterielWindow = new AttributionMaterielWindow(ApplicationData, attribution, AttributionMaterielWindow.Type.Create);
            attributionMaterielWindow.Owner = this;

            bool result = attributionMaterielWindow.ShowDialog().Value;

            if (result)
            {
                ApplicationData.Attributions.Add(attribution);

                ApplicationData.Materiels.ToList().Find(materiel => materiel.IdMateriel == attribution.FKIdMateriel).Attributions.Add(attribution);
                ApplicationData.Personnels.ToList().Find(personnel => personnel.IdPersonnel == attribution.FKIdPersonnel).Attributions.Add(attribution);

                Attributions.Refresh();
            }
        }

        private void DataGridAttributions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Attribution attribution = (Attribution)DataGridAttributions.SelectedItem;

            AttributionMaterielWindow attributionMaterielWindow = new AttributionMaterielWindow(ApplicationData, attribution, AttributionMaterielWindow.Type.Update);
            attributionMaterielWindow.Owner = this;

            bool result = attributionMaterielWindow.ShowDialog().Value;

            if (result)
            {
                DataGridAttributions.SelectedItem = null;

                attribution.FKDateAttribution = attributionMaterielWindow.Attribution.FKDateAttribution;
                attribution.Commentaire = attributionMaterielWindow.Attribution.Commentaire;

                DataGridAttributions.SelectedItem = attribution;

                DataGridAttributions.Items.Refresh();
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Attribution attribution = (Attribution)DataGridAttributions.SelectedItem;

            MessageBoxResult result = MessageBox.Show($"Êtes vous sur de vouloir supprimer ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                attribution.Delete();

                ApplicationData.Attributions.Remove(attribution);

                ApplicationData.Materiels.ToList().Find(materiel => materiel.IdMateriel == attribution.FKIdMateriel).Attributions.Remove(attribution);
                ApplicationData.Personnels.ToList().Find(personnel => personnel.IdPersonnel == attribution.FKIdPersonnel).Attributions.Remove(attribution);

                Attributions.Refresh();
            }
        }

        private void Recherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            Attributions.Refresh();
        }
    }
}
