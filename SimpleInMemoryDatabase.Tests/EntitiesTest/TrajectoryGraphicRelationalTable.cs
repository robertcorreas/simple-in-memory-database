using SimpleInMemoryDatabase.Lib.Api;

namespace SimpleInMemoryDatabase.Tests.EntitiesTest
{
    public class TrajectoryGraphicRelationalTable : Entity
    {
        #region Construtores

        public TrajectoryGraphicRelationalTable(Trajectory trajectory, Graphic graphic)
        {
            Trajectory = trajectory;
            Graphic = graphic;
        }

        #endregion

        #region Propriedades

        public Trajectory Trajectory { get; set; }
        public Graphic Graphic { get; private set; }

        #endregion
    }
}