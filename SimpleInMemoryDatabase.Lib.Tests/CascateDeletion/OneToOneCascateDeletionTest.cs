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
            Db.CreateOneToOne<Well,Geometry>(w => w.Geometry.Id);

            var geometry = new Geometry();
            var well = new Well(geometry, new Trajectory());

            Db.Insert(geometry);
            Db.Insert(well);

            Db.Delete(well);

            Assert.True(0 == Db.Count<Well>());
            Assert.True(0 == Db.Count<Geometry>());
            Assert.True(0 == Db.IndexCount());
        }

        [Fact(DisplayName = "Should Not Delete In Cascate (OneToOne)")]
        public void ShouldNotDeleteInCascate()
        {
            Db.CreateOneToOne<Well,Geometry>(w => w.Geometry.Id, false);

            var geometry = new Geometry();
            var well = new Well(geometry, new Trajectory());

            Db.Insert(geometry);
            Db.Insert(well);

            Db.Delete(well);

            Assert.True(0 == Db.Count<Well>());
            Assert.True(1 == Db.Count<Geometry>());
        }

        [Fact(DisplayName = "Should Delete In Cascate With Multiple OneToOne")]
        public void ShouldDeleteInCascateWithMultipleOneToOne()
        {
            Db.CreateOneToOne<Well,Geometry>(w => w.Geometry.Id);

            var geometry1 = new Geometry();
            var well1 = new Well(geometry1, new Trajectory());

            var geometry2 = new Geometry();
            var well2 = new Well(geometry2, new Trajectory());

            Db.Insert(geometry1);
            Db.Insert(well1);

            Db.Insert(geometry2);
            Db.Insert(well2);

            Assert.True(2 == Db.Count<Well>());
            Assert.True(2 == Db.Count<Geometry>());
            Assert.True(4 == Db.IndexCount());

            Db.Delete(well1);

            Assert.True(1 == Db.Count<Well>());
            Assert.True(1 == Db.Count<Geometry>());
            Assert.True(2 == Db.IndexCount());

            Assert.Equal(well2.Id, Db.GetOne<Well>(well2.Id).Id);
            Assert.Equal(geometry2.Id, Db.GetOne<Well>(well2.Id).Geometry.Id);
            Assert.Equal(geometry2.Id, Db.GetOne<Geometry>(geometry2.Id).Id);
        }
    }
}