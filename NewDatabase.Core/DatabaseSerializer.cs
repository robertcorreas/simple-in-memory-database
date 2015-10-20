using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NewDatabase.Core
{
    public class DatabaseSerializer
    {
        private readonly Data _data;
        private readonly Relation _relation;
        private readonly Index _index;

        public DatabaseSerializer(Data data , Relation relation, Index index)
        {
            _data = data;
            _relation = relation;
            _index = index;
        }

        public JsonDatabase Serialize()
        {
            var jsonData = JsonConvert.SerializeObject(_data, Formatting.Indented);
            var jsonIndex = JsonConvert.SerializeObject(_index, Formatting.Indented);

            return new JsonDatabase()
            {
                JsonData = jsonData,
                JsonIndex = jsonIndex
            };

        }

        public void Deserialize(out Data data, out Index index, JsonDatabase jsonDatabase)
        {
            data = JsonConvert.DeserializeObject<Data>(jsonDatabase.JsonData);
            index = JsonConvert.DeserializeObject<Index>(jsonDatabase.JsonIndex);

                
        }
    }
}
