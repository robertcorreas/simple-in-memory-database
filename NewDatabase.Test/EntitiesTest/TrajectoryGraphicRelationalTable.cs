using NewDatabase.Core;

namespace NewDatabase.Test.EntitiesTest
{
    public class TrajectoryGraphicRelationalTable : Entity
    {
        public TrajectoryGraphicRelationalTable(Trajectory trajectory, Graphic graphic)
        {
            Trajectory = trajectory;
            Graphic = graphic;
        }

        public Trajectory Trajectory { get; set; }
        public Graphic Graphic { get; private set; }
    }
}