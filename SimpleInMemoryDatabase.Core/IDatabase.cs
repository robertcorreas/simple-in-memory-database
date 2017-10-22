using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SimpleInMemoryDatabase.Core
{
    public interface IDatabase
    {
        void CreateTable<T>(Expression<Func<T, Guid>> primaryKey) where T : Entity;

        void CreateManyToMany<T1, T2, T3>(Expression<Func<T3, Guid>> fk1, Expression<Func<T3, Guid>> fk2,
            bool cascateDeletion = true)
            where T1 : Entity
            where T2 : Entity
            where T3 : Entity;

        void CreateOneToMany<T1, T2>(Expression<Func<T2, Guid>> fk, bool cascateDeletion = true)
            where T1 : Entity where T2 : Entity;

        void CreateOneToOne<T1, T2>(Expression<Func<T1, Guid>> fk, bool cascateDeletion = true)
            where T1 : Entity
            where T2 : Entity;

        void Insert<T>(T entity) where T : Entity;
        void Insert<T>(IEnumerable<T> entities) where T : Entity;
        void Delete<T>(T entity) where T : Entity;
        IEnumerable<T> GetAll<T>() where T : Entity;
        T GetOne<T>(Guid pk) where T : Entity;
        long Count<T>() where T : Entity;
        void Delete<T>(IEnumerable<T> entities, Func<T, bool> query) where T : Entity;
        void Update<T>(T entity) where T : Entity;
        IEnumerable<T> Search<T>(Func<T, bool> predicate) where T : Entity;

        long IndexCount();
    }
}