using System;
using System.Collections.Generic;
using System.Text;

namespace CASurvive
{
    internal class FireLayer
    {
        private World world;

        private bool[,] fire;
        private int x = 0;
        private int y = 0;

        public FireLayer(World world, int x, int y)
        {
            this.world = world;
            fire = new bool[x, y];
        }

        public void Tick()
        {
            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                {

                }
            }
        }

        private void CalculateLogic(int x, int y)
        {
            for(int xOffset = -1; xOffset < 2;  xOffset++)
            {
                for(int yOffset = -1; yOffset < 2; yOffset++)
                {
                    if (xOffset == 0 && yOffset == 0) continue;


                }
            }
        }
    }
}
