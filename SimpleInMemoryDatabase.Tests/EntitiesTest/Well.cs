using SimpleInMemoryDatabase.Lib.Api;

namespace SimpleInMemoryDatabase.Tests.EntitiesTest
{
    public class Well : Entity
    {
        #region Construtores

        public Well(Geometry geometry, Trajectory trajectory)
        {
            Geometry = geometry;
            Trajectory = trajectory;
        }

        #endregion

        #region Propriedades

        public string Nome { get; set; }
        public Geometry Geometry { get; set; }
        public Trajectory Trajectory { get; private set; }

        #endregion
    }
}