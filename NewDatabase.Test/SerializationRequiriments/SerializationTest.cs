using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewDatabase.Core;
using NewDatabase.Test.DataTest;
using NewDatabase.Test.EntitiesTest;
using Newtonsoft.Json;
using Xunit;

namespace NewDatabase.Test.SerializationRequiriments
{
    public class SerializationTest
    {
        private readonly DataTest.DataTest _dataTest;

        public SerializationTest()
        {
            _dataTest = new DataTest.DataTest();
        }

        [Fact]
        public void ShouldSerializeOneToOne()
        {
            var index = new Index();
            var relation = new Relation();

            var wellTable = new Table<Well>(tuplas: _dataTest.Wells, primaryKey: w => w.Id, relation: relation, index: index);
            var geometryTable = new Table<Geometry>(tuplas: _dataTest.Geometries, primaryKey: g => g.Id, relation: relation, index: index);

            relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometry = new Geometry();
            var well = new Well(geometry);

            geometryTable.Insert(geometry);
            wellTable.Insert(well);
            

            var jsonData = JsonConvert.SerializeObject(_dataTest, Formatting.Indented);
            var jsonIndex = JsonConvert.SerializeObject(index, Formatting.Indented);

            var newData = JsonConvert.DeserializeObject<DataTest.DataTest>(jsonData);
            var newIndex = JsonConvert.DeserializeObject<Index>(jsonIndex);
            var newRelation = new Relation();


            var newWellTable = new Table<Well>(tuplas: newData.Wells, primaryKey: w => w.Id, relation: newRelation, index: newIndex);
            var newGeometryTable = new Table<Geometry>(tuplas: newData.Geometries, primaryKey: g => g.Id, relation: newRelation, index: newIndex);

            newRelation.CreateOneToOne(newWellTable,newGeometryTable,w => w.Geometry.Id);

            Assert.True(1 == newWellTable.Count);
            Assert.True(1 == newGeometryTable.Count);
            Assert.True(2 == newIndex.Count);

            Assert.Equal(well.Id,newWellTable.Get(well.Id).Id);
            Assert.Equal(geometry.Id, newWellTable.Get(well.Id).Geometry.Id);
            Assert.Equal(geometry.Id, newGeometryTable.Get(geometry.Id).Id);

            newWellTable.Delete(well);

            Assert.True(0 == newWellTable.Count);
            Assert.True(0 == newGeometryTable.Count);
            Assert.True(0 == newIndex.Count);
        }
    }
}
