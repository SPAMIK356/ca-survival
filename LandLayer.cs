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

        public LandLayer(int x, int y)
        {
            land = new LandType[x, y];
            Random rnd = new Random();

            for (int i = 0; i < land.GetLength(0); i++)
            {
                for(int j = 0; j < land.GetLength(1); j++)
                {
                    land[i, j] = (LandType)rnd.Next(3); 
                }
            }
        }
        public (int x, int y) GetDimensions()
        {
            return (land.GetLength(0), land.GetLength(1));
        }
    }
}
