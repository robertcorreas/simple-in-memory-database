using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Nuclex.Cloning;

namespace NewDatabase.Core
{
    public class Table<T> where T : Entity
    {
        public Func<T, Guid> PrimaryKey { get; private set; }

        public int Count
        {
            get { return _tuplas.Count; }
        }

        private readonly Dictionary<Guid, T> _tuplas;

        public Table(Dictionary<Guid,T> tuplas, Expression<Func<T,Guid>> primaryKey)
        {
            PrimaryKey = primaryKey.Compile();
            _tuplas = tuplas;
        }

        public void Insert(T entity)
        {
            if(_tuplas.ContainsKey(PrimaryKey(entity)))
                throw new ArgumentException("Already exist an entity with this id");

            var entityCopy = ExpressionTreeCloner.DeepFieldClone(entity);

            _tuplas.Add(PrimaryKey(entity), entityCopy);
        }

        public T Get(Guid primaryKey)
        {
            return _tuplas[primaryKey]; //TODO: test get with invalid primaryKey
        }
    }
}
