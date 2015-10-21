using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SimpleInMemoryDatabase.Core
{
    public class Relation
    {
        #region Construtores

        public Relation()
        {
            Relations = new Dictionary<Type, List<RelationProperties>>();
        }

        #endregion

        #region Propriedades

        public Dictionary<Type, List<RelationProperties>> Relations { get; set; }

        #endregion

        public void CreateOneToOne<T1, T2>(Table<T1> tableWithDependency, Table<T2> tableDependency,
            Expression<Func<T1, Guid>> foreignKey, bool cascateDeletion = true)
            where T1 : Entity
            where T2 : Entity
        {
            var compiledForeignKey = foreignKey.Compile();

            var relationProperties = new RelationProperties
            {
                RelationType = RelationType.OneToOne,
                TableWithDependency = tableWithDependency.GetType(),
                TableDependency = tableDependency.GetType()
            };

            relationProperties.OnDeleteOperation((tableType, fk) =>
            {
                if (tableType == tableDependency.GetType() && cascateDeletion)
                {
                    tableDependency.Delete(fk);
                }
            });

            relationProperties.OnForeignKey(entity => compiledForeignKey(entity as T1));

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

        public void CreateOneToMany<T1, T2>(Table<T1> tableDependency, Table<T2> tableWithDependency,
            Expression<Func<T2, Guid>> foreignKey, bool cascateDeletion = true) where T1 : Entity where T2 : Entity
        {
            var compiledForeignKey = foreignKey.Compile();

            var relationProperties = new RelationProperties
            {
                RelationType = RelationType.OneToMany,
                TableWithDependency = tableWithDependency.GetType(),
                TableDependency = tableDependency.GetType()
            };

            relationProperties.OnDeleteOperation((tableType, fk) =>
            {
                if (tableType == tableWithDependency.GetType() && cascateDeletion)
                {
                    tableWithDependency.Delete(t => relationProperties.ForeignKey(t) == fk);
                }
            });

            relationProperties.OnForeignKey(entity => compiledForeignKey(entity as T2));

            AddRelationProperties(tableWithDependency.GetType(), relationProperties);
            AddRelationProperties(tableDependency.GetType(), relationProperties);
        }

        public void CreateManyToMany<T1, T2, T3>(Table<T1> table1, Table<T2> table2, Table<T3> relationalTable,
            Expression<Func<T3, Guid>> foreignKey1, Expression<Func<T3, Guid>> foreignKey2, bool cascateDeletion = true)
            where T1 : Entity
            where T2 : Entity
            where T3 : Entity
        {
            var compiledForeignKey1 = foreignKey1.Compile();
            var compiledForeignKey2 = foreignKey2.Compile();

            var relationProperties = new RelationProperties
            {
                RelationType = RelationType.ManyToMany,
                TableWithDependency = table1.GetType(),
                TableDependency = table2.GetType(),
                RelationalTable = relationalTable.GetType(),
                ForeignKey1 = entity => compiledForeignKey1(entity as T3),
                ForeignKey2 = entity => compiledForeignKey2(entity as T3)
            };

            relationProperties.OnDeleteOperation((tableType, fk) =>
            {
                if ((tableType == table1.GetType() || tableType == table2.GetType()) && cascateDeletion)
                {
                    relationalTable.Delete(
                        t => relationProperties.ForeignKey1(t) == fk || relationProperties.ForeignKey2(t) == fk);
                }
            });

            AddRelationProperties(table1.GetType(), relationProperties);
            AddRelationProperties(table2.GetType(), relationProperties);
            AddRelationProperties(relationalTable.GetType(), relationProperties);
        }
    }
}