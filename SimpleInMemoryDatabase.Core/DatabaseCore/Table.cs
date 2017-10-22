using System;
using System.Collections.Generic;
using System.Linq;
using Nuclex.Cloning;
using SimpleInMemoryDatabase.Core.Api;

namespace SimpleInMemoryDatabase.Core.DatabaseCore
{
    internal class Table<T> : ITable where T : Entity
    {
        private readonly Index _index;
        private readonly Func<T, Guid> _primaryKey;
        private readonly Relation _relation;
        private readonly Dictionary<Guid, T> _tuples;

        #region Construtores

        internal Table(Func<T, Guid> primaryKey, Relation relation = null,
            Index index = null)
        {
            _primaryKey = primaryKey;
            _tuples = new Dictionary<Guid, T>();
            _index = index ?? new Index();
            _relation = relation ?? new Relation();
        }

        #endregion

        public void Insert<T1>(T1 entity) where T1 : Entity
        {
            Insert(entity as T);
        }

        public void Insert<T1>(IEnumerable<T1> entities) where T1 : Entity
        {
            Insert(entities.Cast<T>());
        }

        public IEnumerable<T1> GetAll<T1>() where T1 : Entity
        {
            return GetAll().Cast<T1>();
        }

        public void Delete<T1>(T1 entity) where T1 : Entity
        {
            Delete(entity as T);
        }

        public void Delete<T1>(IEnumerable<T1> entities, Func<T1, bool> query) where T1 : Entity
        {
            Delete(entities.Cast<T>(), query as Func<T, bool>);
        }

        long ITable.Count()
        {
            return _tuples.Count;
        }

        public T1 GetOne<T1>(Guid pk) where T1 : Entity
        {
            return Get(pk) as T1;
        }

        public void Update<T1>(T1 entity) where T1 : Entity
        {
            Update(entity as T);
        }

        public IEnumerable<T1> Search<T1>(Func<T1, bool> predicate) where T1 : Entity
        {
            return Search(predicate as Func<T, bool>).Cast<T1>();
        }

        internal void Insert(T entity)
        {
            if (_tuples.ContainsKey(_primaryKey(entity)))
                throw new ArgumentException("Already exist an entity with this id");

            ValidateFk(entity, GetType());

            var entityCopy = ExpressionTreeCloner.DeepFieldClone(entity);

            _tuples.Add(_primaryKey(entity), entityCopy);
            _index.CreateIndex(_primaryKey(entityCopy));
        }

        public void Insert(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Insert(entity);
        }

        private void ValidateFk(T entity, Type type)
        {
            var relationProperties = _relation.Get(type);

            foreach (var relationPropertie in relationProperties)
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

        internal T Get(Guid primaryKey)
        {
            return ExpressionTreeCloner.DeepFieldClone(_tuples[primaryKey]);
        }

        internal void Delete(Guid primaryKey)
        {
            var entity = _tuples[primaryKey];
            var tableType = GetType();

            var relationProperties = _relation.Get(tableType);

            foreach (var relationPropertie in relationProperties)
                if (relationPropertie.RelationType == RelationType.OneToOne)
                {
                    if (relationPropertie.TableWithDependency == tableType)
                        relationPropertie.DeleteOperation(relationPropertie.TableDependency,
                            relationPropertie.ForeignKey(entity));
                }
                else if (relationPropertie.RelationType == RelationType.OneToMany)
                {
                    if (relationPropertie.TableDependency == tableType)
                        relationPropertie.DeleteOperation(relationPropertie.TableWithDependency, primaryKey);
                }
                else
                {
                    relationPropertie.DeleteOperation(tableType, primaryKey);
                }

            _tuples.Remove(primaryKey);
            _index.DeleteIndex(primaryKey);
        }

        internal void Delete(T entity)
        {
            var primaryKey = _primaryKey(entity);

            Delete(primaryKey);
        }

        internal void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Delete(entity);
        }

        internal void Delete(IEnumerable<T> entities, Func<T, bool> query)
        {
            foreach (var entity in entities.Where(query))
                Delete(entity);
        }

        internal List<T> GetAll()
        {
            return _tuples.Values.Select(ExpressionTreeCloner.DeepFieldClone).ToList();
        }

        internal void Delete(Func<T, bool> query)
        {
            var toRemove = _tuples.Values.Where(query).ToArray();
            foreach (var t in toRemove)
                _tuples.Remove(t.Id);
        }

        internal void Update(T entity)
        {
            if (!_tuples.ContainsKey(_primaryKey(entity)))
                throw new ArgumentException("Invalid entity");

            ValidateFk(entity, GetType());

            _tuples[_primaryKey(entity)] = entity;
        }

        internal IEnumerable<T> Search(Func<T, bool> predicate)
        {
            return _tuples.Values.Where(predicate).Select(ExpressionTreeCloner.DeepFieldClone);
        }
    }
}