using System;
using System.Linq;
using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.RelationTest.ManyToManyTest
{
    public class ManyToManyUpdateTest
    {
        private readonly DataTest.DataTest _dataTest;
        private readonly Index _index;
        private readonly Relation _relation;

        #region Construtores

        public ManyToManyUpdateTest()
        {
            _dataTest = new DataTest.DataTest();
            _index = new Index();
            _relation = new Relation();
        }

        #endregion

        [Fact(DisplayName = "Should Update if Fk Satisfy (ManyToMany)")]
        public void ShouldUpdateIfFkSatisfy()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            var trajectory = new Trajectory();
            var trajectory2 = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);


            trajectoryTable.Insert(trajectory);
            trajectoryTable.Insert(trajectory2);
            graphicTable.Insert(graphic);
            trajectoryGraphicRelationalTable.Insert(trajectoryGraphic);

            Assert.True(2 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            Assert.Equal(trajectory.Id, trajectoryGraphicRelationalTable.GetAll().First().Trajectory.Id);
            Assert.Equal(graphic.Id, trajectoryGraphicRelationalTable.GetAll().First().Graphic.Id);

            trajectoryGraphic.Trajectory = trajectory2;
            trajectoryGraphicRelationalTable.Update(trajectoryGraphic);

            Assert.True(2 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            Assert.Equal(trajectory2.Id, trajectoryGraphicRelationalTable.GetAll().First().Trajectory.Id);
            Assert.Equal(graphic.Id, trajectoryGraphicRelationalTable.GetAll().First().Graphic.Id);
        }

        [Fact(DisplayName = "Should Throw Exception When Fk Dependency Not Satisfy (ManyToMany)")]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var graphicTable = new Table<Graphic>(_dataTest.Graphics, g => g.Id, _relation, _index);
            var trajectoryGraphicRelationalTable =
                new Table<TrajectoryGraphicRelationalTable>(_dataTest.TrajectoryGraphicRelationalTables, tgr => tgr.Id,
                    _relation, _index);

            _relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
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

            Assert.Equal(trajectory.Id, trajectoryGraphicRelationalTable.GetAll().First().Trajectory.Id);
            Assert.Equal(graphic.Id, trajectoryGraphicRelationalTable.GetAll().First().Graphic.Id);

            trajectoryGraphic.Trajectory = new Trajectory();

            var ex = Assert.Throws<InvalidOperationException>(() => { trajectoryGraphicRelationalTable.Update(trajectoryGraphic); });

            Assert.Equal("Invalid FK", ex.Message);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(1 == graphicTable.Count);
            Assert.True(1 == trajectoryGraphicRelationalTable.Count);

            Assert.Equal(trajectory.Id, trajectoryGraphicRelationalTable.GetAll().First().Trajectory.Id);
            Assert.Equal(graphic.Id, trajectoryGraphicRelationalTable.GetAll().First().Graphic.Id);
        }
    }
}