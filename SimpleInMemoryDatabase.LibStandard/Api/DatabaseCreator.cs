using SimpleInMemoryDatabase.Lib.Core;

namespace SimpleInMemoryDatabase.Lib.Api
{
    public static class DatabaseCreator
    {
        public static IDatabase Create()
        {
            return new Database();
        }
    }
}
