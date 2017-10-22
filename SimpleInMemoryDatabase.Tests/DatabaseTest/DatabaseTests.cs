using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInMemoryDatabase.Core;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.DatabaseTest
{
    public class DatabaseTests
    {

        public DatabaseTests()
        {

        }

        [Fact]
        public void DeveCriar()
        {
            var db = new Database();

            db.CreateTable<Well>(x => x.Id);

            for (int i = 0; i < 100000 * 9; i++)
            {
                var w = new Well(new Geometry(), new Trajectory());
                db.Insert(w);
            }

            var wells = db.GetAll<Well>();

            Assert.Equal(100000 * 9, wells.Count());
        }
    }
}
