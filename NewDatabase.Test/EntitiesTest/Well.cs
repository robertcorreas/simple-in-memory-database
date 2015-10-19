using NewDatabase.Core;

namespace NewDatabase.Test.EntitiesTest
{
    public class Well : Entity
    {
        public Geometry Geometry { get; private set; }

        public Well(Geometry geometry)
        {
            Geometry = geometry;
        }
    }
}
