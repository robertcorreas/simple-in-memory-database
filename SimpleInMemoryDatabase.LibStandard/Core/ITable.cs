using System;
using System.Collections.Generic;
using SimpleInMemoryDatabase.Lib.Api;

namespace SimpleInMemoryDatabase.Lib.Core
{
    internal interface ITable
    {
        void Insert<T>(T entity) where T : Entity;
        void Insert<T>(IEnumerable<T> entities) where T : Entity;
        IEnumerable<T> GetAll<T>() where T : Entity;
        void Delete<T>(T entity) where T : Entity;
        void Delete<T>(IEnumerable<T> entities, Func<T, bool> query) where T : Entity;
        void DeleteAll<T>();
        long Count();
        T GetOne<T>(Guid pk) where T : Entity;
        void Update<T>(T entity) where T : Entity;
        void Update<T>(IEnumerable<T> entities) where T : Entity;
        IEnumerable<T> Search<T>(Func<T, bool> predicate) where T : Entity;
    }
}