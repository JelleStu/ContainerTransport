using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class ValuableContainer : Container
    {
        public ValuableContainer(int _weight)
        {
            weight = _weight;
            canHaveContainerOnTop = false;
        }
        public ValuableContainer() { }
    }
}
