using System;
using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.RelationTest.OneToOneTest
{
    public class OneToOneUpdateTest
    {
        private readonly DataTest.DataTest _dataTest;
        private readonly Index _index;
        private readonly Relation _relation;

        #region Construtores

        public OneToOneUpdateTest()
        {
            _dataTest = new DataTest.DataTest();
            _index = new Index();
            _relation = new Relation();
        }

        #endregion

        [Fact]
        public void ShouldUpdateIfFkSatisfy()
        {
            var wellTable = new Table<Well>(_dataTest.Wells, w => w.Id, _relation, _index);
            var geometryTable = new Table<Geometry>(_dataTest.Geometries, g => g.Id, _relation, _index);

            _relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometry = new Geometry();
            var geometry2 = new Geometry();
            var well = new Well(geometry, new Trajectory());

            geometryTable.Insert(geometry);
            geometryTable.Insert(geometry2);
            wellTable.Insert(well);

            Assert.True(1 == wellTable.Count);
            Assert.True(2 == geometryTable.Count);
            Assert.True(3 == _index.Count);

            Assert.Equal(well.Id, wellTable.Get(well.Id).Id);
            Assert.Equal(geometry.Id, wellTable.Get(well.Id).Geometry.Id);
            Assert.Equal(geometry.Id, geometryTable.Get(geometry.Id).Id);
            Assert.Equal(geometry2.Id, geometryTable.Get(geometry2.Id).Id);

            well.Geometry = geometry2;

            wellTable.Update(well);

            Assert.True(1 == wellTable.Count);
            Assert.True(2 == geometryTable.Count);
            Assert.True(3 == _index.Count);

            Assert.Equal(well.Id, wellTable.Get(well.Id).Id);
            Assert.Equal(geometry2.Id, wellTable.Get(well.Id).Geometry.Id);
            Assert.Equal(geometry.Id, geometryTable.Get(geometry.Id).Id);
            Assert.Equal(geometry2.Id, geometryTable.Get(geometry2.Id).Id);
        }

        [Fact]
        public void ShouldThrowExceptionUpdateIfFkNotSatisfy()
        {
            var wellTable = new Table<Well>(_dataTest.Wells, w => w.Id, _relation, _index);
            var geometryTable = new Table<Geometry>(_dataTest.Geometries, g => g.Id, _relation, _index);

            _relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometry = new Geometry();
            var well = new Well(geometry, new Trajectory());

            geometryTable.Insert(geometry);
            wellTable.Insert(well);

            Assert.True(1 == wellTable.Count);
            Assert.True(1 == geometryTable.Count);
            Assert.True(2 == _index.Count);

            Assert.Equal(well.Id, wellTable.Get(well.Id).Id);
            Assert.Equal(geometry.Id, wellTable.Get(well.Id).Geometry.Id);
            Assert.Equal(geometry.Id, geometryTable.Get(geometry.Id).Id);

            well.Geometry = new Geometry();

            var ex = Assert.Throws<InvalidOperationException>(() => { wellTable.Update(well); });

            Assert.Equal("Invalid FK", ex.Message);
        }
    }
}