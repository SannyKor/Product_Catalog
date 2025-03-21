using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassCatalog;
using System.Collections.Generic;



namespace CatalogTests

{
    [TestClass]
    public sealed class Product_CatalogTests
    {
        //каталог і сторедж будем створювать в кожному тесті окремо
        //private Catalog catalog;
        //private FakeStorage fakeStorage;

        [TestInitialize]
        public void Setup()
        {
            //цей метод викликаеться 1 раз перед стартом тесті,
            //нем це зараз не  потрібно закоментуем 
            //fakeStorage = new FakeStorage();
            //fakeStorage.Clear();
            //catalog = new Catalog(fakeStorage);
            
        }

        [TestMethod]
        public void AddUnit_AddedSuccesfully()
        {
            //arrange
            //в нас кожен тест атомарний, каталог новий і сторедж також
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
            Catalog catalog = new Catalog(new FakeStorage());
            //arrange
            catalog.AddUnit("продукт", "опис", 100.5, 10);
            //act
            catalog.RemoveUnit(10001);
            //assert
            Assert.AreEqual(0, catalog.Units.Count);
        }

        
    
    }

    //реалізація методів не потрібно, можна обійтись просто заглушками,
    //а краще попрактикувать з Moqu або схожой бібліотекой для моків
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
