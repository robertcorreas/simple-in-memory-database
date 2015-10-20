using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDatabase.Core
{
    public class Index
    {
        public Dictionary<Guid, bool> Indexes { get; private set; }

        public Index()
        {
            Indexes = new Dictionary<Guid, bool>();
        }

        public int Count
        {
            get { return Indexes.Count; }
        }

        public bool Contains(Guid foreignKey)
        {
            return Indexes.ContainsKey(foreignKey);
        }

        public void CreateIndex(Guid id)
        {
            Indexes.Add(id,true);
        }

        public void DeleteIndex(Guid primaryKey)
        {
            Indexes.Remove(primaryKey);
        }
    }
}
