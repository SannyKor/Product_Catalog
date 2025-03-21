using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassCatalog;
using System.Collections.Generic;



namespace CatalogTests

{
    [TestClass]
    public sealed class Product_CatalogTests
    {
        

        [TestInitialize]
        

        [TestMethod]
        public void AddUnit_AddedSuccesfully()
        {
            //arrange
            Catalog catalog = new Catalog(new FakeStorage());

            //act
            catalog.AddUnit("продукт", "опис", 100.5, 10);

            //assert            
            Assert.AreEqual(1, catalog.Units.Count);
            
        }
        [TestMethod]
        public void AddUnit_ValidData()
        {
            //arrange
            Catalog catalog = new Catalog(new FakeStorage());
            //act
            catalog.AddUnit("продукт", "опис", 100.5, 10);

            //assert                     
            Assert.AreEqual(10001, catalog.Units[0].Id);
            Assert.AreEqual("продукт", catalog.Units[0].Name);
            Assert.AreEqual("опис", catalog.Units[0].Description);
            Assert.AreEqual(100.5, catalog.Units[0].Price);
            Assert.AreEqual(10, catalog.Units[0].Quantity);
        }

        [TestMethod]
        public void TestMethod2() 
        {
            //arrange
            Catalog catalog = new Catalog(new FakeStorage());
            catalog.AddUnit("продукт", "опис", 100.5, 10);
            //act
            catalog.RemoveUnit(10001);
            //assert
            Assert.AreEqual(0, catalog.Units.Count);
        }

        
    
    }
    public class FakeStorage : Storage
    {
        //private List<Unit> _units = new List<Unit>();
        public override List<Unit> LoadUnits()
        {
            return new List<Unit>();
        }
        public override void SaveUnits(List<Unit> units)
        {
            //_units = new List<Unit>(units);
        }

        //public void Clear() => _units.Clear();

    }
}
