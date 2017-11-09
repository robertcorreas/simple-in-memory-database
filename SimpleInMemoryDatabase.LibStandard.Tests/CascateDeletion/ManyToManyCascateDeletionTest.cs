using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.CascateDeletion
{
    public class ManyToManyCascateDeletionTest : TestBase
    {
        [Fact(DisplayName = "Should Delete In Cascate Deleting Table1")]
        public void ShouldDeleteInCascateDeletingTable1()
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

            Db.Delete(trajectory);

            Assert.True(0 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(0 == Db.Count<TrajectoryGraphicRelationalTable>());
        }

        [Fact(DisplayName = "Should Delete In Cascate Deleting Table2")]
        public void ShouldDeleteInCascateDeletingTable2()
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

            Db.Delete(graphic);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(0 == Db.Count<Graphic>());
            Assert.True(0 == Db.Count<TrajectoryGraphicRelationalTable>());
        }

        [Fact(DisplayName = "Should Delete In Cascate Deleting Relational Table")]
        public void ShouldDeleteInCascateDeletingRelationalTable()
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

            Db.Delete(trajectoryGraphic);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(0 == Db.Count<TrajectoryGraphicRelationalTable>());
        }

        [Fact(DisplayName = "Should Not Delete In Cascate Deleting Table1")]
        public void ShouldNotDeleteInCascateDeletingTable1()
        {
            Db.CreateManyToMany<Trajectory,Graphic,TrajectoryGraphicRelationalTable>(tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id, false);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            Db.Insert(trajectory);
            Db.Insert(graphic);
            Db.Insert(trajectoryGraphic);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());

            Db.Delete(trajectory);

            Assert.True(0 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());
        }

        [Fact(DisplayName = "Should Not Delete In Cascate Deleting Table2")]
        public void ShouldNotDeleteInCascateDeletingTable2()
        {
            Db.CreateManyToMany<Trajectory, Graphic, TrajectoryGraphicRelationalTable>(tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id, false);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            Db.Insert(trajectory);
            Db.Insert(graphic);
            Db.Insert(trajectoryGraphic);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());


            Db.Delete(graphic);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(0 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());

        }

        [Fact(DisplayName = "Should Not Delete In Cascate Deleting RelationalTable")]
        public void ShouldNotDeleteInCascateDeletingRelationalTable()
        {
            Db.CreateManyToMany<Trajectory, Graphic, TrajectoryGraphicRelationalTable>(tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id, false);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            Db.Insert(trajectory);
            Db.Insert(graphic);
            Db.Insert(trajectoryGraphic);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());

            Db.Delete(trajectoryGraphic);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(0 == Db.Count<TrajectoryGraphicRelationalTable>());
        }

        [Fact(DisplayName = "Should Delete In Cascate With Multiple ManyToMany Deleting Table1")]
        public void ShouldDeleteInCascateWithMultipleManyToManyDeletingTable1()
        {
            Db.CreateManyToMany<Trajectory,Graphic,TrajectoryGraphicRelationalTable>(tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();
            var graphic1 = new Graphic();
            var graphic2 = new Graphic();
            var trajectoryGraphic1 = new TrajectoryGraphicRelationalTable(trajectory1, graphic1);
            var trajectoryGraphic2 = new TrajectoryGraphicRelationalTable(trajectory2, graphic2);

            Db.Insert(trajectory1);
            Db.Insert(trajectory2);
            Db.Insert(graphic1);
            Db.Insert(graphic2);
            Db.Insert(trajectoryGraphic1);
            Db.Insert(trajectoryGraphic2);

            Assert.True(2 == Db.Count<Trajectory>());
            Assert.True(2 == Db.Count<Graphic>());
            Assert.True(2 == Db.Count<TrajectoryGraphicRelationalTable>());

            Db.Delete(trajectory1);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(2 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());
        }

        [Fact(DisplayName = "Should Delete In Cascate With Multiple ManyToMany Deleting Table2")]
        public void ShouldDeleteInCascateWithMultipleManyToManyDeletingTable2()
        {
            Db.CreateManyToMany<Trajectory,Graphic,TrajectoryGraphicRelationalTable>(tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();
            var graphic1 = new Graphic();
            var graphic2 = new Graphic();
            var trajectoryGraphic1 = new TrajectoryGraphicRelationalTable(trajectory1, graphic1);
            var trajectoryGraphic2 = new TrajectoryGraphicRelationalTable(trajectory2, graphic2);

            Db.Insert(trajectory1);
            Db.Insert(trajectory2);
            Db.Insert(graphic1);
            Db.Insert(graphic2);
            Db.Insert(trajectoryGraphic1);
            Db.Insert(trajectoryGraphic2);

            Assert.True(2 == Db.Count<Trajectory>());
            Assert.True(2 == Db.Count<Graphic>());
            Assert.True(2 == Db.Count<TrajectoryGraphicRelationalTable>());

            Db.Delete(graphic2);

            Assert.True(2 == Db.Count<Trajectory>());
            Assert.True(1 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());
        }

        [Fact(DisplayName = "Should Delete In Cascate With Multiple ManyToMany Deleting RelationalTable")]
        public void ShouldDeleteInCascateWithMultipleManyToManyDeletingRelationalTable()
        {
            Db.CreateManyToMany<Trajectory,Graphic,TrajectoryGraphicRelationalTable>(tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();
            var graphic1 = new Graphic();
            var graphic2 = new Graphic();
            var trajectoryGraphic1 = new TrajectoryGraphicRelationalTable(trajectory1, graphic1);
            var trajectoryGraphic2 = new TrajectoryGraphicRelationalTable(trajectory2, graphic2);

            Db.Insert(trajectory1);
            Db.Insert(trajectory2);
            Db.Insert(graphic1);
            Db.Insert(graphic2);
            Db.Insert(trajectoryGraphic1);
            Db.Insert(trajectoryGraphic2);

            Assert.True(2 == Db.Count<Trajectory>());
            Assert.True(2 == Db.Count<Graphic>());
            Assert.True(2 == Db.Count<TrajectoryGraphicRelationalTable>());

            Db.Delete(trajectoryGraphic1);

            Assert.True(2 == Db.Count<Trajectory>());
            Assert.True(2 == Db.Count<Graphic>());
            Assert.True(1 == Db.Count<TrajectoryGraphicRelationalTable>());
        }
    }
}