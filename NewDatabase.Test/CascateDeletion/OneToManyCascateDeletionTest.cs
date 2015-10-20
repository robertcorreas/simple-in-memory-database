using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.CascateDeletion
{
    public class OneToManyCascateDeletionTest
    {
        private readonly DataTest.DataTest _dataTest;
        private readonly Index _index;
        private readonly Relation _relation;

        public OneToManyCascateDeletionTest()
        {
            _dataTest = new DataTest.DataTest();
            _relation = new Relation();
            _index = new Index();
        }

        [Fact]
        public void ShouldDeleteInCascate()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var trajectoryPointTable = new Table<TrajectoryPoint>(_dataTest.TrajectoryPoints, tp => tp.Id, _relation,
                _index);

            _relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id);

            var trajectory = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory);
            var tp2 = new TrajectoryPoint(trajectory);
            var tp3 = new TrajectoryPoint(trajectory);

            trajectoryTable.Insert(trajectory);
            trajectoryPointTable.Insert(tp1);
            trajectoryPointTable.Insert(tp2);
            trajectoryPointTable.Insert(tp3);

            trajectoryTable.Delete(trajectory);

            Assert.True(0 == trajectoryTable.Count);
            Assert.True(0 == trajectoryPointTable.Count);
        }

        [Fact]
        public void ShouldNotDeleteInCascate()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var trajectoryPointTable = new Table<TrajectoryPoint>(_dataTest.TrajectoryPoints, tp => tp.Id, _relation,
                _index);

            _relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id,false);

            var trajectory = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory);
            var tp2 = new TrajectoryPoint(trajectory);
            var tp3 = new TrajectoryPoint(trajectory);

            trajectoryTable.Insert(trajectory);
            trajectoryPointTable.Insert(tp1);
            trajectoryPointTable.Insert(tp2);
            trajectoryPointTable.Insert(tp3);

            trajectoryTable.Delete(trajectory);

            Assert.True(0 == trajectoryTable.Count);
            Assert.True(3 == trajectoryPointTable.Count);
        }

        [Fact]
        public void ShouldDeleteInCascateWithMultipleOneToMany()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var trajectoryPointTable = new Table<TrajectoryPoint>(_dataTest.TrajectoryPoints, tp => tp.Id, _relation,
                _index);

            _relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id, true);

            var trajectory1 = new Trajectory();
            var trajectory2 = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory1);
            var tp2 = new TrajectoryPoint(trajectory2);
            var tp3 = new TrajectoryPoint(trajectory2);
            var tp4 = new TrajectoryPoint(trajectory1);

            trajectoryTable.Insert(trajectory1);
            trajectoryTable.Insert(trajectory2);
            trajectoryPointTable.Insert(tp1);
            trajectoryPointTable.Insert(tp2);
            trajectoryPointTable.Insert(tp3);
            trajectoryPointTable.Insert(tp4);

            trajectoryTable.Delete(trajectory1);

            Assert.True(1 == trajectoryTable.Count);
            Assert.True(2 == trajectoryPointTable.Count);

            Assert.Equal(trajectory2.Id,trajectoryTable.Get(trajectory2.Id).Id);

            foreach (var trajectoryPoint in trajectoryPointTable.GetAll())
            {
                Assert.Equal(trajectory2.Id, trajectoryPoint.Trajectory.Id);
            }
        }
    }
}