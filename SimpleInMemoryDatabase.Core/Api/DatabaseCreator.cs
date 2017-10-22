using SimpleInMemoryDatabase.Core.DatabaseCore;

namespace SimpleInMemoryDatabase.Core.Api
{
    public static class DatabaseCreator
    {
        public static IDatabase Create()
        {
            return new Database();
        }
    }
}
