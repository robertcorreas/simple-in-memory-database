using System;
using System.Collections.Generic;

namespace SimpleInMemoryDatabase.Core.DatabaseCore
{
    internal class Index
    {
        #region Construtores

        internal Index()
        {
            _indexes = new HashSet<Guid>();
        }

        #endregion

        internal bool Contains(Guid foreignKey)
        {
            return _indexes.Contains(foreignKey);
        }

        internal void CreateIndex(Guid id)
        {
            _indexes.Add(id);
        }

        internal void DeleteIndex(Guid primaryKey)
        {
            _indexes.Remove(primaryKey);
        }

        #region Propriedades

        private readonly HashSet<Guid> _indexes;

        internal int Count => _indexes.Count;

        #endregion
    }
}