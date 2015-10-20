using System;
using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.TableTest
{
    public class TableInsertion
    {
        private readonly DataTest.DataTest _dataTest;

        public TableInsertion()
        {
            _dataTest = new DataTest.DataTest();
        }

        [Fact]
        public void ShouldInsertWellInWellTable()
        {
            var wellTable = new Table<Well>(_dataTest.Wells, w => w.Id);

            var well = new Well(new Geometry(), new Trajectory());

            wellTable.Insert(well);

            Assert.Equal(well.Id, wellTable.Get(well.Id).Id);
        }

        [Fact]
        public void ShouldInsertWellInWellTableAndInsertedIsACopy()
        {
            var wellTable = new Table<Well>(_dataTest.Wells, w => w.Id);

            var well = new Well(new Geometry(), new Trajectory());

            wellTable.Insert(well);

            Assert.Equal(well.Id, wellTable.Get(well.Id).Id);
            Assert.NotEqual(well, wellTable.Get(well.Id));
        }

        [Fact]
        public void ShouldThrowExceptionIfTheSameEntityIsInserted()
        {
            var wellTable = new Table<Well>(_dataTest.Wells, w => w.Id);

            var well = new Well(new Geometry(), new Trajectory());

            wellTable.Insert(well);

            var ex = Assert.Throws<ArgumentException>(() => { wellTable.Insert(well); });

            Assert.Equal("Already exist an entity with this id", ex.Message);

            Assert.True(1 == wellTable.Count);
        }
    }
}