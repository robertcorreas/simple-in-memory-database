using System;
using System.Linq;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.RelationTest.ManyToManyTest
{
    public class ManyToManyInsertTest : TestBase
    {
        [Fact(DisplayName = "Should Insert ManyToMany If Fk Satisfy")]
        public void ShouldInsertManyToManyIfFkSatisfy()
        {
            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            Db.CreateManyToMany<Trajectory,Graphic,TrajectoryGraphicRelationalTable>(tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            Db.Insert(trajectory);
            Db.Insert(graphic);
            Db.Insert(trajectoryGraphic);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());

            Assert.Equal(trajectory.Id, Db.GetAll<TrajectoryGraphicRelationalTable>().First().Trajectory.Id);
            Assert.Equal(graphic.Id, Db.GetAll<TrajectoryGraphicRelationalTable>().First().Graphic.Id);
        }

        [Fact(DisplayName = "Should Throw Exception When Fk Dependency Not Satisfy - Insert (ManyToMany)")]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
        {
            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            Db.CreateManyToMany<Trajectory,Graphic,TrajectoryGraphicRelationalTable>(tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var ex = Assert.Throws<InvalidOperationException>(() => { Db.Insert(trajectoryGraphic); });

            Assert.Equal("Invalid FK", ex.Message);

            Assert.True(0 == Db.Count<Trajectory>());
            Assert.True(0 == Db.Count<Graphic>());
            Assert.True(0 == Db.Count<TrajectoryGraphicRelationalTable>());
        }
    }
}