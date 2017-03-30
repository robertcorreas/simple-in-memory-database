using SimpleInMemoryDatabase.Core;
using SimpleInMemoryDatabase.Tests.EntitiesTest;

namespace SimpleInMemoryDatabase.Tests.Helpers
{
    public class TableFactoryHelper
    {
        #region Construtores

        public TableFactoryHelper(DataTest.DataTest data, Index index, Relation relation)
        {
            Data = data;
            Index = index;
            Relation = relation;
        }

        #endregion

        #region Propriedades

        public DataTest.DataTest Data { get; set; }
        public Index Index { get; private set; }
        public Relation Relation { get; private set; }

        #endregion

        public Table<Well> GetWellTable()
        {
            return new Table<Well>(Data.Wells, w => w.Id, Relation, Index);
        }

        public Table<Geometry> GetGeometryTable()
        {
            return new Table<Geometry>(Data.Geometries, g => g.Id, Relation, Index);
        }

        public Table<Trajectory> GetTrajectoryTable()
        {
            return new Table<Trajectory>(Data.Trajectories, t => t.Id, Relation, Index);
        }

        public Table<TrajectoryPoint> GetTrajectoryPointTable()
        {
            return new Table<TrajectoryPoint>(Data.TrajectoryPoints, tp => tp.Id, Relation, Index);
        }

        public Table<Graphic> GetGraphicTable()
        {
            return new Table<Graphic>(Data.Graphics, g => g.Id, Relation, Index);
        }

        public Table<TrajectoryGraphicRelationalTable> GetTrajectoryGraphicRelationalTable()
        {
            return new Table<TrajectoryGraphicRelationalTable>(Data.TrajectoryGraphicRelationalTables, tgr => tgr.Id, Relation, Index);
        }
    }
}