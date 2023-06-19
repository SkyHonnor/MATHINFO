using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE_MATINFO.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Text;
using System.Threading.Tasks;

namespace SAE_MATINFO.Model.Tests
{
    [TestClass()]
    public class PersonnelTests
    {

        [TestMethod()]
        public void CreateTest()
        {
            Personnel personnel1 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel1.Create();

            Personnel personnel2 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel2.Read();

            Assert.AreEqual(personnel1, personnel2, "Test de creation d'un Personnel");

        }

        [TestMethod()]
        public void DeleteTest()
        {
            Personnel personnel1 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel1.Create();

            personnel1.Delete();

            Personnel personnel2 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel2.Read();

            Assert.AreEqual(0, personnel2.IdPersonnel, "Test de suppression d'un Personnel");

        }

        [TestMethod()]
        public void ReadTest()
        {
            Personnel personnel1 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel1.Create();

            Personnel personnel2 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel2.Read();

            Assert.AreEqual(personnel1, personnel2, "Test de lecture d'un Personnel");

        }

        [TestMethod()]
        public void UpdateTest()
        {
            Personnel personnel1 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel1.Create();

            personnel1.PrenomPersonnel = "Dylan";
            personnel1.NomPersonnel = "Battig";
            personnel1.MailPersonnel = "dylanelix@gmail.com";
            personnel1.Update();
           
            Personnel personnel2 = new Personnel("BATTIG", "Dylan", "dylanelix@gmail.com");
            personnel2.Read();

            Assert.AreEqual(personnel2, personnel1, "Test de mise à jour d'un Personnel");

        }

        [TestMethod()]
        public void FindAllTest()
        {
            Personnel personnel1 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel1.Create();
            Personnel personnel2 = new Personnel("RUAULT", "Maxime", "ruaultmaxime@gmail.com");
            personnel2.Create();
            Personnel personnel3 = new Personnel("BATTIG", "Dylan", "dylan@gmail.com");
            personnel3.Create();

            ObservableCollection<Personnel> lesPersonnels = new Personnel().FindAll();
            Assert.AreEqual(3, lesPersonnels.Count, "Test de récuperation de tout les Personnel");

        }

        [TestMethod()]
        public void FindBySelectionTest()
        {
            Personnel personnel1 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel1.Create();
            Personnel personnel2 = new Personnel("RUAULT", "Maxime", "ruaultmaxime@gmail.com");
            personnel2.Create();
            Personnel personnel3 = new Personnel("BATTIG", "Dylan", "dylan@gmail.com");
            personnel3.Create();

            ObservableCollection<Personnel> lesPersonnels = new Personnel().FindBySelection("mail = 'ruaultmaxime@gmail.com'");
            Assert.AreEqual(1, lesPersonnels.Count, "Test de récuperation d'un  Personnel avec un/des critère(s) spécifique(s)");


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