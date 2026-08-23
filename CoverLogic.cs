using System;
using System.Collections.Generic;
using System.Text;

namespace CASurvive
{
    internal abstract class CoverLogic
    {

        public abstract CoverType Process(World world, int x, int y);

    }

    class GrassLogic : CoverLogic
    {
        public override CoverType Process(World world, int x, int y)
        {
            
        }
    }
}
