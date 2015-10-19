using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewDatabase.Test.EntitiesTest;

namespace NewDatabase.Test.DataTest
{
    public class Data 
    {
        public Dictionary<Guid, Well> Wells { get; set; }

        public Data()
        {
            Wells = new Dictionary<Guid, Well>();
        }
    }
}
