using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nuclex.Cloning;

namespace NewDatabase.Core
{
    public class Table<T> where T : Entity
    {
        private readonly Index _index;
        private readonly Relation _relation;
        private readonly Dictionary<Guid, T> _tuplas;

        public Table(Dictionary<Guid, T> tuplas, Expression<Func<T, Guid>> primaryKey, Relation relation = null,
            Index index = null)
        {
            _primaryKey = primaryKey.Compile();
            _tuplas = tuplas;
            _index = index ?? new Index();
            _relation = relation ?? new Relation();
        }

        private readonly Func<T, Guid> _primaryKey;

        public int Count
        {
            get { return _tuplas.Count; }
        }

        public void Insert(T entity)
        {
            if (_tuplas.ContainsKey(_primaryKey(entity)))
                throw new ArgumentException("Already exist an entity with this id");

            var type = GetType();

            var relationProperties = _relation.Get(type);

            foreach (var relationPropertie in relationProperties)
            {
                if (relationPropertie.TableWithDependency == type &&
                    relationPropertie.RelationType != RelationType.ManyToMany)
                {
                    var foreignKey = relationPropertie.ForeignKey(entity);

                    if (!_index.Contains(foreignKey)) throw new InvalidOperationException("Invalid FK");
                }
                else if (relationPropertie.RelationalTable == type)
                {
                    var foreignKey1 = relationPropertie.ForeignKey1(entity);
                    var foreignKey2 = relationPropertie.ForeignKey2(entity);

                    if (!(_index.Contains(foreignKey1) && _index.Contains(foreignKey2)))
                        throw new InvalidOperationException("Invalid FK");
                }
            }


            var entityCopy = ExpressionTreeCloner.DeepFieldClone(entity);

            _tuplas.Add(_primaryKey(entity), entityCopy);
            _index.CreateIndex(_primaryKey(entityCopy));
        }

        public T Get(Guid primaryKey)
        {
            return   ExpressionTreeCloner.DeepFieldClone(_tuplas[primaryKey]);
        }

        public void Delete(Guid primaryKey)
        {
            var entity = _tuplas[primaryKey];
            var tableType = GetType();

            var relationProperties = _relation.Get(tableType);

            foreach (var relationPropertie in relationProperties)
            {
                if (relationPropertie.RelationType == RelationType.OneToOne)
                {
                    if (relationPropertie.TableWithDependency == tableType)
                    {
                        relationPropertie.DeleteOperation(relationPropertie.TableDependency,
                            relationPropertie.ForeignKey(entity));
                    }
                }
                else if (relationPropertie.RelationType == RelationType.OneToMany)
                {
                    if (relationPropertie.TableDependency == tableType)
                    {
                        relationPropertie.DeleteOperation(relationPropertie.TableWithDependency, primaryKey);
                    }
                }
                else
                {
                    relationPropertie.DeleteOperation(tableType, primaryKey);
                }
            }

            _tuplas.Remove(primaryKey);
            _index.DeleteIndex(primaryKey);
        }

        public void Delete(T entity)
        {
            var primaryKey = _primaryKey(entity);

            Delete(primaryKey);
        }

        public List<T> GetAll()
        {
            return _tuplas.Values.Select<T, T>(ExpressionTreeCloner.DeepFieldClone).ToList();
        }

        public void Delete(Func<T, bool> query)
        {
            var toRemove = _tuplas.Values.Where(query).ToArray();
            foreach (var t in toRemove)
            {
                _tuplas.Remove(t.Id);
            }
        }
    }
}