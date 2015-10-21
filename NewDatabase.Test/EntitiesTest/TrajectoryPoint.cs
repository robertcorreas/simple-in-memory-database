using NewDatabase.Core;

namespace NewDatabase.Test.EntitiesTest
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