using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SimpleInMemoryDatabase.Lib.Api;

namespace SimpleInMemoryDatabase.Lib.Core
{
    internal class Database : IDatabase
    {
        private readonly Index _index;
        private readonly Relation _relation;
        private readonly Dictionary<Type, ITable> _tables;

        internal Database()
        {
            _tables = new Dictionary<Type, ITable>();
            _relation = new Relation();
            _index = new Index();
        }

        public void CreateTable<T>(Expression<Func<T, Guid>> primaryKey) where T : Entity
        {
            primaryKey.Compile();
            var t = new Table<T>(primaryKey.Compile(), _relation, _index);
            _tables.Add(typeof(T), t);
        }

        public void CreateManyToMany<T1, T2, T3>(Expression<Func<T3, Guid>> fk1, Expression<Func<T3, Guid>> fk2,
            bool cascateDeletion = true)
            where T1 : Entity
            where T2 : Entity
            where T3 : Entity
        {
            var t1 = _tables[typeof(T1)] as Table<T1>;
            var t2 = _tables[typeof(T2)] as Table<T2>;
            var t3 = _tables[typeof(T3)] as Table<T3>;

            _relation.CreateManyToMany(t1, t2, t3, fk1, fk2, cascateDeletion);
        }

        public void CreateOneToMany<T1, T2>(Expression<Func<T2, Guid>> fk, bool cascateDeletion = true)
            where T1 : Entity where T2 : Entity
        {
            var t1 = _tables[typeof(T1)] as Table<T1>;
            var t2 = _tables[typeof(T2)] as Table<T2>;

            _relation.CreateOneToMany(t1, t2, fk.Compile(), cascateDeletion);
        }

        public void CreateOneToOne<T1, T2>(Expression<Func<T1, Guid>> fk, bool cascateDeletion = true)
            where T1 : Entity
            where T2 : Entity
        {
            var t1 = _tables[typeof(T1)] as Table<T1>;
            var t2 = _tables[typeof(T2)] as Table<T2>;

            _relation.CreateOneToOne(t1, t2, fk.Compile(), cascateDeletion);
        }

        public void Insert<T>(T entity) where T : Entity
        {
            _tables[typeof(T)].Insert(entity);
        }

        public void Insert<T>(IEnumerable<T> entities) where T : Entity
        {
            _tables[typeof(T)].Insert(entities);
        }

        public void InsertOrUpdate<T>(T entity) where T : Entity
        {
            try
            {
                _tables[typeof(T)].Update(entity);
            }
            catch (Exception e)
            {
                _tables[typeof(T)].Insert(entity);
            }
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _tables[typeof(T)].Delete(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return _tables[typeof(T)].GetAll<T>();
        }

        public T GetOne<T>(Guid pk) where T : Entity
        {
            return _tables[typeof(T)].GetOne<T>(pk);
        }

        public long Count<T>() where T : Entity
        {
            return _tables[typeof(T)].Count();
        }

        public void Delete<T>(IEnumerable<T> entities, Func<T, bool> query) where T : Entity
        {
            _tables[typeof(T)].Delete(entities, query);
        }

        public void DeleteAll<T>()
        {
            _tables[typeof(T)].DeleteAll<T>();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _tables[typeof(T)].Update(entity);
        }

        public void Update<T>(IEnumerable<T> entities) where T : Entity
        {
            _tables[typeof(T)].Update(entities);
        }

        public IEnumerable<T> Search<T>(Func<T, bool> predicate) where T : Entity
        {
            return _tables[typeof(T)].Search(predicate);
        }

        public long IndexCount()
        {
            return _index.Count;
        }
    }
}