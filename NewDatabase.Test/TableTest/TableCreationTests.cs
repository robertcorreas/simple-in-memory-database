using NewDatabase.Core;
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
            var wellTable = new Table<Well>(_dataTest.Wells, w => w.Id);

            Assert.NotNull(wellTable);
        }
    }
}