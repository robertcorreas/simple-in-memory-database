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
    public class TableCreationTests
    {
        private readonly Data _data;
        public TableCreationTests()
        {
            _data = new Data();
        }

        [Fact]
        public void ShouldCreateWellTableWithPrimaryKey()
        {
            var wellTable = new Table<Well>(tuplas: _data.Wells,primaryKey: w => w.Id);

            Assert.NotNull(wellTable);
        }
    }


}
