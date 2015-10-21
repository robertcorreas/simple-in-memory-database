using System;
using SimpleInMemoryDatabase.Test.EntitiesTest;
using SimpleInMemoryDatabase.Test.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Test.RelationTest.OneToOneTest
{
    public class OneToOneUpdateTest : TesteBase
    {
        [Fact(DisplayName = "Should Update If Fk Satisfy")]
        public void ShouldUpdateIfFkSatisfy()
        {
            Relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometry = new Geometry();
            var geometry2 = new Geometry();
            var well = new Well(geometry, new Trajectory());

            geometryTable.Insert(geometry);
            geometryTable.Insert(geometry2);
            wellTable.Insert(well);

            Assert.True(1 == wellTable.Count);
            Assert.True(2 == geometryTable.Count);
            Assert.True(3 == Index.Count);

            Assert.Equal(well.Id, wellTable.Get(well.Id).Id);
            Assert.Equal(geometry.Id, wellTable.Get(well.Id).Geometry.Id);
            Assert.Equal(geometry.Id, geometryTable.Get(geometry.Id).Id);
            Assert.Equal(geometry2.Id, geometryTable.Get(geometry2.Id).Id);

            well.Geometry = geometry2;

            wellTable.Update(well);

            Assert.True(1 == wellTable.Count);
            Assert.True(2 == geometryTable.Count);
            Assert.True(3 == Index.Count);

            Assert.Equal(well.Id, wellTable.Get(well.Id).Id);
            Assert.Equal(geometry2.Id, wellTable.Get(well.Id).Geometry.Id);
            Assert.Equal(geometry.Id, geometryTable.Get(geometry.Id).Id);
            Assert.Equal(geometry2.Id, geometryTable.Get(geometry2.Id).Id);
        }

        [Fact(DisplayName = "Should Throw Exception Update If Fk Not Satisfy (OneToOne)")]
        public void ShouldThrowExceptionUpdateIfFkNotSatisfy()
        {
            Relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometry = new Geometry();
            var well = new Well(geometry, new Trajectory());

            geometryTable.Insert(geometry);
            wellTable.Insert(well);

            Assert.True(1 == wellTable.Count);
            Assert.True(1 == geometryTable.Count);
            Assert.True(2 == Index.Count);

            Assert.Equal(well.Id, wellTable.Get(well.Id).Id);
            Assert.Equal(geometry.Id, wellTable.Get(well.Id).Geometry.Id);
            Assert.Equal(geometry.Id, geometryTable.Get(geometry.Id).Id);

            well.Geometry = new Geometry();

            var ex = Assert.Throws<InvalidOperationException>(() => { wellTable.Update(well); });

            Assert.Equal("Invalid FK", ex.Message);
        }
    }
}