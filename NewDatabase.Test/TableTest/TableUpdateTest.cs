using System;
using NewDatabase.Test.EntitiesTest;
using NewDatabase.Test.Helpers;
using Xunit;

namespace NewDatabase.Test.TableTest
{
    public class TableUpdateTest : TesteBase
    {
        [Fact(DisplayName = "Should Update")]
        public void ShouldUpdate()
        {
            var graphic = new Graphic {Title = "title1"};

            graphicTable.Insert(graphic);

            Assert.Equal(graphic.Title, graphicTable.Get(graphic.Id).Title);
            graphic.Title = "Other title";

            graphicTable.Update(graphic);

            Assert.Equal(graphic.Title, graphicTable.Get(graphic.Id).Title);
        }

        [Fact(DisplayName = "Should Throw Exception When Update With Invalid Entity")]
        public void ShouldThrowExceptionWhenUpdateWithInvalidEntity()
        {
            var graphic = new Graphic {Title = "title1"};

            graphicTable.Insert(graphic);

            Assert.Equal(graphic.Title, graphicTable.Get(graphic.Id).Title);
            graphic.Title = "Other title";

            var ex = Assert.Throws<ArgumentException>(() => { graphicTable.Update(new Graphic()); });

            Assert.Equal("Invalid entity", ex.Message);
            Assert.NotEqual(graphic.Title, graphicTable.Get(graphic.Id).Title);
        }
    }
}