using NewDatabase.Test.Helpers;
using Xunit;

namespace NewDatabase.Test.TableTest
{
    public class TableCreationTests : TesteBase
    {
        [Fact(DisplayName = "Should Create WellTable With Primary Key")]
        public void ShouldCreateWellTableWithPrimaryKey()
        {
            Assert.NotNull(wellTable);
        }
    }
}