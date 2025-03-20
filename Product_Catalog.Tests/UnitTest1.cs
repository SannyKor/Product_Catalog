using Xunit;
using Moq;
using Product_Catalog;

using ClassCatalog;
using ClassCatalogLib;

namespace Product_Catalog.Tests
{
    public class UnitTest1
    {
        [Fact]


        public void Test1()
        {
            Class1 class1 = new Class1();


            //var mockStorage = new Mock<Storage>();
            //var catalog = new Catalog(mockStorage.Object);

            Assert.Equal(1, class1.add(5, 6));


        }

    }

}