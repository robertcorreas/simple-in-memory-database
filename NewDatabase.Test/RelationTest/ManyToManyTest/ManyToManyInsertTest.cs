using System;
using System.Linq;
using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.RelationTest.ManyToManyTest
{
    public class ManyToManyInsertTest
    {
        private readonly DataTest.DataTest _dataTest;
        private readonly Index _index;
        private readonly Relation _relation;

        public ManyToManyInsertTest()
        {
            _dataTest = new DataTest.DataTest();
            _relation = new Relation();
            _index = new Index();
        }

        [Fact]
        public void ShouldInsertManyToManyIfFkSatisfy()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);


            trajectoryTable.Insert(trajectory);
            graphicTable.Insert(graphic);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            Assert.Equal(trajectory.Id, trajectoryGraphicRelationalTable.GetAll().First().Trajectory.Id);
            Assert.Equal(graphic.Id, trajectoryGraphicRelationalTable.GetAll().First().Graphic.Id);
        }

        [Fact]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
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


            var ex =
                Assert.Throws<InvalidOperationException>(
                    () => { trajectoryGraphicRelationalTable.Insert(trajectoryGraphic); });

            Assert.Equal("Invalid FK", ex.Message);


            Assert.True(0 == trajectoryTable.Count);
            Assert.True(0 == graphicTable.Count);
            Assert.True(0 == trajectoryGraphicRelationalTable.Count);
        }
    }
}