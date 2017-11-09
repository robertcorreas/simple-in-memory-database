using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.TableTest
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

            Db.Insert(trajectory1);
            Db.Insert(trajectory2);

            Db.Insert(tp1);
            Db.Insert(tp2);
            Db.Insert(tp3);
            Db.Insert(tp4);
            Db.Insert(tp5);
            Db.Insert(tp6);

            var founded = Db.Search<TrajectoryPoint>(tp => tp.Trajectory.Id == trajectory2.Id).ToList();

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
           
            Db.Insert(trajectory1);
            Db.Insert(tp1);
            
            var founded = Db.Search<TrajectoryPoint>(tp => tp.Trajectory.Id == trajectory1.Id).First();
            founded.Trajectory = new Trajectory();

            var newFounded = Db.Search<TrajectoryPoint>(tp => tp.Trajectory.Id == trajectory1.Id).First();

            Assert.Equal(founded.Id,newFounded.Id);
            Assert.NotEqual(founded.Trajectory.Id, newFounded.Trajectory.Id);
        }
    }
}
