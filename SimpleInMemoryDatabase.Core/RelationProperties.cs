using System;

namespace SimpleInMemoryDatabase.Core
{
    internal class RelationProperties
    {
        #region Propriedades

        internal RelationType RelationType { get; set; }
        internal Type TableWithDependency { get; set; }
        internal Type TableDependency { get; set; }
        internal Type RelationalTable { get; set; }
        internal Func<Entity, Guid> ForeignKey { get; private set; }
        internal Func<Entity, Guid> ForeignKey1 { get; set; }
        internal Func<Entity, Guid> ForeignKey2 { get; set; }
        internal Action<Type, Guid> DeleteOperation { get; private set; }

        #endregion

        internal void OnDeleteOperation(Action<Type, Guid> action)
        {
            DeleteOperation = action;
        }

        internal void OnForeignKey(Func<Entity, Guid> fk)
        {
            ForeignKey = fk;
        }
    }
}