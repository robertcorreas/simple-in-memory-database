using SimpleInMemoryDatabase.Core;
using SimpleInMemoryDatabase.Tests.EntitiesTest;

namespace SimpleInMemoryDatabase.Tests.Helpers
{
    public class TestBase
    {
        protected readonly Database Db;

        #region Construtores

        public TestBase()
        {
            Db = new Database();

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