using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.CascateDeletion
{
    public class OneToManyCascateDeletionTest : TestBase
    {
        [Fact(DisplayName = "Should Delete In Cascate (OneToMany)")]
        public void ShouldDeleteInCascate()
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

            Db.Delete(trajectory);

            Assert.True(0 == Db.Count<Trajectory>());
            Assert.True(0 == Db.Count<TrajectoryPoint>());
        }

        [Fact(DisplayName = "Should Not Delete In Cascate")]
        public void ShouldNotDeleteInCascate()
        {
            Db.CreateOneToMany<Trajectory,TrajectoryPoint>(tp => tp.Trajectory.Id, false);

            var trajectory = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory);
            var tp2 = new TrajectoryPoint(trajectory);
            var tp3 = new TrajectoryPoint(trajectory);

            Db.Insert(trajectory);
            Db.Insert(tp1);
            Db.Insert(tp2);
            Db.Insert(tp3);

            Db.Delete(trajectory);

            Assert.True(0 == Db.Count<Trajectory>());
            Assert.True(3 == Db.Count<TrajectoryPoint>());
        }

        [Fact(DisplayName = "Should Delete In Cascate With Multiple OneToMany")]
        public void ShouldDeleteInCascateWithMultipleOneToMany()
        {
            Db.CreateOneToMany<Trajectory,TrajectoryPoint>(tp => tp.Trajectory.Id, true);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory1);
            var tp2 = new TrajectoryPoint(trajectory2);
            var tp3 = new TrajectoryPoint(trajectory2);
            var tp4 = new TrajectoryPoint(trajectory1);

            Db.Insert(trajectory1);
            Db.Insert(trajectory2);
            Db.Insert(tp1);
            Db.Insert(tp2);
            Db.Insert(tp3);
            Db.Insert(tp4);

            Db.Delete(trajectory1);

            Assert.True(1 == Db.Count<Trajectory>());
            Assert.True(2 == Db.Count<TrajectoryPoint>());

            Assert.Equal(trajectory2.Id, Db.GetOne<Trajectory>(trajectory2.Id).Id);

            foreach (var trajectoryPoint in Db.GetAll<TrajectoryPoint>())
            {
                Assert.Equal(trajectory2.Id, trajectoryPoint.Trajectory.Id);
            }
        }
    }
}