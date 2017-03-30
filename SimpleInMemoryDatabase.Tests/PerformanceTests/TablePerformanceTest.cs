using System.Collections.Generic;
using System.Diagnostics;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.PerformanceTests
{
    public class TablePerformanceTest : TestBase
    {
        [Fact(DisplayName = "Should Insert In Good Time OneToMany")]
        public void ShouldInsertInGoodTimeOneToMany()
        {
            Relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id);

            var trajectory = new Trajectory();

            trajectoryTable.Insert(trajectory);
            var trajectoryPoints = new List<TrajectoryPoint>();
            for (var i = 0; i < 220000; i++)
            {
                trajectoryPoints.Add(new TrajectoryPoint(trajectory));
            }

            var timeWatch = Stopwatch.StartNew();
            trajectoryPointTable.Insert(trajectoryPoints);
            timeWatch.Stop();

            const int expectedTime = 600;

            Assert.True(timeWatch.ElapsedMilliseconds <= expectedTime, string.Format("Time expected: {0} - Time result: {1}", expectedTime, timeWatch.ElapsedMilliseconds));
        }

        [Fact(DisplayName = "Should Delete In Good Time OneToMany")]
        public void ShouldDeleteInGoodTimeOneToMany()
        {
            Relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();

            trajectoryTable.Insert(trajectory1);
            trajectoryTable.Insert(trajectory2);


            var trajectoryPoints = new List<TrajectoryPoint>();
            for (var i = 0; i < 220000; i++)
            {
                trajectoryPoints.Add(new TrajectoryPoint(trajectory1));
                trajectoryPoints.Add(new TrajectoryPoint(trajectory2));
            }

            trajectoryPointTable.Insert(trajectoryPoints);

            var timeWatch = Stopwatch.StartNew();
            trajectoryPointTable.Delete(trajectoryPoints, tp => tp.Trajectory.Id == trajectory2.Id);
            timeWatch.Stop();

            const int expectedTime = 600;

            Assert.True(timeWatch.ElapsedMilliseconds <= expectedTime, string.Format("Time expected: {0} - Time result: {1}", expectedTime, timeWatch.ElapsedMilliseconds));

            var all = trajectoryPointTable.GetAll();

            Assert.Equal(220000, all.Count);
            foreach (var t in all)
            {
                Assert.Equal(t.Trajectory.Id, trajectory1.Id);
            }
        }
    }
}