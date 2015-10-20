using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewDatabase.Core;
using NewDatabase.Test.EntitiesTest;

namespace NewDatabase.Test.DataTest
{
    public class DataTest : Data
    {
        public Dictionary<Guid, Well> Wells { get; set; }
        public Dictionary<Guid, Geometry> Geometries{ get; set; }
        public Dictionary<Guid, Trajectory> Trajectories { get; set; }
        public Dictionary<Guid, TrajectoryPoint> TrajectoryPoints { get; set; }
        public Dictionary<Guid, Graphic> Graphics { get; set; }
        public Dictionary<Guid, TrajectoryGraphicRelationalTable> TrajectoryGraphicRelationalTables { get; set; }

        public DataTest()
        {
            Wells = new Dictionary<Guid, Well>();
            Geometries = new Dictionary<Guid, Geometry>();
            Trajectories = new Dictionary<Guid, Trajectory>();
            TrajectoryPoints = new Dictionary<Guid, TrajectoryPoint>();
            Graphics = new Dictionary<Guid, Graphic>();
            TrajectoryGraphicRelationalTables = new Dictionary<Guid, TrajectoryGraphicRelationalTable>();
        }
    }
}
