using System;
using System.Collections.Generic;

namespace SimpleInMemoryDatabase.Core
{
    public class Index
    {
        #region Construtores

        public Index()
        {
            Indexes = new Dictionary<Guid, bool>();
        }

        #endregion

        #region Propriedades

        public Dictionary<Guid, bool> Indexes { get; private set; }

        public int Count
        {
            get { return Indexes.Count; }
        }

        #endregion

        public bool Contains(Guid foreignKey)
        {
            return Indexes.ContainsKey(foreignKey);
        }

        public void CreateIndex(Guid id)
        {
            Indexes.Add(id, true);
        }

        public void DeleteIndex(Guid primaryKey)
        {
            Indexes.Remove(primaryKey);
        }
    }
}