using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NewDatabase.Core;
using NewDatabase.Test.DataTest;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.RelationTest.OneToOneTest
{
    public class OneToOneInsertTest
    {
        private readonly DataTest.DataTest _dataTest;

        public OneToOneInsertTest()
        {
            _dataTest = new DataTest.DataTest();
        }

        [Fact]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
        {
            var relation = new Relation();
            var index = new Index();

            var wellTable = new Table<Well>(tuplas: _dataTest.Wells, primaryKey: w => w.Id, relation: relation, index: index);
            var geometryTable = new Table<Geometry>(tuplas: _dataTest.Geometries, primaryKey: g => g.Id, relation: relation, index: index);

            relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometria = new Geometry();
            var well = new Well(geometria);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                wellTable.Insert(well);
            });

            Assert.True(0 == wellTable.Count);
            Assert.Equal("Invalid FK", ex.Message);
        }

        [Fact]
        public void ShouldInsertIfFkDependencySatisfy()
        {
            var relation = new Relation();
            var index = new Index();

            var wellTable = new Table<Well>(tuplas: _dataTest.Wells, primaryKey: w => w.Id, relation: relation, index: index);
            var geometryTable = new Table<Geometry>(tuplas: _dataTest.Geometries, primaryKey: g => g.Id, relation: relation, index: index);

            relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometry = new Geometry();
            var well = new Well(geometry);

            geometryTable.Insert(geometry);
            wellTable.Insert(well);

            Assert.True(1 == wellTable.Count);
            Assert.True(1 == geometryTable.Count);

            Assert.Equal(geometry.Id, wellTable.Get(well.Id).Geometry.Id);
        }
    }
}
