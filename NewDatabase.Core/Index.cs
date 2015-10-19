using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDatabase.Core
{
    public class Index
    {
        private readonly Dictionary<Guid, bool> _indexes;

        public Index()
        {
            _indexes = new Dictionary<Guid, bool>();
        }

        public int Count
        {
            get { return _indexes.Count; }
        }

        public bool Contains(Guid foreignKey)
        {
            return _indexes.ContainsKey(foreignKey);
        }

        public void CreateIndex(Guid id)
        {
            _indexes.Add(id,true);
        }

        public void DeleteIndex(Guid primaryKey)
        {
            _indexes.Remove(primaryKey);
        }
    }
}
