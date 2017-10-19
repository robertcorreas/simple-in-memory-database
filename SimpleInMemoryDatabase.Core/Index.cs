using System;
using System.Collections.Generic;

namespace SimpleInMemoryDatabase.Core
{
    public class Index
    {
        #region Construtores

        public Index()
        {
            _indexes = new HashSet<Guid>();
        }

        #endregion

        #region Propriedades

        private HashSet<Guid> _indexes;

        public int Count => _indexes.Count;

        #endregion

        public bool Contains(Guid foreignKey)
        {
            return _indexes.Contains(foreignKey);
        }

        public void CreateIndex(Guid id)
        {
            _indexes.Add(id);
        }

        public void DeleteIndex(Guid primaryKey)
        {
            _indexes.Remove(primaryKey);
        }
    }
}