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
        private readonly DataTest.DataTest _dataTest;
        public TableCreationTests()
        {
            _dataTest = new DataTest.DataTest();
        }

        [Fact]
        public void ShouldCreateWellTableWithPrimaryKey()
        {
            var wellTable = new Table<Well>(tuplas: _dataTest.Wells,primaryKey: w => w.Id);

            Assert.NotNull(wellTable);
        }
    }


}
