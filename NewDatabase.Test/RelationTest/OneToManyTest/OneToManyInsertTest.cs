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
    
    public class OneToManyInsertTest
    {
        private readonly DataTest.DataTest _dataTest;
        private readonly Relation _relation;
        private readonly Index _index;
        

        public OneToManyInsertTest()
        {
            _dataTest = new DataTest.DataTest();
            _relation = new Relation();
            _index= new Index();

        }

        [Fact]
        public void ShouldInsertOneToManyIfFkIsSatisfy()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories,t=>t.Id,_relation,_index);
            var trajectoryPointTable = new Table<TrajectoryPoint>(_dataTest.TrajectoryPoints, tp => tp.Id, _relation, _index);

            _relation.CreateOneToMany(trajectoryTable,trajectoryPointTable, tp => tp.Trajectory.Id);

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
                Assert.Equal(trajectory.Id,trajectoryPoint.Trajectory.Id);
            }
        }

        [Fact]
        public void ShouldThrowExceptionWhenFkDependencyNotSatisfy()
        {
            var trajectoryTable = new Table<Trajectory>(_dataTest.Trajectories, t => t.Id, _relation, _index);
            var trajectoryPointTable = new Table<TrajectoryPoint>(_dataTest.TrajectoryPoints, tp => tp.Id, _relation, _index);

            _relation.CreateOneToMany(trajectoryTable, trajectoryPointTable, tp => tp.Trajectory.Id);

            var trajectory = new Trajectory();

            var tp1 = new TrajectoryPoint(trajectory);

            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                trajectoryPointTable.Insert(tp1);
            });


            Assert.Equal("Invalid FK",ex.Message);
            Assert.True(0 == trajectoryTable.Count);
            Assert.True(0 == trajectoryPointTable.Count);
        }
    }
}
