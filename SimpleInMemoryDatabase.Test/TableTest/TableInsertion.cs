using System;
using SimpleInMemoryDatabase.Test.EntitiesTest;
using SimpleInMemoryDatabase.Test.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Test.TableTest
{
    public class TableInsertion : TestBase
    {
        [Fact(DisplayName = "Should Insert Well In Well Table")]
        public void ShouldInsertWellInWellTable()
        {
            var well = new Well(new Geometry(), new Trajectory());

            wellTable.Insert(well);

            Assert.Equal(well.Id, wellTable.Get(well.Id).Id);
        }

        [Fact(DisplayName = "Should Insert Well In Well Table And Inserted Is A Copy")]
        public void ShouldInsertWellInWellTableAndInsertedIsACopy()
        {
            var well = new Well(new Geometry(), new Trajectory());

            wellTable.Insert(well);

            Assert.Equal(well.Id, wellTable.Get(well.Id).Id);
            Assert.NotEqual(well, wellTable.Get(well.Id));
        }

        [Fact(DisplayName = "Should Throw Exception If The Same Entity Is Inserted")]
        public void ShouldThrowExceptionIfTheSameEntityIsInserted()
        {
            var well = new Well(new Geometry(), new Trajectory());

            wellTable.Insert(well);

            var ex = Assert.Throws<ArgumentException>(() => { wellTable.Insert(well); });

            Assert.Equal("Already exist an entity with this id", ex.Message);

            Assert.True(1 == wellTable.Count);
        }
    }
}