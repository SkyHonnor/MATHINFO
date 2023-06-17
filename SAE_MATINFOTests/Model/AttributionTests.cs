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
        [TestMethod()]
        public void CreateTest()
        {
            Attribution attribution1 = new Attribution();
            attribution1.Create();

            Attribution attribution2 = new Attribution();
            attribution2.Read();

            Assert.AreEqual(attribution1, attribution2);

            ClearAll();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
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

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.Fail();
        }

        private void ClearAll()
        {
            ObservableCollection<Attribution> lesAttribute = new Attribution().FindAll();
            foreach (Attribution attribute in lesAttribute)
            {
                attribute.Delete();
            }
        }
    }
}