﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.TableTest
{
    public class TableDeleteTests : TestBase
    {
        [Fact]
        public void ShouldDeleteAll()
        {
            var traj1 = new Trajectory();
            var traj2 = new Trajectory();
            var traj3 = new Trajectory();

            base.Db.Insert<Trajectory>(new List<Trajectory>{traj1,traj2,traj3});

            Assert.Equal(3,Db.Count<Trajectory>());

            Db.DeleteAll<Trajectory>();

            Assert.Equal(0, Db.Count<Trajectory>());
        }
    }
}
