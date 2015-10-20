using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewDatabase.Core;

namespace NewDatabase.Test.EntitiesTest
{
    public class TrajectoryGraphicRelationalTable : Entity
    {
        public Trajectory Trajectory { get; private set; }
        public Graphic Graphic { get; private set; }

        public TrajectoryGraphicRelationalTable(Trajectory trajectory, Graphic graphic)
        {
            Trajectory = trajectory;
            Graphic = graphic;
        }
    }
}
