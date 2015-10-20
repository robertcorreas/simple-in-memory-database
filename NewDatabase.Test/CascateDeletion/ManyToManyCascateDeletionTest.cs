using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.CascateDeletion
{
    public class ManyToManyCascateDeletionTest
    {
        private readonly DataTest.DataTest _dataTest;
        private readonly Index _index;
        private readonly Relation _relation;

        public ManyToManyCascateDeletionTest()
        {
            _dataTest = new DataTest.DataTest();
            _relation = new Relation();
            _index = new Index();
        }

        [Fact]
        public void ShouldDeleteInCascateDeletingTable1()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
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

        [Fact]
        public void ShouldDeleteInCascateDeletingTable2()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

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

        [Fact]
        public void ShouldDeleteInCascateDeletingRelationalTable()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

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

        [Fact]
        public void ShouldNotDeleteInCascateDeletingTable1()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id,false);

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

        [Fact]
        public void ShouldNotDeleteInCascateDeletingTable2()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id, false);

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

        [Fact]
        public void ShouldNotDeleteInCascateDeletingRelationalTable()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id, false);

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

        [Fact]
        public void ShouldDeleteInCascateWithMultipleManyToManyDeletingTable1()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();
            var graphic1 = new Graphic();
            var graphic2 = new Graphic();
            var trajectoryGraphic1 = new TrajectoryGraphicRelationalTable(trajectory1, graphic1);
            var trajectoryGraphic2 = new TrajectoryGraphicRelationalTable(trajectory2, graphic2);

            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

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

        [Fact]
        public void ShouldDeleteInCascateWithMultipleManyToManyDeletingTable2()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();
            var graphic1 = new Graphic();
            var graphic2 = new Graphic();
            var trajectoryGraphic1 = new TrajectoryGraphicRelationalTable(trajectory1, graphic1);
            var trajectoryGraphic2 = new TrajectoryGraphicRelationalTable(trajectory2, graphic2);

            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

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

        [Fact]
        public void ShouldDeleteInCascateWithMultipleManyToManyDeletingRelationalTable()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();
            var graphic1 = new Graphic();
            var graphic2 = new Graphic();
            var trajectoryGraphic1 = new TrajectoryGraphicRelationalTable(trajectory1, graphic1);
            var trajectoryGraphic2 = new TrajectoryGraphicRelationalTable(trajectory2, graphic2);

            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

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