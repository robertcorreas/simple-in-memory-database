using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;

namespace NewDatabase.Test.Helpers
{
    public class TesteBase
    {
        protected readonly TableFactoryHelper _tableFactoryHelper;
        protected readonly Table<Geometry> geometryTable;
        protected readonly Table<Graphic> graphicTable;
        protected readonly Table<TrajectoryGraphicRelationalTable> trajectoryGraphicRelationalTable;
        protected readonly Table<TrajectoryPoint> trajectoryPointTable;
        protected readonly Table<Trajectory> trajectoryTable;
        protected readonly Table<Well> wellTable;

        #region Construtores

        public TesteBase()
        {
            _tableFactoryHelper = new TableFactoryHelper(new DataTest.DataTest(), new Index(), new Relation());

            trajectoryTable = _tableFactoryHelper.GetTrajectoryTable();
            graphicTable = _tableFactoryHelper.GetGraphicTable();
            trajectoryGraphicRelationalTable = _tableFactoryHelper.GetTrajectoryGraphicRelationalTable();
            trajectoryPointTable = _tableFactoryHelper.GetTrajectoryPointTable();
            geometryTable = _tableFactoryHelper.GetGeometryTable();
            wellTable = _tableFactoryHelper.GetWellTable();
        }

        #endregion

        #region Propriedades

        protected Relation Relation
        {
            get { return _tableFactoryHelper.Relation; }
        }

        protected Index Index
        {
            get { return _tableFactoryHelper.Index; }
        }

        protected DataTest.DataTest Data
        {
            get { return _tableFactoryHelper.Data; }
        }

        #endregion
    }
}