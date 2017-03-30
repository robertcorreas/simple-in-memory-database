using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.CascateDeletion
{
    public class OneToOneCascateDeletionTest : TestBase
    {
        [Fact(DisplayName = "Should Delete In Cascate (OneToOne)")]
        public void ShouldDeleteInCascate()
        {
            Relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometry = new Geometry();
            var well = new Well(geometry, new Trajectory());

            geometryTable.Insert(geometry);
            wellTable.Insert(well);

            wellTable.Delete(well);

            Assert.True(0 == wellTable.Count);
            Assert.True(0 == geometryTable.Count);
            Assert.True(0 == Index.Count);
        }

        [Fact(DisplayName = "Should Not Delete In Cascate (OneToOne)")]
        public void ShouldNotDeleteInCascate()
        {
            Relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id, false);

            var geometry = new Geometry();
            var well = new Well(geometry, new Trajectory());

            geometryTable.Insert(geometry);
            wellTable.Insert(well);

            wellTable.Delete(well);

            Assert.True(0 == wellTable.Count);
            Assert.True(1 == geometryTable.Count);
        }

        [Fact(DisplayName = "Should Delete In Cascate With Multiple OneToOne")]
        public void ShouldDeleteInCascateWithMultipleOneToOne()
        {
            Relation.CreateOneToOne(wellTable, geometryTable, w => w.Geometry.Id);

            var geometry1 = new Geometry();
            var well1 = new Well(geometry1, new Trajectory());

            var geometry2 = new Geometry();
            var well2 = new Well(geometry2, new Trajectory());

            geometryTable.Insert(geometry1);
            wellTable.Insert(well1);

            geometryTable.Insert(geometry2);
            wellTable.Insert(well2);

            Assert.True(2 == wellTable.Count);
            Assert.True(2 == geometryTable.Count);
            Assert.True(4 == Index.Count);

            wellTable.Delete(well1);

            Assert.True(1 == wellTable.Count);
            Assert.True(1 == geometryTable.Count);
            Assert.True(2 == Index.Count);

            Assert.Equal(well2.Id, wellTable.Get(well2.Id).Id);
            Assert.Equal(geometry2.Id, wellTable.Get(well2.Id).Geometry.Id);
            Assert.Equal(geometry2.Id, geometryTable.Get(geometry2.Id).Id);
        }
    }
}