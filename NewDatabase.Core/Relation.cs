using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NewDatabase.Core
{
    public class Relation
    {
        public Relation()
        {
            Relations = new Dictionary<Type, List<RelationProperties>>();
        }

        public Dictionary<Type, List<RelationProperties>> Relations { get; set; }

        public void CreateOneToOne<T1, T2>(Table<T1> tableWithDependency, Table<T2> tableDependency,
            Expression<Func<T1, Guid>> foreignKey, bool cascateDeletion = true)
            where T1 : Entity
            where T2 : Entity
        {
            var relationProperties = new RelationProperties
            {
                RelationType = RelationType.OneToOne,
                TableWithDependency = tableWithDependency.GetType(),
                TableDependency = tableDependency.GetType(),
                CascateDeletion = cascateDeletion
            };

            relationProperties.OnDeleteOperation((tableType, fk) =>
            {
                if (tableType == tableDependency.GetType() && cascateDeletion)
                {
                    tableDependency.Delete(fk);
                }
            });

            relationProperties.OnForeignKey(entity => foreignKey.Compile().Invoke(entity as T1));

            AddRelationProperties(tableWithDependency.GetType(), relationProperties);
            AddRelationProperties(tableDependency.GetType(), relationProperties);
        }

        private void AddRelationProperties(Type tableType, RelationProperties relationProperties)
        {
            if (Relations.ContainsKey(tableType))
            {
                Relations[tableType].Add(relationProperties);
            }
            else
            {
                Relations[tableType] = new List<RelationProperties> {relationProperties};
            }
        }

        public List<RelationProperties> Get(Type tableType)
        {
            if (Relations.ContainsKey(tableType))
                return Relations[tableType];
            return new List<RelationProperties>();
        }
    }
}