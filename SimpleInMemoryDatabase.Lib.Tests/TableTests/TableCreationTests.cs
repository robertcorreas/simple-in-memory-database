using SimpleInMemoryDatabase.Tests.EntitiesTest;
using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Lib.Tests.TableTests
{
    public class TableCreationTests : TestBase
    {
        [Fact(DisplayName = "Should Create WellTable With Primary Key")]
        public void ShouldCreateWellTableWithPrimaryKey()
        {
            Assert.Empty(Db.GetAll<Well>());
        }
    }
}