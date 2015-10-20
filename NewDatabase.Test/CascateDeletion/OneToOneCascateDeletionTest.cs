using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewDatabase.Core;
using NewDatabase.Test.DataTest;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.CascateDeletion
{
    public class OneToOneCascateDeletionTest
    {
        private readonly DataTest.DataTest _dataTest;

        public OneToOneCascateDeletionTest()
        {
            _dataTest = new DataTest.DataTest();
        }

        [Fact]
        public void ShouldDeleteInCascate()
        {
            var index = new Index();
            var relation = new Relation();

            var wellTable = new Table<Well>(tuplas: _dataTest.Wells, primaryKey: w => w.Id, relation:relation, index:index);
            var geometryTable = new Table<Geometry>(tuplas: _dataTest.Geometries, primaryKey: g => g.Id, relation: relation, index: index);

            relation.CreateOneToOne(wellTable,geometryTable,w => w.Geometry.Id);

            var geometry = new Geometry();
            var well = new Well(geometry);

            geometryTable.Insert(geometry);
            wellTable.Insert(well);

            wellTable.Delete(well);

            Assert.True(0 == wellTable.Count);
            Assert.True(0 == geometryTable.Count);
            Assert.True(0 == index.Count);
        }
        [Fact]
        public void ShouldNotDeleteInCascate()
        {
            var index = new Index();
            var relation = new Relation();

            var wellTable = new Table<Well>(tuplas: _dataTest.Wells, primaryKey: w => w.Id, relation: relation, index: index);
            var geometryTable = new Table<Geometry>(tuplas: _dataTest.Geometries, primaryKey: g => g.Id, relation: relation, index: index);

            relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id, false);

            var geometry = new Geometry();
            var well = new Well(geometry);

            geometryTable.Insert(geometry);
            wellTable.Insert(well);

            wellTable.Delete(well);

            Assert.True(0 == wellTable.Count);
            Assert.True(1 == geometryTable.Count);
        }

        [Fact]
        public void ShouldDeleteInCascateWithMultipleOneToOne()
        {
            var index = new Index();
            var relation = new Relation();

            var wellTable = new Table<Well>(tuplas: _dataTest.Wells, primaryKey: w => w.Id, relation: relation, index: index);
            var geometryTable = new Table<Geometry>(tuplas: _dataTest.Geometries, primaryKey: g => g.Id, relation: relation, index: index);

            relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometry1 = new Geometry();
            var well1 = new Well(geometry1);

            var geometry2 = new Geometry();
            var well2 = new Well(geometry2);

            geometryTable.Insert(geometry1);
            wellTable.Insert(well1);

            geometryTable.Insert(geometry2);
            wellTable.Insert(well2);

            Assert.True(2 == wellTable.Count);
            Assert.True(2 == geometryTable.Count);
            Assert.True(4 == index.Count);

            wellTable.Delete(well1);

            Assert.True(1 == wellTable.Count);
            Assert.True(1 == geometryTable.Count);
            Assert.True(2 == index.Count);

            Assert.Equal(well2.Id,wellTable.Get(well2.Id).Id);
            Assert.Equal(geometry2.Id, wellTable.Get(well2.Id).Geometry.Id);
            Assert.Equal(geometry2.Id, geometryTable.Get(geometry2.Id).Id);
        }


    }
}
