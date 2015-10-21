using System;
using NewDatabase.Test.EntitiesTest;
using NewDatabase.Test.Helpers;
using Xunit;

namespace NewDatabase.Test.RelationTest.OneToOneTest
{
    public class OneToOneInsertTest : TesteBase
    {
        [Fact(DisplayName = "Should Throw Exception When Fk Dependency Not Satisfy (OneToOne)")]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
        {
            Relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometria = new Geometry();
            var well = new Well(geometria, new Trajectory());

            var ex = Assert.Throws<InvalidOperationException>(() => { wellTable.Insert(well); });

            Assert.True(0 == wellTable.Count);
            Assert.Equal("Invalid FK", ex.Message);
        }

        [Fact(DisplayName = "Should Insert If Fk Dependency Satisfy (OneToOne)")]
        public void ShouldInsertIfFkDependencySatisfy()
        {
            Relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometry = new Geometry();
            var well = new Well(geometry, new Trajectory());

            geometryTable.Insert(geometry);
            wellTable.Insert(well);

            Assert.True(1 == wellTable.Count);
            Assert.True(1 == geometryTable.Count);

            Assert.Equal(geometry.Id, wellTable.Get(well.Id).Geometry.Id);
        }
    }
}