using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewDatabase.Core;
using NewDatabase.Test.DataTest;
using NewDatabase.Test.EntitiesTest;

namespace NewDatabase.Test.RelationTest.OneToOneTest
{
    public class WellAndGeometryTest
    {
        private readonly DataTest.DataTest _dataTest;

        public WellAndGeometryTest()
        {
            _dataTest = new DataTest.DataTest();
        }

        public void CreateRelationOneToOne()
        {
            var wellTable = new Table<Well>(tuplas: _dataTest.Wells, primaryKey: w => w.Id);
            var geometryTable = new Table<Geometry>(tuplas: _dataTest.Geometries, primaryKey: g => g.Id);

            var relation = new Relation();
            relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);
        }
       
    }
}
