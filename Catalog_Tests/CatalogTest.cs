using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ClassCatalog
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddUnit_AddedSuccesfully()
        {
            //arrange
            Mock<Storage> _mockStorage = new Mock<Storage>();
            _mockStorage.Setup(s => s.LoadUnits()).Returns(new List<Unit>());
            Catalog catalog = new Catalog(_mockStorage.Object);
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
            Unit expectedUnit = new Unit(10001)
            {
                Name = "продукт",
                Description = "опис",
                Price = 100.5,
                Quantity = 10
            };
            _mockStorage
                .Setup(s => s.InsertUnit("продукт", "опис", 100.5, 10))
                .Returns(expectedUnit);
            _mockStorage
                .Setup(s => s.LoadUnits())
                .Returns(new List<Unit> ());

            Catalog catalog = new Catalog(_mockStorage.Object);
            
            //act            
            catalog.AddUnit("продукт", "опис", 100.5, 10);

            //assert
            Assert.AreEqual (1, catalog.Units.Count);
            Unit unit = catalog.Units[0]; 

            Assert.AreEqual(10001, unit.Id);
            Assert.AreEqual("продукт", unit.Name);
            Assert.AreEqual("опис", unit.Description);
            Assert.AreEqual(100.5, unit.Price);
            Assert.AreEqual(10, unit.Quantity);
        }

        [TestMethod]
        public void RemoveUnit_ShouldUnitDecreas_WhenUnitExists()
        {
            //arrange
            Mock<Storage> _mockStorage = new Mock<Storage>();
            Unit expectedUnit = new Unit(10001)
            {
                Name = "продукт",
                Description = "опис",
                Price = 100.5,
                Quantity = 10
            };
            _mockStorage
                .Setup(s => s.InsertUnit("продукт", "опис", 100.5, 10))
                .Returns(expectedUnit);
            _mockStorage
                .Setup(s => s.LoadUnits())
                .Returns(new List<Unit>());
            _mockStorage
                .Setup(s => s.RemoveUnit(10001))
                .Returns(true);
            Catalog catalog = new Catalog(_mockStorage.Object);            
            catalog.AddUnit("продукт", "опис", 100.5, 10);
            int count = catalog.Units.Count;
            //act
            catalog.RemoveUnit(10001);
            //assert
            Assert.AreEqual(count - 1, catalog.Units.Count);
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

