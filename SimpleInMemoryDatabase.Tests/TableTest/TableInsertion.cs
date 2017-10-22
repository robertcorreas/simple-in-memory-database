using System;
using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.TableTest
{
    public class TableInsertion : TestBase
    {
        [Fact(DisplayName = "Should Insert Well In Well Table")]
        public void ShouldInsertWellInWellTable()
        {
            var well = new Well(new Geometry(), new Trajectory());

            Db.Insert(well);

            Assert.Equal(well.Id, Db.GetOne<Well>(well.Id).Id);
        }

        [Fact(DisplayName = "Should Insert Well In Well Table And Inserted Is A Copy")]
        public void ShouldInsertWellInWellTableAndInsertedIsACopy()
        {
            var well = new Well(new Geometry(), new Trajectory());

            Db.Insert(well);

            Assert.Equal(well.Id, Db.GetOne<Well>(well.Id).Id);
            Assert.NotEqual(well, Db.GetOne<Well>(well.Id));
        }

        [Fact(DisplayName = "Should Throw Exception If The Same Entity Is Inserted")]
        public void ShouldThrowExceptionIfTheSameEntityIsInserted()
        {
            var well = new Well(new Geometry(), new Trajectory());

            Db.Insert(well);

            var ex = Assert.Throws<ArgumentException>(() => { Db.Insert(well); });

            Assert.Equal("Already exist an entity with this id", ex.Message);

            Assert.True(1 == Db.Count<Well>());
        }
    }
}