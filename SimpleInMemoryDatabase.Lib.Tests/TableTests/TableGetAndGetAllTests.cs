using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Lib.Tests.TableTests
{
    public class TableGetAndGetAllTests : TestBase
    {
        [Fact(DisplayName = "Should Return Copy In Get Method")]
        public void ShouldReturnCopyInGetMethod()
        {
            var graphic = new Graphic {Title = "title1"};

            Db.Insert(graphic);

            var graphicByGet = Db.GetOne<Graphic>(graphic.Id);

            Assert.Equal(graphic.Title, graphicByGet.Title);
            graphicByGet.Title = "Other title";

            Assert.NotEqual(graphicByGet.Title, Db.GetOne<Graphic>(graphic.Id).Title);
        }

        [Fact(DisplayName = "Should Return Copies In GetAll Method")]
        public void ShouldReturnCopiesInGetAllMethod()
        {
            var graphic1 = new Graphic {Title = "title1"};
            var graphic2 = new Graphic {Title = "title2"};
            var graphic3 = new Graphic {Title = "title3"};

            Db.Insert(graphic1);
            Db.Insert(graphic2);
            Db.Insert(graphic3);

            var graphic1ByGet = Db.GetOne<Graphic>(graphic1.Id);
            var graphic2ByGet = Db.GetOne<Graphic>(graphic2.Id);
            var graphic3ByGet = Db.GetOne<Graphic>(graphic3.Id);

            Assert.Equal(graphic1.Title, graphic1ByGet.Title);
            Assert.Equal(graphic2.Title, graphic2ByGet.Title);
            Assert.Equal(graphic3.Title, graphic3ByGet.Title);

            graphic1ByGet.Title = "Other title1";
            graphic2ByGet.Title = "Other title2";
            graphic3ByGet.Title = "Other title3";

            Assert.NotEqual(graphic1ByGet.Title, Db.GetOne<Graphic>(graphic1.Id).Title);
            Assert.NotEqual(graphic2ByGet.Title, Db.GetOne<Graphic>(graphic2.Id).Title);
            Assert.NotEqual(graphic3ByGet.Title, Db.GetOne<Graphic>(graphic3.Id).Title);
        }
    }
}