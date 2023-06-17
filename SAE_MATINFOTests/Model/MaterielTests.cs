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
    public class MaterielTests
    {


        [TestMethod()]
        public void MaterielTest()
        {
            Materiel materiel1 = new Materiel();
            materiel1.Create();

            Materiel materiel2 = new Materiel();
            materiel2.Read();

            Assert.AreEqual(materiel1, materiel2);

            ClearAll();
        }

        [TestMethod()]
        public void MaterielTest1()
        {
            ClearAll();
        }

        [TestMethod()]
        public void MaterielTest2()
        {
            ClearAll();
        }

        [TestMethod()]
        public void MaterielTest3()
        {
            ClearAll();
        }

        [TestMethod()]
        public void CreateTest()
        {
            ClearAll();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            ClearAll();
        }

        [TestMethod()]
        public void ReadTest()
        {
            ClearAll();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            ClearAll();
        }

        [TestMethod()]
        public void FindAllTest()
        {
            ClearAll();
        }

        [TestMethod()]
        public void FindBySelectionTest()
        {
            ClearAll();
        }

        private void ClearAll()
        {
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
        }
    }
}