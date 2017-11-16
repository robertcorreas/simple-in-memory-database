using System.Collections.Generic;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Lib.Tests.TableTests
{
    public class TableDeleteTests : TestBase
    {
        [Fact(DisplayName = "Should Delete All Elements From Table")]
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
