using System;

namespace SimpleInMemoryDatabase.Core
{
    public class RelationProperties
    {
        #region Propriedades

        public RelationType RelationType { get; set; }
        public Type TableWithDependency { get; set; }
        public Type TableDependency { get; set; }
        public Type RelationalTable { get; set; }
        public Func<Entity, Guid> ForeignKey { get; private set; }
        public Func<Entity, Guid> ForeignKey1 { get; set; }
        public Func<Entity, Guid> ForeignKey2 { get; set; }
        public Action<Type, Guid> DeleteOperation { get; private set; }

        #endregion

        public void OnDeleteOperation(Action<Type, Guid> action)
        {
            DeleteOperation = action;
        }

        public void OnForeignKey(Func<Entity, Guid> fk)
        {
            ForeignKey = fk;
        }
    }
}