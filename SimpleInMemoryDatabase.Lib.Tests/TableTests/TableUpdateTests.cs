using System;
using System.Collections.Generic;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Lib.Tests.TableTests
{
    public class TableUpdateTests : TestBase
    {
        [Fact(DisplayName = "Should Update Elements")]
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


        [Fact(DisplayName = "Should Update Multiple Elements")]
        public void ShouldUpdateMultipleItens()
        {
            var graphic1 = new Graphic { Title = "title1" };
            var graphic2 = new Graphic { Title = "title2" };

            Db.Insert(graphic1);
            Db.Insert(graphic2);

            Assert.Equal(graphic1.Title, Db.GetOne<Graphic>(graphic1.Id).Title);
            Assert.Equal(graphic2.Title, Db.GetOne<Graphic>(graphic2.Id).Title);
            graphic1.Title = "Other title 1";
            graphic2.Title = "Other title 2";


            Db.Update<Graphic>(new List<Graphic>(){graphic1, graphic2});

            Assert.Equal(graphic1.Title, Db.GetOne<Graphic>(graphic1.Id).Title);
            Assert.Equal(graphic2.Title, Db.GetOne<Graphic>(graphic2.Id).Title);
        }

        [Fact(DisplayName = "Should Update Element With Reference Secure")]
        public void ShouldUpdateWithRefereceSecure()
        {
            var graphic = new Graphic { Title = "title1" };

            Db.Insert(graphic);

            Assert.Equal(graphic.Title, Db.GetOne<Graphic>(graphic.Id).Title);
            graphic.Title = "Other title";

            Db.Update(graphic);

            graphic.Title = "aaa";

            Assert.NotEqual(graphic.Title, Db.GetOne<Graphic>(graphic.Id).Title);
        }

        [Fact(DisplayName = "Should Update Multiple Elements With Reference secure")]
        public void ShouldUpdateMultipleItensWithRefereceSecure()
        {
            var graphic1 = new Graphic { Title = "title1" };
            var graphic2 = new Graphic { Title = "title2" };

            Db.Insert(graphic1);
            Db.Insert(graphic2);

            graphic1.Title = "Other title 1";
            graphic2.Title = "Other title 2";

            Db.Update<Graphic>(new List<Graphic>() { graphic1, graphic2 });

            graphic1.Title = "aaaa";
            graphic2.Title = "aaaa";

            Assert.NotEqual(graphic1.Title, Db.GetOne<Graphic>(graphic1.Id).Title);
            Assert.NotEqual(graphic2.Title, Db.GetOne<Graphic>(graphic2.Id).Title);
        }
    }
}