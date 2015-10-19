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
        private readonly Data _data;

        public WellAndGeometryTest()
        {
            _data = new Data();
        }

        public void CreateRelationOneToOne()
        {
            var wellTable = new Table<Well>(tuplas: _data.Wells, primaryKey: w => w.Id);
            var geometryTable = new Table<Geometry>(tuplas: _data.Geometries, primaryKey: g => g.Id);

            var relation = new Relation();
            relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);
        }
       
    }
}
