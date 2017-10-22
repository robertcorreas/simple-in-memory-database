using SimpleInMemoryDatabase.Core;
using SimpleInMemoryDatabase.Core.Api;

namespace SimpleInMemoryDatabase.Tests.EntitiesTest
{
    public class TrajectoryPoint : Entity
    {
        #region Construtores

        public TrajectoryPoint(Trajectory trajectory)
        {
            Trajectory = trajectory;
        }

        #endregion

        #region Propriedades

        public Trajectory Trajectory { get; set; }

        #endregion
    }
}