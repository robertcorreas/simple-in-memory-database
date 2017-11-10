using System;
using System.Linq;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Lib.Tests.RelationTests.ManyToManyTests
{
    public class ManyToManyUpdateTests : TestBase
    {
        [Fact(DisplayName = "Should Update if Fk Satisfy (ManyToMany)")]
        public void ShouldUpdateIfFkSatisfy()
        {
            Db.CreateManyToMany<Trajectory,Graphic,TrajectoryGraphicRelationalTable>(tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var trajectory = new Trajectory();
            var trajectory2 = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            Db.Insert(trajectory);
            Db.Insert(trajectory2);
            Db.Insert(graphic);
            Db.Insert(trajectoryGraphic);

            Assert.True(2 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());

            Assert.Equal(trajectory.Id, Db.GetAll<TrajectoryGraphicRelationalTable>().First().Trajectory.Id);
            Assert.Equal(graphic.Id, Db.GetAll<TrajectoryGraphicRelationalTable>().First().Graphic.Id);

            trajectoryGraphic.Trajectory = trajectory2;
            Db.Update(trajectoryGraphic);

            Assert.True(2 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());

            Assert.Equal(trajectory2.Id, Db.GetAll<TrajectoryGraphicRelationalTable>().First().Trajectory.Id);
            Assert.Equal(graphic.Id, Db.GetAll<TrajectoryGraphicRelationalTable>().First().Graphic.Id);
        }

        [Fact(DisplayName = "Should Throw Exception When Fk Dependency Not Satisfy - Update(ManyToMany)")]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
        {
            Db.CreateManyToMany<Trajectory,Graphic,TrajectoryGraphicRelationalTable>(tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            Db.Insert(trajectory);
            Db.Insert(graphic);
            Db.Insert(trajectoryGraphic);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());

            Assert.Equal(trajectory.Id, Db.GetAll<TrajectoryGraphicRelationalTable>().First().Trajectory.Id);
            Assert.Equal(graphic.Id, Db.GetAll<TrajectoryGraphicRelationalTable>().First().Graphic.Id);

            trajectoryGraphic.Trajectory = new Trajectory();

            var ex = Assert.Throws<InvalidOperationException>(() => { Db.Update(trajectoryGraphic); });

            Assert.Equal("Invalid FK", ex.Message);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());

            Assert.Equal(trajectory.Id, Db.GetAll<TrajectoryGraphicRelationalTable>().First().Trajectory.Id);
            Assert.Equal(graphic.Id, Db.GetAll<TrajectoryGraphicRelationalTable>().First().Graphic.Id);
        }
    }
}