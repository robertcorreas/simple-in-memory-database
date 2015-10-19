using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewDatabase.Core;
using NewDatabase.Test.DataTest;
using NewDatabase.Test.EntitiesTest;
using Xunit;

namespace NewDatabase.Test.TableTest
{
    public class TableInsertion
    {
        private readonly Data _data;

        public TableInsertion()
        {
            _data = new Data();
        }

        [Fact]
        public void ShouldInsertWellInWellTable()
        {
            var wellTable = new Table<Well>(tuplas: _data.Wells, primaryKey: w => w.Id);

            var well = new Well();

            wellTable.Insert(well);

            Assert.Equal(well.Id,wellTable.Get(well.Id).Id);
        }

        [Fact]
        public void ShouldInsertWellInWellTableAndInsertedIsACopy()
        {
            var wellTable = new Table<Well>(tuplas: _data.Wells, primaryKey: w => w.Id);

            var well = new Well();

            wellTable.Insert(well);

            Assert.Equal(well.Id, wellTable.Get(well.Id).Id);
            Assert.NotEqual(well, wellTable.Get(well.Id));
        }

        [Fact]
        public void ShouldThrowExceptionIfTheSameEntityIsInserted()
        {
            var wellTable = new Table<Well>(tuplas: _data.Wells, primaryKey: w => w.Id);

            var well = new Well();

            wellTable.Insert(well);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                wellTable.Insert(well);
            });

            Assert.Equal("Already exist an entity with this id", ex.Message);

            Assert.True(1 == wellTable.Count);
        }
    }

}
