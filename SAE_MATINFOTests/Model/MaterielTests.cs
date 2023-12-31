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
    public class MaterielTests
    {
        private static Categorie categorie;

        [TestInitialize()]
        public void Initialize()
        {
            categorie = new Categorie("Ordinateur");
            categorie.Create();
        }

        [TestMethod()]
        public void CreateTest()
        {
            Materiel materiel1 = new Materiel(categorie.IdCategorie, "PC Dell (pas ouf)", "1010101010", "REF604");
            materiel1.Create();

            Materiel materiel2 = new Materiel(categorie.IdCategorie, "PC Dell (pas ouf)", "1010101010", "REF604");
            materiel2.Read();

            Assert.AreEqual(materiel1, materiel2, "Test de creation d'un Materiel");

        }

        [TestMethod()]
        public void DeleteTest()
        {
            Materiel materiel1 = new Materiel(categorie.IdCategorie, "PC Dell (pas ouf)", "1010101010", "REF604");
            materiel1.Create();

            materiel1.Delete();

            Materiel materiel2 = new Materiel(categorie.IdCategorie, "PC Dell (pas ouf)", "1010101010", "REF604");
            materiel2.Read();

            Assert.AreEqual(0, materiel2.IdMateriel, "Test de suppression d'un Materiel");

        }

        [TestMethod()]
        public void ReadTest()
        {
            Materiel materiel1 = new Materiel(categorie.IdCategorie, "PC Dell (pas ouf)", "1010101010", "REF604");
            materiel1.Create();

            Materiel materiel2 = new Materiel(categorie.IdCategorie, "PC Dell (pas ouf)", "1010101010", "REF604");
            materiel2.Read();

            Assert.AreEqual(materiel1, materiel2, "Test de lecture d'un Materiel");

        }

        [TestMethod()]
        public void UpdateTest()
        {
            Materiel materiel1 = new Materiel(categorie.IdCategorie, "PC Dell (pas ouf)", "1010101010", "REF604");
            materiel1.Create();

            materiel1.NomMateriel = "Imprimante";
            materiel1.Update();

            Materiel materiel2 = new Materiel(categorie.IdCategorie, "Imprimante", "1010101010", "REF604");
            materiel2.Read();

            Assert.AreEqual(materiel1, materiel2, "Test de mise à jour d'un Materiel");
        }

        [TestMethod()]
        public void FindAllTest()
        {
            Materiel materiel1 = new Materiel(categorie.IdCategorie, "PC Dell (pas ouf)", "1010101010", "REF604");
            materiel1.Create();
            Materiel materiel2 = new Materiel(categorie.IdCategorie, "Imprimante", "4423123", "refdgfdg");
            materiel2.Create();
            Materiel materiel3 = new Materiel(categorie.IdCategorie, "PC de ouf", "231312123", "fgdfgdfg");
            materiel3.Create();

            ObservableCollection<Materiel> lesMateriels = new Materiel().FindAll();
            foreach(Materiel mat in lesMateriels)
            {
                Console.WriteLine(mat.NomMateriel);
            }
            Assert.AreEqual(3, lesMateriels.Count, "Test de récuperation de tout les Materiel");

        }

        [TestMethod()]
        public void FindBySelectionTest()
        {
            Materiel materiel1 = new Materiel(categorie.IdCategorie, "PC Dell (pas ouf)", "1010101010", "REF604");
            materiel1.Create();
            Materiel materiel2 = new Materiel(categorie.IdCategorie, "Imprimante", "4423123", "refdgfdg");
            materiel2.Create();
            Materiel materiel3 = new Materiel(categorie.IdCategorie, "PC de ouf", "231312123", "fgdfgdfg");
            materiel3.Create();

            ObservableCollection<Materiel> lesMateriels = new Materiel().FindBySelection("nom_materiel = 'PC de ouf'");

            Assert.AreEqual(1, lesMateriels.Count, "Test de récuperation d'un Materiel avec un/des critère(s) spécifique(s)");
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