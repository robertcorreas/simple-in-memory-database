using SimpleInMemoryDatabase.Tests.Helpers;
using Xunit;

namespace SimpleInMemoryDatabase.Tests.TableTest
{
    public class TableCreationTests : TestBase
    {
        [Fact(DisplayName = "Should Create WellTable With Primary Key")]
        public void ShouldCreateWellTableWithPrimaryKey()
        {
            Assert.NotNull(wellTable);
        }
    }
}