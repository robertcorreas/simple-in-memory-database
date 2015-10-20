using System;
using Newtonsoft.Json;

namespace NewDatabase.Core
{
    public class RelationProperties
    {
        public RelationType RelationType { get; set; }
        public Type TableWithDependency { get; set; }
        public Type TableDependency { get; set; }
        [JsonIgnore]
        public Func<Entity, Guid> ForeignKey { get; private set; }
        [JsonIgnore]
        public Action<Type, Guid> DeleteOperation { get; private set; }
        public bool CascateDeletion { get; set; }

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