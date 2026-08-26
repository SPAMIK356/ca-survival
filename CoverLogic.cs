using System;
using System.Collections.Generic;
using System.Text;

namespace CASurvive
{
    public abstract class CoverLogic
    {

        public abstract CoverType Process(World world, int x, int y);

    }

    public class GrassLogic : CoverLogic
    {
        public override CoverType Process(World world, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
