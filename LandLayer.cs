using System;
using System.Collections.Generic;
using System.Text;

namespace CASurvive
{
    public enum LandType
    {
        Dirt,
        Rock,
        Sand
    }
    public class LandLayer
    {
        LandType[,] land;

        public LandType GetLandAt(int x, int y) => land[x, y];


    }
}
