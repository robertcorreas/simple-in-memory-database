using NewDatabase.Core;

namespace NewDatabase.Test.EntitiesTest
{
    public class Well : Entity
    {
        public Geometry Geometry { get; private set; }
        public Trajectory Trajectory { get; private set; }

        public Well(Geometry geometry,Trajectory trajectory)
        {
            Geometry = geometry;
            Trajectory = trajectory;
        }
    }
}
