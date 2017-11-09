using System;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.RelationTest.OneToManyTest
{
    public class OneToManyInsertTest : TestBase
    {
        [Fact(DisplayName = "Should Insert OneToMany If Fk Is Satisfy")]
        public void ShouldInsertOneToManyIfFkIsSatisfy()
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
        }

        [Fact(DisplayName = "Should Throw Exception When Fk Dependency Not Satisfy (OneToMany)")]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
        {
            Db.CreateOneToMany<Trajectory,TrajectoryPoint>(tp => tp.Trajectory.Id);

            var trajectory = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory);

            var ex = Assert.Throws<InvalidOperationException>(() => { Db.Insert(tp1); });


            Assert.Equal("Invalid FK", ex.Message);
            Assert.True(0 == Db.Count<Trajectory>());
            Assert.True(0 == Db.Count<TrajectoryPoint>());
        }
    }
}