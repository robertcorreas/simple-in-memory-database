using System;
using NewDatabase.Test.EntitiesTest;
using NewDatabase.Test.Helpers;
using Xunit;

namespace NewDatabase.Test.RelationTest.OneToManyTest
{
    public class OneToManyUpdateTest : TesteBase
    {
        [Fact(DisplayName = "Update if Fk Satisfy (OneToMany)")]
        public void ShouldUpdateIfFkSatisfy()
        {
            Relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id);

            var trajectory = new Trajectory();
            var trajectory2 = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory);
            var tp2 = new TrajectoryPoint(trajectory);
            var tp3 = new TrajectoryPoint(trajectory);

            trajectoryTable.Insert(trajectory);
            trajectoryTable.Insert(trajectory2);
            trajectoryPointTable.Insert(tp1);
            trajectoryPointTable.Insert(tp2);
            trajectoryPointTable.Insert(tp3);

            Assert.True(2 == trajectoryTable.Count);
            Assert.True(3 == trajectoryPointTable.Count);

            foreach (var trajectoryPoint in trajectoryPointTable.GetAll())
            {
                Assert.Equal(trajectory.Id, trajectoryPoint.Trajectory.Id);
            }

            tp2.Trajectory = trajectory2;
            trajectoryPointTable.Update(tp2);

            var countForTrajectory = 0;
            var countForTrajectory2 = 0;
            foreach (var trajectoryPoint in trajectoryPointTable.GetAll())
            {
                if (trajectory.Id == trajectoryPoint.Trajectory.Id)
                    countForTrajectory++;

                if (trajectory2.Id == trajectoryPoint.Trajectory.Id)
                    countForTrajectory2++;
            }

            Assert.Equal(2, countForTrajectory);
            Assert.Equal(1, countForTrajectory2);
        }

        [Fact(DisplayName = "Throw Exception When Fk Dependency Not Satisfy (OneToMany)")]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
        {
            Relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id);

            var trajectory = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory);
            var tp2 = new TrajectoryPoint(trajectory);
            var tp3 = new TrajectoryPoint(trajectory);

            trajectoryTable.Insert(trajectory);
            trajectoryPointTable.Insert(tp1);
            trajectoryPointTable.Insert(tp2);
            trajectoryPointTable.Insert(tp3);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(3 == trajectoryPointTable.Count);

            foreach (var trajectoryPoint in trajectoryPointTable.GetAll())
            {
                Assert.Equal(trajectory.Id, trajectoryPoint.Trajectory.Id);
            }
            tp2.Trajectory = new Trajectory();

            var ex = Assert.Throws<InvalidOperationException>(() => { trajectoryPointTable.Update(tp2); });

            Assert.Equal("Invalid FK", ex.Message);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(3 == trajectoryPointTable.Count);

            foreach (var trajectoryPoint in trajectoryPointTable.GetAll())
            {
                Assert.Equal(trajectory.Id, trajectoryPoint.Trajectory.Id);
            }
        }
    }
}