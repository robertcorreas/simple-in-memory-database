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
        private readonly Data _data;

        public OneToOneCascateDeletionTest()
        {
            _data = new Data();
        }

        [Fact]
        public void ShouldDeleteInCascate()
        {
            var index = new Index();
            var relation = new Relation();

            var wellTable = new Table<Well>(tuplas: _data.Wells, primaryKey: w => w.Id, relation:relation, index:index);
            var geometryTable = new Table<Geometry>(tuplas: _data.Geometries, primaryKey: g => g.Id, relation: relation, index: index);

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

            var wellTable = new Table<Well>(tuplas: _data.Wells, primaryKey: w => w.Id, relation: relation, index: index);
            var geometryTable = new Table<Geometry>(tuplas: _data.Geometries, primaryKey: g => g.Id, relation: relation, index: index);

            relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id, false);

            var geometry = new Geometry();
            var well = new Well(geometry);

            geometryTable.Insert(geometry);
            wellTable.Insert(well);

            wellTable.Delete(well);

            Assert.True(0 == wellTable.Count);
            Assert.True(1 == geometryTable.Count);
        }
    }
}
