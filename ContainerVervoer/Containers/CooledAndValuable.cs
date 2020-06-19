using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class CooledAndValuable : Container
    {
        public CooledAndValuable(int _weight)
        {
            weight = _weight;
            canHaveContainerOnTop = false;
        }

        public CooledAndValuable()
        {

        }
    }
}
