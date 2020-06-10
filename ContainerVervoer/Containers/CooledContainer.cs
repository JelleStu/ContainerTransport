using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class CooledContainer : Container
    {
        public CooledContainer(int weight, bool canHaveContainerOnTop) : base(weight, canHaveContainerOnTop) { }
        public CooledContainer() { }
    }
}
