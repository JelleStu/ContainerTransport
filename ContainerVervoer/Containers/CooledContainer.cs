using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class CooledContainer : Container
    {
        public CooledContainer(int _weight)
        {
            weight = _weight;
            canHaveContainerOnTop = true;
        }
        public CooledContainer() { }
    }
}
