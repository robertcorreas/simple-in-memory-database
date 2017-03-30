﻿using System;
using System.Linq;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.RelationTest.ManyToManyTest
{
    public class ManyToManyUpdateTest : TestBase
    {
        [Fact(DisplayName = "Should Update if Fk Satisfy (ManyToMany)")]
        public void ShouldUpdateIfFkSatisfy()
        {
            Relation.CreateManyToMany(trajectoryTable, graphicTable, trajectoryGraphicRelationalTable,
                tgr => tgr.Trajectory.Id, tgr => tgr.Graphic.Id);

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

        [Fact(DisplayName = "Should Throw Exception When Fk Dependency Not Satisfy - Update(ManyToMany)")]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
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