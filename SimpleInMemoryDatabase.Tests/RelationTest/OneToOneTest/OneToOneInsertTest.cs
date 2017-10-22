using System;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.RelationTest.OneToOneTest
{
    public class OneToOneInsertTest : TestBase
    {
        [Fact(DisplayName = "Should Throw Exception When Fk Dependency Not Satisfy (OneToOne)")]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
        {
            Db.CreateOneToOne<Well,Geometry>(w => w.Geometry.Id);

            var geometria = new Geometry();
            var well = new Well(geometria, new Trajectory());

            var ex = Assert.Throws<InvalidOperationException>(() => { Db.Insert(well); });

            Assert.True(0 == Db.Count<Well>());
            Assert.Equal("Invalid FK", ex.Message);
        }

        [Fact(DisplayName = "Should Insert If Fk Dependency Satisfy (OneToOne)")]
        public void ShouldInsertIfFkDependencySatisfy()
        {
            Db.CreateOneToOne<Well,Geometry>(w => w.Geometry.Id);

            var geometry = new Geometry();
            var well = new Well(geometry, new Trajectory());

            Db.Insert(geometry);
            Db.Insert(well);

            Assert.True(1 == Db.Count<Well>());
            Assert.True(1 == Db.Count<Geometry>());

            Assert.Equal(geometry.Id, Db.GetOne<Well>(well.Id).Geometry.Id);
        }
    }
}