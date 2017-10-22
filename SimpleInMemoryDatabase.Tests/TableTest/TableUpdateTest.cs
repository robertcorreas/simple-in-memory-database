using System;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.TableTest
{
    public class TableUpdateTest : TestBase
    {
        [Fact(DisplayName = "Should Update")]
        public void ShouldUpdate()
        {
            var graphic = new Graphic { Title = "title1" };

            Db.Insert(graphic);

            Assert.Equal(graphic.Title, Db.GetOne<Graphic>(graphic.Id).Title);
            graphic.Title = "Other title";

            Db.Update(graphic);

            Assert.Equal(graphic.Title, Db.GetOne<Graphic>(graphic.Id).Title);
        }

        [Fact(DisplayName = "Should Throw Exception When Update With Invalid Entity")]
        public void ShouldThrowExceptionWhenUpdateWithInvalidEntity()
        {
            var graphic = new Graphic { Title = "title1" };

            Db.Insert(graphic);

            Assert.Equal(graphic.Title, Db.GetOne<Graphic>(graphic.Id).Title);
            graphic.Title = "Other title";

            var ex = Assert.Throws<ArgumentException>(() => { Db.Update(new Graphic()); });

            Assert.Equal("Invalid entity", ex.Message);
            Assert.NotEqual(graphic.Title, Db.GetOne<Graphic>(graphic.Id).Title);
        }
    }
}