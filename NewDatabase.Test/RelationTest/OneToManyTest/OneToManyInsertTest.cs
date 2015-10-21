using System;
using NewDatabase.Test.EntitiesTest;
using NewDatabase.Test.Helpers;
using Xunit;

namespace NewDatabase.Test.RelationTest.OneToManyTest
{
    public class OneToManyInsertTest : TesteBase
    {
        [Fact(DisplayName = "Should Insert OneToMany If Fk Is Satisfy")]
        public void ShouldInsertOneToManyIfFkIsSatisfy()
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
        }

        [Fact(DisplayName = "Should Throw Exception When Fk Dependency Not Satisfy (OneToMany)")]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
        {
            Relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id);

            var trajectory = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory);

            var ex = Assert.Throws<InvalidOperationException>(() => { trajectoryPointTable.Insert(tp1); });


            Assert.Equal("Invalid FK", ex.Message);
            Assert.True(0 == trajectoryTable.Count);
            Assert.True(0 == trajectoryPointTable.Count);
        }
    }
}