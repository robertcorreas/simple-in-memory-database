using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInMemoryDatabase.Core
{
    public static class DatabaseCreator
    {
        public static IDatabase Create()
        {
            return new Database();
        }
    }
}
