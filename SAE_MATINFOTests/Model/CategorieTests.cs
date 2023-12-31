﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            Assert.AreEqual(categorie1, categorie2, "Test de creation d'une Categorie");

        }

        [TestMethod()]
        public void DeleteTest()
        {
            Categorie categorie1 = new Categorie("Ordinateur");
            categorie1.Create();

            categorie1.Delete();

            Categorie categorie2 = new Categorie("Ordinateur");
            categorie2.Read();

            Assert.AreEqual(0, categorie2.IdCategorie, "Test de suppression d'une Categorie");

        }

        [TestMethod()]
        public void ReadTest()
        {
            Categorie categorie1 = new Categorie("Ordinateur");
            categorie1.Create();

            Categorie categorie2 = new Categorie("Ordinateur");
            categorie2.Read();

            Assert.AreEqual(categorie1, categorie2, "Test de lecture d'une Categorie");

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

            Assert.AreEqual(personnel2, categorie1, "Test de mise à jour d'une Categorie");

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

            ObservableCollection<Categorie> lesCategorie = new Categorie().FindAll();
            Assert.AreEqual(3, lesCategorie.Count, "Test de récuperation de toute les Categorie");

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
            Assert.AreEqual(1, lesCategorie.Count, "Test de récuperation d'une Categorie avec un/des critère(s) spécifique(s)");

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