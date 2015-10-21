using NewDatabase.Test.EntitiesTest;
using NewDatabase.Test.Helpers;
using Xunit;

namespace NewDatabase.Test.CascateDeletion
{
    public class ManyToManyCascateDeletionTest : TesteBase
    {
        [Fact(DisplayName = "Should Delete In Cascate Deleting Table1")]
        public void ShouldDeleteInCascateDeletingTable1()
        {
            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            Relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            trajectoryTable.Insert(trajectory);
            graphicTable.Insert(graphic);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            trajectoryTable.Delete(trajectory);

            Assert.True(0 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(0 == trajectoryGraphicRelationalTable.Count);
        }

        [Fact(DisplayName = "Should Delete In Cascate Deleting Table2")]
        public void ShouldDeleteInCascateDeletingTable2()
        {
            Relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            trajectoryTable.Insert(trajectory);
            graphicTable.Insert(graphic);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            graphicTable.Delete(graphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(0 == graphicTable.Count);
            Assert.True(0 == trajectoryGraphicRelationalTable.Count);
        }

        [Fact(DisplayName = "Should Delete In Cascate Deleting Relational Table")]
        public void ShouldDeleteInCascateDeletingRelationalTable()
        {
            Relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            trajectoryTable.Insert(trajectory);
            graphicTable.Insert(graphic);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            trajectoryGraphicRelationalTable.Delete(trajectoryGraphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(0 == trajectoryGraphicRelationalTable.Count);
        }

        [Fact(DisplayName = "Should Not Delete In Cascate Deleting Table1")]
        public void ShouldNotDeleteInCascateDeletingTable1()
        {
            Relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id, false);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            trajectoryTable.Insert(trajectory);
            graphicTable.Insert(graphic);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            trajectoryTable.Delete(trajectory);

            Assert.True(0 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);
        }

        [Fact(DisplayName = "Should Not Delete In Cascate Deleting Table2")]
        public void ShouldNotDeleteInCascateDeletingTable2()
        {
            Relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id, false);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            trajectoryTable.Insert(trajectory);
            graphicTable.Insert(graphic);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            graphicTable.Delete(graphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(0 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);
        }

        [Fact(DisplayName = "Should Not Delete In Cascate Deleting RelationalTable")]
        public void ShouldNotDeleteInCascateDeletingRelationalTable()
        {
            Relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id, false);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            trajectoryTable.Insert(trajectory);
            graphicTable.Insert(graphic);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            trajectoryGraphicRelationalTable.Delete(trajectoryGraphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(0 == trajectoryGraphicRelationalTable.Count);
        }

        [Fact(DisplayName = "Should Delete In Cascate With Multiple ManyToMany Deleting Table1")]
        public void ShouldDeleteInCascateWithMultipleManyToManyDeletingTable1()
        {
            Relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();
            var graphic1 = new Graphic();
            var graphic2 = new Graphic();
            var trajectoryGraphic1 = new TrajectoryGraphicRelationalTable(trajectory1, graphic1);
            var trajectoryGraphic2 = new TrajectoryGraphicRelationalTable(trajectory2, graphic2);

            trajectoryTable.Insert(trajectory1);
            trajectoryTable.Insert(trajectory2);
            graphicTable.Insert(graphic1);
            graphicTable.Insert(graphic2);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic1);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic2);

            Assert.True(2 == trajectoryTable.Count);
            Assert.True(2 == graphicTable.Count);
            Assert.True(2 == trajectoryGraphicRelationalTable.Count);

            trajectoryTable.Delete(trajectory1);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(2 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);
        }

        [Fact(DisplayName = "Should Delete In Cascate With Multiple ManyToMany Deleting Table2")]
        public void ShouldDeleteInCascateWithMultipleManyToManyDeletingTable2()
        {
            Relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();
            var graphic1 = new Graphic();
            var graphic2 = new Graphic();
            var trajectoryGraphic1 = new TrajectoryGraphicRelationalTable(trajectory1, graphic1);
            var trajectoryGraphic2 = new TrajectoryGraphicRelationalTable(trajectory2, graphic2);

            trajectoryTable.Insert(trajectory1);
            trajectoryTable.Insert(trajectory2);
            graphicTable.Insert(graphic1);
            graphicTable.Insert(graphic2);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic1);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic2);

            Assert.True(2 == trajectoryTable.Count);
            Assert.True(2 == graphicTable.Count);
            Assert.True(2 == trajectoryGraphicRelationalTable.Count);

            graphicTable.Delete(graphic2);

            Assert.True(2 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);
        }

        [Fact(DisplayName = "Should Delete In Cascate With Multiple ManyToMany Deleting RelationalTable")]
        public void ShouldDeleteInCascateWithMultipleManyToManyDeletingRelationalTable()
        {
            Relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();
            var graphic1 = new Graphic();
            var graphic2 = new Graphic();
            var trajectoryGraphic1 = new TrajectoryGraphicRelationalTable(trajectory1, graphic1);
            var trajectoryGraphic2 = new TrajectoryGraphicRelationalTable(trajectory2, graphic2);

            trajectoryTable.Insert(trajectory1);
            trajectoryTable.Insert(trajectory2);
            graphicTable.Insert(graphic1);
            graphicTable.Insert(graphic2);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic1);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic2);

            Assert.True(2 == trajectoryTable.Count);
            Assert.True(2 == graphicTable.Count);
            Assert.True(2 == trajectoryGraphicRelationalTable.Count);

            trajectoryGraphicRelationalTable.Delete(trajectoryGraphic1);

            Assert.True(2 == trajectoryTable.Count);
            Assert.True(2 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);
        }
    }
}