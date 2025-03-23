using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassCatalog;
using System.Collections.Generic;
using Moq;



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
            Mock<Storage> _mockStorage = new Mock<Storage>();
            _mockStorage.Setup(s => s.LoadUnits()).Returns(new List<Unit>());
            Catalog catalog = new(_mockStorage.Object);
            //Catalog catalog = new Catalog(new FakeStorage());

            //act
            catalog.AddUnit("продукт", "опис", 100.5, 10);

            //assert            
            Assert.AreEqual(1, catalog.Units.Count);
            
        }
        [TestMethod]
        public void AddUnit_ValidData()
        {
            //arrange
            Mock<Storage> _mockStorage = new Mock<Storage>();
            _mockStorage.Setup(s => s.LoadUnits()).Returns(new List<Unit>());
            Catalog catalog = new(_mockStorage.Object);
            //Catalog catalog = new Catalog(new FakeStorage());
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
        public void RemoveUnit_ShouldUnitDecreas_WhenUnitExists() 
        {
            //arrange
            Mock<Storage> _mockStorage = new Mock<Storage>();
            _mockStorage.Setup(s => s.LoadUnits()).Returns(new List<Unit>());
            Catalog catalog = new(_mockStorage.Object);
            //Catalog catalog = new Catalog(new FakeStorage());
            catalog.AddUnit("продукт", "опис", 100.5, 10);
            int count = catalog.Units.Count;
            //act
            catalog.RemoveUnit(10001);
            //assert
            Assert.AreEqual(count -1 , catalog.Units.Count);
        }

        
    
    }
    /*public class FakeStorage : Storage
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

    }*/
}
