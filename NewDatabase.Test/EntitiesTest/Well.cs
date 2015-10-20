using NewDatabase.Core;

namespace NewDatabase.Test.EntitiesTest
{
    public class Well : Entity
    {
        public Well(Geometry geometry, Trajectory trajectory)
        {
            Geometry = geometry;
            Trajectory = trajectory;
        }

        public Geometry Geometry { get; private set; }
        public Trajectory Trajectory { get; private set; }
    }
}