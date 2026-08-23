using System;
using System.Collections.Generic;
using System.Text;

namespace CASurvive
{
    enum LandType
    {
        Dirt,
        Rock,
        Sand
    }
    internal class LandLayer
    {
        LandType[,] land;

        public LandType GetLandAt(int x, int y) => land[x, y];


    }
}
