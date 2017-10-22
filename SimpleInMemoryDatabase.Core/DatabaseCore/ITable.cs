using System;
using System.Collections.Generic;
using SimpleInMemoryDatabase.Core.Api;

namespace SimpleInMemoryDatabase.Core.DatabaseCore
{
    internal interface ITable
    {
        void Insert<T>(T entity) where T : Entity;
        void Insert<T>(IEnumerable<T> entities) where T : Entity;
        IEnumerable<T> GetAll<T>() where T : Entity;
        void Delete<T>(T entity) where T : Entity;
        void Delete<T>(IEnumerable<T> entities, Func<T, bool> query) where T : Entity;
        long Count();
        T GetOne<T>(Guid pk) where T : Entity;
        void Update<T>(T entity) where T : Entity;
        IEnumerable<T> Search<T>(Func<T, bool> predicate) where T : Entity;
    }
}