using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.RelationTest.OneToManyTest
{
    public class OneToManyUpdateTest
    {
        private readonly DataTest.DataTest _dataTest;
        private readonly Index _index;
        private readonly Relation _relation;

        #region Construtores

        public OneToManyUpdateTest()
        {
            _dataTest = new DataTest.DataTest();
            _index = new Index();
            _relation = new Relation();
        }

        #endregion
        [Fact(DisplayName = "Update if Fk Satisfy (OneToMany)")]
        public void ShouldUpdateIfFkSatisfy()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var trajectoryPointTable = new Table<TrajectoryPoint>(_dataTest.TrajectoryPoints, tp => tp.Id, _relation,
                _index);

            _relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id);

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
                if(trajectory.Id== trajectoryPoint.Trajectory.Id)
                    countForTrajectory++;

                if (trajectory2.Id == trajectoryPoint.Trajectory.Id)
                    countForTrajectory2++;
            }

            Assert.Equal(2, countForTrajectory);
            Assert.Equal(1,countForTrajectory2);

        }
        [Fact(DisplayName = "Throw Exception When Fk Dependency Not Satisfy (OneToMany)")]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
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
