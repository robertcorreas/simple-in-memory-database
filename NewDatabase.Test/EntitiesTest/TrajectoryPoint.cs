using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewDatabase.Core;

namespace NewDatabase.Test.EntitiesTest
{
    public class TrajectoryPoint : Entity
    {
        public Trajectory Trajectory { get; private set; }

        public TrajectoryPoint(Trajectory trajectory)
        {
            Trajectory = trajectory;
        }
    }
}
