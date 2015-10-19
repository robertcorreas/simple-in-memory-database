using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewDatabase.Core
{
    public class Relation
    {
        private readonly Dictionary<Type, List<RelationProperties>> _relations;

        public Relation()
        {
            _relations = new Dictionary<Type, List<RelationProperties>>();
        }

        public void CreateOneToOne<T1, T2>(Table<T1> tableWithDependency, Table<T2> tableDependency, Expression<Func<T1, Guid>> foreignKey, bool cascateDeletion = true)
            where T1 : Entity
            where T2 : Entity
        {
            var relationProperties = new RelationProperties
            {
                RelationType = RelationType.OneToOne,
                TableWithDependency = tableWithDependency.GetType(),
                TableDependency = tableDependency.GetType(),
                ForeignKey = entity => foreignKey.Compile().Invoke(entity as T1),
                CascateDeletion = cascateDeletion,
                DeleteOperation = (tableType, fk) =>
                {
                    if (tableType == tableDependency.GetType() && cascateDeletion)
                    {
                        tableDependency.Delete(fk);
                    }
                } 
            };

            AddRelationProperties(tableWithDependency.GetType(), relationProperties);
            AddRelationProperties(tableDependency.GetType(), relationProperties);
        }

        private void AddRelationProperties(Type tableType, RelationProperties relationProperties)
        {
            if (_relations.ContainsKey(tableType))
            {

                _relations[tableType].Add(relationProperties);
            }
            else
            {
                _relations[tableType] = new List<RelationProperties>() { relationProperties };
            }
        }

        public List<RelationProperties> Get(Type tableType)
        {
            if(_relations.ContainsKey(tableType))
                return _relations[tableType];
            return new List<RelationProperties>();
        }
    }


    public class RelationProperties
    {
        public RelationType RelationType { get; set; }
        public Type TableWithDependency { get; set; }
        public Type TableDependency { get; set; }
        public Func<Entity,Guid> ForeignKey { get; set; }
        public Action<Type,Guid> DeleteOperation { get; set; }
        public bool CascateDeletion { get; set; }
    }

    public enum RelationType
    {
        OneToOne
    }
}
