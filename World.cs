using System;
using System.Collections.Generic;
using System.Text;

namespace CASurvive
{
    public class World
    {
        public LandLayer  landLayer { get; private set; }
        public CoverLayer coverLayer { get; private set; }

        public World(LandLayer landLayer)
        {
            this.landLayer = landLayer;
        }

    }
}
