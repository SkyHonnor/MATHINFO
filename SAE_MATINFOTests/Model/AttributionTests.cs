using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE_MATINFO.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_MATINFO.Model.Tests
{
    [TestClass()]
    public class AttributionTests
    {
        private static Categorie categorie;
        private static Materiel materiel;
        private static Materiel materiel1;
        private static Materiel materiel2;
        private static Personnel personnel;

        [TestInitialize]
        public void Initialize()
        {
            categorie = new Categorie("Ordinateur");
            categorie.Create();
            materiel = new Materiel(categorie.IdCategorie, "Pc Dell Puissant", "102012001", "REF204");
            materiel.Create();
            materiel1 = new Materiel(categorie.IdCategorie, "Pc Dell (pas ouf)", "102452001", "REF119");
            materiel1.Create();
            materiel2 = new Materiel(categorie.IdCategorie, "Pc Super Computer", "12859662", "REF856");
            materiel2.Create();
            personnel = new Personnel("Ruault", "Maxime", "uwu@uwu.fr");
            personnel.Create();
        }



        [TestMethod()]
        public void CreateTest()
        {
            Attribution attribution1 = new Attribution(personnel.IdPersonnel, materiel.IdMateriel, DateTime.Today);
            attribution1.Create();

            Attribution attribution2 = new Attribution(personnel.IdPersonnel, materiel.IdMateriel, DateTime.Today);
            attribution2.Read();

            Assert.AreEqual(attribution1, attribution2, "Test de creation d'une Attribution");

        }

        [TestMethod()]
        public void DeleteTest()
        {
            Attribution attribution1 = new Attribution(personnel.IdPersonnel, materiel.IdMateriel, DateTime.Today);
            attribution1.Create();

            attribution1.Delete();

            Attribution attribution2 = new Attribution(personnel.IdPersonnel, materiel.IdMateriel, DateTime.Today);
            attribution2.Read();

            Assert.AreEqual(attribution1, attribution2, "Test de suppression d'une Attribution");

        }

        [TestMethod()]
        public void ReadTest()
        {
            Attribution attribution1 = new Attribution(personnel.IdPersonnel, materiel.IdMateriel, DateTime.Today);
            attribution1.Create();

            Attribution attribution2 = new Attribution(personnel.IdPersonnel, materiel.IdMateriel, DateTime.Today);
            attribution2.Read();

            Assert.AreEqual(attribution1, attribution2, "Test de lecture d'une Attribution");

        }

        [TestMethod()]
        public void UpdateTest()
        {
            Attribution attribution1 = new Attribution(personnel.IdPersonnel, materiel.IdMateriel, DateTime.Today);
            attribution1.Create();

            attribution1.FKDateAttribution = DateTime.Today.AddDays(1);
            attribution1.Update();

            Attribution attribution2 = new Attribution(personnel.IdPersonnel, materiel.IdMateriel, DateTime.Today);
            attribution2.Read();

            Assert.AreEqual(attribution1, attribution2, "Test de mise à jour d'une Attribution");

        }

        [TestMethod()]
        public void FindAllTest()
        {
            Attribution attribution1 = new Attribution(personnel.IdPersonnel, materiel.IdMateriel, DateTime.Today, "test");
            attribution1.Create();

            Attribution attribution2 = new Attribution(personnel.IdPersonnel, materiel1.IdMateriel, DateTime.Today, "test2");
            attribution2.Create();

            Attribution attribution3 = new Attribution(personnel.IdPersonnel, materiel2.IdMateriel, DateTime.Today, "test3");
            attribution3.Create();

            ObservableCollection<Attribution> lesAttribution = new Attribution().FindAll();

            Assert.AreEqual(3, lesAttribution.Count, "Test de récuperation de toute les Attribution");


        }

        [TestMethod()]
        public void FindBySelectionTest()
        {
            Attribution attribution1 = new Attribution(personnel.IdPersonnel, materiel.IdMateriel, DateTime.Today, "test");
            attribution1.Create();
            Attribution attribution2 = new Attribution(personnel.IdPersonnel, materiel1.IdMateriel, DateTime.Today, "test2");
            attribution2.Create();
            Attribution attribution3 = new Attribution(personnel.IdPersonnel, materiel2.IdMateriel, DateTime.Today, "test3");
            attribution3.Create();

            ObservableCollection<Attribution> lesAttribution = new Attribution().FindBySelection("commentaire = 'test2'");
            Assert.AreEqual(1, lesAttribution.Count, "Test de récuperation d'une Attribution avec un/des critère(s) spécifique(s)");

        }


        [TestCleanup]
        public void Cleanup()
        {
            ObservableCollection<Attribution> lesAttributions = new Attribution().FindAll();
            foreach (Attribution attribution in lesAttributions)
            {
                attribution.Delete();
            }

            ObservableCollection<Materiel> lesMateriaux = new Materiel().FindAll();
            foreach (Materiel mat in lesMateriaux)
            {
                mat.Delete();
            }

            ObservableCollection<Categorie> lesCategorie = new Categorie().FindAll();
            foreach (Categorie categorie in lesCategorie)
            {
                categorie.Delete();
            }

            ObservableCollection<Personnel> lesPersonnels = new Personnel().FindAll();
            foreach (Personnel pers in lesPersonnels)
            {
                pers.Delete();
            }
        }
    }
}