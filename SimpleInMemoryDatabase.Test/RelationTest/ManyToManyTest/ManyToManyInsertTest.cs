using System;
using System.Linq;
using SimpleInMemoryDatabase.Test.EntitiesTest;
using SimpleInMemoryDatabase.Test.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Test.RelationTest.ManyToManyTest
{
    public class ManyToManyInsertTest : TestBase
    {
        [Fact(DisplayName = "Should Insert ManyToMany If Fk Satisfy")]
        public void ShouldInsertManyToManyIfFkSatisfy()
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

            Assert.Equal(trajectory.Id, trajectoryGraphicRelationalTable.GetAll().First().Trajectory.Id);
            Assert.Equal(graphic.Id, trajectoryGraphicRelationalTable.GetAll().First().Graphic.Id);
        }

        [Fact(DisplayName = "Should Throw Exception When Fk Dependency Not Satisfy - Insert (ManyToMany)")]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
        {
            var trajectory = new Trajectory();
            var graphic = new Graphic();
            var trajectoryGraphic = new TrajectoryGraphicRelationalTable(trajectory, graphic);

            Relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

            var ex = Assert.Throws<InvalidOperationException>(() => { trajectoryGraphicRelationalTable.Insert(trajectoryGraphic); });

            Assert.Equal("Invalid FK", ex.Message);

            Assert.True(0 == trajectoryTable.Count);
            Assert.True(0 == graphicTable.Count);
            Assert.True(0 == trajectoryGraphicRelationalTable.Count);
        }
    }
}