using SimpleInMemoryDatabase.Core;
using SimpleInMemoryDatabase.Tests.EntitiesTest;

namespace SimpleInMemoryDatabase.Tests.Helpers
{
    public class TestBase
    {
        protected readonly TableFactoryHelper _tableFactoryHelper;
        protected readonly Table<Geometry> geometryTable;
        protected readonly Table<Graphic> graphicTable;
        protected readonly Table<TrajectoryGraphicRelationalTable> trajectoryGraphicRelationalTable;
        protected readonly Table<TrajectoryPoint> trajectoryPointTable;
        protected readonly Table<Trajectory> trajectoryTable;
        protected readonly Table<Well> wellTable;

        protected readonly Database Db;

        #region Construtores

        public TestBase()
        {
            _tableFactoryHelper = new TableFactoryHelper(new DataTest.DataTest(), new Index(), new Relation());

            Db = new Database();

            Db.CreateTable<Trajectory>(t => t.Id);
            Db.CreateTable<Graphic>(g => g.Id);
            Db.CreateTable<TrajectoryGraphicRelationalTable>(tg => tg.Id);
            Db.CreateTable<TrajectoryPoint>(tp => tp.Id);
            Db.CreateTable<Geometry>(ge => ge.Id);
            Db.CreateTable<Well>(w => w.Id);

            //trajectoryTable = _tableFactoryHelper.GetTrajectoryTable();
            //graphicTable = _tableFactoryHelper.GetGraphicTable();
            //trajectoryGraphicRelationalTable = _tableFactoryHelper.GetTrajectoryGraphicRelationalTable();
            //trajectoryPointTable = _tableFactoryHelper.GetTrajectoryPointTable();
            //geometryTable = _tableFactoryHelper.GetGeometryTable();
            //wellTable = _tableFactoryHelper.GetWellTable();
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