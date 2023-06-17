using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAE_MATINFO.Model;
using System;
using System.Collections.Generic;
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
            Console.WriteLine(personnel1.IdPersonnel);

            Personnel personnel2 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel2.Read();
            Console.WriteLine(personnel2.IdPersonnel);

            Assert.AreEqual(personnel1, personnel2);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Personnel personnel1 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel1.Create();

            personnel1.Delete();

            
            Personnel personnel2 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel2.Read();

            Assert.AreEqual(0, personnel2.IdPersonnel);

        }

        [TestMethod()]
        public void ReadTest()
        {

            Personnel personnel1 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel1.Create();
            Console.WriteLine(personnel1.IdPersonnel);

            Personnel personnel2 = new Personnel("HIDRI", "Imene", "hidri@gmail.com");
            personnel2.Read();
            Console.WriteLine(personnel2.IdPersonnel);

            Assert.AreEqual(personnel1, personnel2);
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

            Console.WriteLine(personnel1.IdPersonnel);
            Console.WriteLine(personnel2.IdPersonnel);

            Assert.AreEqual(personnel2, personnel1);
        }

        [TestMethod()]
        public void FindAllTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindBySelectionTest()
        {
            Assert.Fail();
        }
    }
}