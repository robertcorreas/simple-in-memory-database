using System;
using System.Collections;
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
        private readonly Index _index;
        private readonly Relation _relation;

        public Table(Dictionary<Guid, T> tuplas, Expression<Func<T, Guid>> primaryKey, Relation relation = null, Index index = null)
        {
            PrimaryKey = primaryKey.Compile();
            _tuplas = tuplas;
            _index = index ?? new Index();
            _relation = relation ?? new Relation();
        }

        public void Insert(T entity)
        {
            if (_tuplas.ContainsKey(PrimaryKey(entity)))
                throw new ArgumentException("Already exist an entity with this id");

            var relationProperties = _relation.Get(this.GetType());

            foreach (var relationPropertie in relationProperties)
            {
                if (relationPropertie.TableWithDependency == this.GetType())
                {
                    var foreignKey = relationPropertie.ForeignKey(entity);

                    if (!_index.Contains(foreignKey)) throw new InvalidOperationException("Invalid FK");

                }
                if (relationPropertie.TableDependency == this.GetType())
                {
                    //TODO
                }
            }


            var entityCopy = ExpressionTreeCloner.DeepFieldClone(entity);

            _tuplas.Add(PrimaryKey(entity), entityCopy);
            _index.CreateIndex(PrimaryKey(entityCopy));
        }

        public T Get(Guid primaryKey)
        {
            return _tuplas[primaryKey]; //TODO: test get with invalid primaryKey

        }

        public void Delete(Guid primaryKey)
        {
            var entity = _tuplas[primaryKey];

            var relationProperties = _relation.Get(this.GetType());

            foreach (var relationPropertie in relationProperties)
            {
                if (relationPropertie.TableWithDependency == this.GetType())
                {
                    relationPropertie.DeleteOperation(relationPropertie.TableDependency, relationPropertie.ForeignKey(entity));
                }
            }

            _tuplas.Remove(primaryKey);
            _index.DeleteIndex(primaryKey);
        }

        public void Delete(T entity)
        {
            var primaryKey = PrimaryKey(entity);

            Delete(primaryKey);
        }

        public List<T> GetAll()
        {
            return _tuplas.Values.Select(ExpressionTreeCloner.DeepFieldClone).ToList();
        }
    }
}
