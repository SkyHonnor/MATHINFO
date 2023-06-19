using SAE_MATINFO.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace SAE_MATINFO
{
    public class ApplicationData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Page[] Pages { get; private set; }
        private int currentPageIndex;

        public ObservableCollection<Categorie> Categories { get; set; }
        public ObservableCollection<Materiel> Materiels { get; set; }
        public ObservableCollection<Personnel> Personnels { get; set; }
        public ObservableCollection<Attribution> Attributions { get; set; }

        public ApplicationData()
        {
            //--------------------------------------------------

            Categorie categorieRead = new Categorie();
            Categories = categorieRead.FindAll();

            //--------------------------------------------------

            Personnel personnelRead = new Personnel();
            Personnels = personnelRead.FindAll();

            //--------------------------------------------------

            Materiel materielRead = new Materiel();
            Materiels = materielRead.FindAll();

            foreach (Materiel materiel in Materiels)
                materiel.Categorie = Categories.ToList().Find(categorie => categorie.IdCategorie == materiel.FKIdCategorie);

            foreach (Categorie categorie in Categories)
                categorie.Materiels = new ObservableCollection<Materiel>(Materiels.ToList().FindAll(materiel => materiel.FKIdCategorie == categorie.IdCategorie));

            //--------------------------------------------------

            Attribution attributionRead = new Attribution();
            Attributions = attributionRead.FindAll();

            foreach (Attribution attribution in Attributions)
            {
                attribution.Personnel = Personnels.ToList().Find(personnel => personnel.IdPersonnel == attribution.FKIdPersonnel);
                attribution.Materiel = Materiels.ToList().Find(materiel => materiel.IdMateriel == attribution.FKIdMateriel);
            }

            foreach (Personnel personnel in Personnels)
                personnel.Attributions = new ObservableCollection<Attribution>(Attributions.ToList().FindAll(attribution => attribution.FKIdPersonnel == personnel.IdPersonnel));

            foreach (Materiel materiel in Materiels)
                materiel.Attributions = new ObservableCollection<Attribution>(Attributions.ToList().FindAll(attribution => attribution.FKIdMateriel == materiel.IdMateriel));

            //--------------------------------------------------

            Pages = new Page[]
            {
                new Pages.CategoriePage(this),
                new Pages.MaterielPage(this),
                new Pages.PersonnelPage(this),
                new Pages.AttributionPage(this),
            };

            CurrentPageIndex = 0;
        }

        public int CurrentPageIndex
        {
            get => currentPageIndex;

            set
            {
                this.currentPageIndex = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentPage"));
            }
        }

        public Page CurrentPage
        {
            get => Pages[CurrentPageIndex];
        }
    }
}
