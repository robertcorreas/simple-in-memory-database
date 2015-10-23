using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInMemoryDatabase.Test.EntitiesTest;
using SimpleInMemoryDatabase.Test.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Test.TableTest
{
    public class TableSearchTest : TestBase
    {
        [Fact(DisplayName = "Should Search")]
        public void ShouldSearch()
        {
            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory1);
            var tp2 = new TrajectoryPoint(trajectory2);
            var tp3 = new TrajectoryPoint(trajectory2);
            var tp4 = new TrajectoryPoint(trajectory1);
            var tp5 = new TrajectoryPoint(trajectory2);
            var tp6 = new TrajectoryPoint(trajectory1);

            trajectoryTable.Insert(trajectory1);
            trajectoryTable.Insert(trajectory2);

            trajectoryPointTable.Insert(tp1);
            trajectoryPointTable.Insert(tp2);
            trajectoryPointTable.Insert(tp3);
            trajectoryPointTable.Insert(tp4);
            trajectoryPointTable.Insert(tp5);
            trajectoryPointTable.Insert(tp6);

            var founded = trajectoryPointTable.Search(tp => tp.Trajectory.Id == trajectory2.Id).ToList();

            var expected = new List<TrajectoryPoint>()
            {
                tp2, tp3, tp5
            };

            Assert.Equal(expected.Count, founded.Count);

            for (int i = 0; i < founded.Count; i++)
            {
                Assert.Equal(expected[i].Id, founded[i].Id);
                Assert.Equal(expected[i].Trajectory.Id, founded[i].Trajectory.Id);
            }
        }

        [Fact(DisplayName = "Should Return Copy From Search")]
        public void ShouldReturnCopyFromSearch()
        {
            var trajectory1 = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory1);
           
            trajectoryTable.Insert(trajectory1);

            trajectoryPointTable.Insert(tp1);
            
            var founded = trajectoryPointTable.Search(tp => tp.Trajectory.Id == trajectory1.Id).First();
            founded.Trajectory = new Trajectory();

            var newFounded = trajectoryPointTable.Search(tp => tp.Trajectory.Id == trajectory1.Id).First();

            Assert.Equal(founded.Id,newFounded.Id);
            Assert.NotEqual(founded.Trajectory.Id, newFounded.Trajectory.Id);
        }
    }
}
