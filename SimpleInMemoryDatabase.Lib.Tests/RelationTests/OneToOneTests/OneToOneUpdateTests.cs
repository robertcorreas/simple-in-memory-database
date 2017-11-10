using System;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Lib.Tests.RelationTests.OneToOneTests
{
    public class OneToOneUpdateTests : TestBase
    {
        [Fact(DisplayName = "Should Update If Fk Satisfy")]
        public void ShouldUpdateIfFkSatisfy()
        {
            Db.CreateOneToOne<Well,Geometry>(w => w.Geometry.Id);

            var geometry = new Geometry();
            var geometry2 = new Geometry();
            var well = new Well(geometry, new Trajectory());

            Db.Insert(geometry);
            Db.Insert(geometry2);
            Db.Insert(well);

            Assert.True(1 == Db.Count<Well>());
            Assert.True(2 == Db.Count<Geometry>());
            Assert.True(3 == Db.IndexCount());

            Assert.Equal(well.Id, Db.GetOne<Well>(well.Id).Id);
            Assert.Equal(geometry.Id, Db.GetOne<Well>(well.Id).Geometry.Id);
            Assert.Equal(geometry.Id, Db.GetOne<Geometry>(geometry.Id).Id);
            Assert.Equal(geometry2.Id, Db.GetOne<Geometry>(geometry2.Id).Id);

            well.Geometry = geometry2;

            Db.Update(well);

            Assert.True(1 == Db.Count<Well>());
            Assert.True(2 == Db.Count<Geometry>());
            Assert.True(3 == Db.IndexCount());

            Assert.Equal(well.Id, Db.GetOne<Well>(well.Id).Id);
            Assert.Equal(geometry2.Id, Db.GetOne<Well>(well.Id).Geometry.Id);
            Assert.Equal(geometry.Id, Db.GetOne<Geometry>(geometry.Id).Id);
            Assert.Equal(geometry2.Id, Db.GetOne<Geometry>(geometry2.Id).Id);
        }

        [Fact(DisplayName = "Should Throw Exception Update If Fk Not Satisfy (OneToOne)")]
        public void ShouldThrowExceptionUpdateIfFkNotSatisfy()
        {
            Db.CreateOneToOne<Well,Geometry>(w => w.Geometry.Id);

            var geometry = new Geometry();
            var well = new Well(geometry, new Trajectory());

            Db.Insert(geometry);
            Db.Insert(well);

            Assert.True(1 == Db.Count<Well>());
            Assert.True(1 == Db.Count<Geometry>());
            Assert.True(2 == Db.IndexCount());

            Assert.Equal(well.Id, Db.GetOne<Well>(well.Id).Id);
            Assert.Equal(geometry.Id, Db.GetOne<Well>(well.Id).Geometry.Id);
            Assert.Equal(geometry.Id, Db.GetOne<Geometry>(geometry.Id).Id);

            well.Geometry = new Geometry();

            var ex = Assert.Throws<InvalidOperationException>(() => { Db.Update(well); });

            Assert.Equal("Invalid FK", ex.Message);
        }
    }
}