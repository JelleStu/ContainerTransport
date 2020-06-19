using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class NormalContainer : Container
    {
        public NormalContainer(int _weight)
        {
            weight = _weight;
            canHaveContainerOnTop = true;
        }

        public NormalContainer() { }
    }
}
