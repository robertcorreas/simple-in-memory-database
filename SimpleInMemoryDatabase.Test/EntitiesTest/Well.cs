using SimpleInMemoryDatabase.Core;

namespace SimpleInMemoryDatabase.Test.EntitiesTest
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

        public Geometry Geometry { get; set; }
        public Trajectory Trajectory { get; private set; }

        #endregion
    }
}