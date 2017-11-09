using SimpleInMemoryDatabase.Lib.Api;
using SimpleInMemoryDatabase.Tests.EntitiesTest;

namespace SimpleInMemoryDatabase.Tests.Helpers
{
    public class TestBase
    {
        protected readonly IDatabase Db;

        #region Construtores

        public TestBase()
        {
            Db = DatabaseCreator.Create();

            Db.CreateTable<Trajectory>();
            Db.CreateTable<Graphic>();
            Db.CreateTable<TrajectoryGraphicRelationalTable>();
            Db.CreateTable<TrajectoryPoint>();
            Db.CreateTable<Geometry>();
            Db.CreateTable<Well>();
        }

        #endregion
    }
}