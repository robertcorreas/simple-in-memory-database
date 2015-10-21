using NewDatabase.Core;

namespace NewDatabase.Test.EntitiesTest
{
    public class TrajectoryPoint : Entity
    {
        public TrajectoryPoint(Trajectory trajectory)
        {
            Trajectory = trajectory;
        }

        public Trajectory Trajectory { get;  set; }
    }
}