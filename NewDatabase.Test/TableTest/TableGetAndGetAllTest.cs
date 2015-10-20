﻿using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.TableTest
{
    public class TableGetAndGetAllTest
    {
        private readonly DataTest.DataTest _dataTest;
        private readonly Index _index;
        private readonly Relation _relation;

        public TableGetAndGetAllTest()
        {
            _dataTest = new DataTest.DataTest();
            _relation = new Relation();
            _index = new Index();
        }

        [Fact]
        public void ShouldReturnCopyInGetMethod()
        {
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);

            var graphic = new Graphic {Title = "title1"};

            graphicTable.Insert(graphic);

            var graphicByGet = graphicTable.Get(graphic.Id);

            Assert.Equal(graphic.Title, graphicByGet.Title);
            graphicByGet.Title = "Other title";

            Assert.NotEqual(graphicByGet.Title, graphicTable.Get(graphic.Id).Title);
        }

        [Fact]
        public void ShouldReturnCopiesInGetAllMethod()
        {
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);

            var graphic1 = new Graphic {Title = "title1"};
            var graphic2 = new Graphic {Title = "title2"};
            var graphic3 = new Graphic {Title = "title3"};

            graphicTable.Insert(graphic1);
            graphicTable.Insert(graphic2);
            graphicTable.Insert(graphic3);

            var graphic1ByGet = graphicTable.Get(graphic1.Id);
            var graphic2ByGet = graphicTable.Get(graphic2.Id);
            var graphic3ByGet = graphicTable.Get(graphic3.Id);

            Assert.Equal(graphic1.Title, graphic1ByGet.Title);
            Assert.Equal(graphic2.Title, graphic2ByGet.Title);
            Assert.Equal(graphic3.Title, graphic3ByGet.Title);

            graphic1ByGet.Title = "Other title1";
            graphic2ByGet.Title = "Other title2";
            graphic3ByGet.Title = "Other title3";

            Assert.NotEqual(graphic1ByGet.Title, graphicTable.Get(graphic1.Id).Title);
            Assert.NotEqual(graphic2ByGet.Title, graphicTable.Get(graphic2.Id).Title);
            Assert.NotEqual(graphic3ByGet.Title, graphicTable.Get(graphic3.Id).Title);
        }
    }
}