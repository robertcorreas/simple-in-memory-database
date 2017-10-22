using SimpleInMemoryDatabase.Core;
using SimpleInMemoryDatabase.Core.Api;
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

            Db.CreateTable<Trajectory>(t => t.Id);
            Db.CreateTable<Graphic>(g => g.Id);
            Db.CreateTable<TrajectoryGraphicRelationalTable>(tg => tg.Id);
            Db.CreateTable<TrajectoryPoint>(tp => tp.Id);
            Db.CreateTable<Geometry>(ge => ge.Id);
            Db.CreateTable<Well>(w => w.Id);
        }

        #endregion
    }
}