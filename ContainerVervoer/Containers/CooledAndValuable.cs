using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class CooledAndValuable : Container
    {
        public CooledAndValuable(int weight, bool _canHaveContainerOnTop) : base(weight, _canHaveContainerOnTop)
        {
           
        }

        public CooledAndValuable()
        {

        }
    }
}
