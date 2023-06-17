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
    public class CategorieTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            Categorie categorie1 = new Categorie("Ordinateur");
            categorie1.Create();

            Categorie categorie2 = new Categorie("Ordinateur");
            categorie2.Read();

            Assert.AreEqual(categorie1, categorie2);

            ClearAll();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Categorie categorie1 = new Categorie("Ordinateur");
            categorie1.Create();

            categorie1.Delete();

            Categorie categorie2 = new Categorie("Ordinateur");
            categorie2.Read();

            Assert.AreEqual(0, categorie2.IdCategorie);

            ClearAll();
        }

        [TestMethod()]
        public void ReadTest()
        {
            Categorie categorie1 = new Categorie("Ordinateur");
            categorie1.Create();

            Categorie categorie2 = new Categorie("Ordinateur");
            categorie2.Read();

            Assert.AreEqual(categorie1, categorie2);

            ClearAll();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Categorie categorie1 = new Categorie("Ordinateur");
            categorie1.Create();

            categorie1.NomCategorie = "Imprimante";
            categorie1.Update();

            Categorie personnel2 = new Categorie("Imprimante");
            personnel2.Read();

            Assert.AreEqual(personnel2, categorie1);

            ClearAll();
        }

        [TestMethod()]
        public void FindAllTest()
        {
            Categorie categorie1 = new Categorie("Ordinateur");
            categorie1.Create();
            Categorie categorie2 = new Categorie("Imprimante");
            categorie2.Create();
            Categorie categorie3 = new Categorie("Informatique");
            categorie3.Create();

            ObservableCollection<Personnel> lesCategorie = new Personnel().FindAll();
            Assert.AreEqual(3, lesCategorie.Count);

            ClearAll();
        }

        [TestMethod()]
        public void FindBySelectionTest()
        {
            Categorie categorie1 = new Categorie("Ordinateur");
            categorie1.Create();
            Categorie categorie2 = new Categorie("Imprimante");
            categorie2.Create();
            Categorie categorie3 = new Categorie("Informatique");
            categorie3.Create();

            ObservableCollection<Categorie> lesCategorie = new Categorie().FindBySelection("nom_categorie = 'Imprimante'");
            Assert.AreEqual(1, lesCategorie.Count);

            ClearAll();
        }

        private void ClearAll()
        {
            ObservableCollection<Categorie> lesCategorie = new Categorie().FindAll();
            foreach (Categorie categorie in lesCategorie)
            {
                categorie.Delete();
            }
        }
    }
}