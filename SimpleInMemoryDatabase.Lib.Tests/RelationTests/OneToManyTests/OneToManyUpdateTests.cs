using System;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Lib.Tests.RelationTests.OneToManyTests
{
    public class OneToManyUpdateTests : TestBase
    {
        [Fact(DisplayName = "Update If Fk Satisfy (OneToMany)")]
        public void ShouldUpdateIfFkSatisfy()
        {
            Db.CreateOneToMany<Trajectory,TrajectoryPoint>(tp => tp.Trajectory.Id);

            var trajectory = new Trajectory();
            var trajectory2 = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory);
            var tp2 = new TrajectoryPoint(trajectory);
            var tp3 = new TrajectoryPoint(trajectory);

            Db.Insert(trajectory);
            Db.Insert(trajectory2);
            Db.Insert(tp1);
            Db.Insert(tp2);
            Db.Insert(tp3);

            Assert.True(2 == Db.Count<Trajectory>());
            Assert.True(3 == Db.Count<TrajectoryPoint>());

            foreach (var trajectoryPoint in Db.GetAll<TrajectoryPoint>())
            {
                Assert.Equal(trajectory.Id, trajectoryPoint.Trajectory.Id);
            }

            tp2.Trajectory = trajectory2;
            Db.Update(tp2);

            var countForTrajectory = 0;
            var countForTrajectory2 = 0;
            foreach (var trajectoryPoint in Db.GetAll<TrajectoryPoint>())
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
            Db.CreateOneToMany<Trajectory,TrajectoryPoint>(tp => tp.Trajectory.Id);

            var trajectory = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory);
            var tp2 = new TrajectoryPoint(trajectory);
            var tp3 = new TrajectoryPoint(trajectory);

            Db.Insert(trajectory);
            Db.Insert(tp1);
            Db.Insert(tp2);
            Db.Insert(tp3);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(3 == Db.Count<TrajectoryPoint>());

            foreach (var trajectoryPoint in Db.GetAll<TrajectoryPoint>())
            {
                Assert.Equal(trajectory.Id, trajectoryPoint.Trajectory.Id);
            }
            tp2.Trajectory = new Trajectory();

            var ex = Assert.Throws<InvalidOperationException>(() => { Db.Update(tp2); });

            Assert.Equal("Invalid FK", ex.Message);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(3 == Db.Count<TrajectoryPoint>());

            foreach (var trajectoryPoint in Db.GetAll<TrajectoryPoint>())
            {
                Assert.Equal(trajectory.Id, trajectoryPoint.Trajectory.Id);
            }
        }
    }
}