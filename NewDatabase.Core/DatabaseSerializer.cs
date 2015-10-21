using Newtonsoft.Json;

namespace NewDatabase.Core
{
    public class DatabaseSerializer
    {
        private readonly Data _data;
        private readonly Index _index;
        private readonly Relation _relation;

        #region Construtores

        public DatabaseSerializer(Data data, Relation relation, Index index)
        {
            _data = data;
            _relation = relation;
            _index = index;
        }

        #endregion

        public JsonDatabase Serialize()
        {
            var jsonData = JsonConvert.SerializeObject(_data, Formatting.Indented);
            var jsonIndex = JsonConvert.SerializeObject(_index, Formatting.Indented);

            return new JsonDatabase
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