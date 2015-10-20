using System;

namespace NewDatabase.Core
{
    public class RelationProperties
    {
        public RelationType RelationType { get; set; }
        public Type TableWithDependency { get; set; }
        public Type TableDependency { get; set; }
        public Type RelationalTable { get; set; }
        public Func<Entity, Guid> ForeignKey { get; private set; }
        public Func<Entity, Guid> ForeignKey1 { get; set; }
        public Func<Entity, Guid> ForeignKey2 { get; set; }
        public Action<Type, Guid> DeleteOperation { get; private set; }

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