using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.TableTest
{
    public class TableUpdateTest
    {
        private readonly DataTest.DataTest _dataTest;
        private readonly Index _index;
        private readonly Relation _relation;

        public TableUpdateTest()
        {
            _dataTest = new DataTest.DataTest();
            _relation = new Relation();
            _index = new Index();
        }

        [Fact]
        public void ShouldUpdate()
        {
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);

            var graphic = new Graphic { Title = "title1" };

            graphicTable.Insert(graphic);

            Assert.Equal(graphic.Title, graphicTable.Get(graphic.Id).Title);
            graphic.Title = "Other title";

            graphicTable.Update(graphic);

            Assert.Equal(graphic.Title, graphicTable.Get(graphic.Id).Title);
        }

        [Fact]
        public void ShouldThrowExceptionWhenUpdateWithInvalidEntity()
        {
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);

            var graphic = new Graphic { Title = "title1" };

            graphicTable.Insert(graphic);

            Assert.Equal(graphic.Title, graphicTable.Get(graphic.Id).Title);
            graphic.Title = "Other title";

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                graphicTable.Update(new Graphic());
            });

            Assert.Equal("Invalid entity",ex.Message);
            Assert.NotEqual(graphic.Title, graphicTable.Get(graphic.Id).Title);
        }
    }
}
