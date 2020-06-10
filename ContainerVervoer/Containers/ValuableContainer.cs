using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class ValuableContainer : Container
    {
        public ValuableContainer(int weight, bool _canHaveContainerOnTop) : base(weight, _canHaveContainerOnTop)
        {
        }
        public ValuableContainer() { }
    }
}
